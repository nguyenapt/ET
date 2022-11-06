<script>
import { mapActions, mapGetters } from "vuex";
import { format } from "date-fns";
import { BLANK, DATE_FORMAT, DATE_PARSE } from "../../../constants";

const REQUEST_BODY = [
  "Client",
  "Department",
  "Project",
  "Resource",
  "Beneficiary"
];

export default {
  data() {
    return {
      callbackOnClose: null,
      startDateFormatted: BLANK,
      endDateFormatted: BLANK,
      showDialog: false,
      showStart: false,
      showEnd: false,
      sow: {},
      initSelectors: {},
      rules: {
        nameRules: [v => (!v || !v.trim()) && "Name is required"],
        clientRules: [v => !!v || "Client is required"],
        projectRules: [v => !!v || "Project is required"]
      }
    };
  },
  computed: {
    ...mapGetters("sow", ["selectors"]),
    dateRules() {
      const rules = [];

      rules.push(() => {
        const { startDate, endDate } = this.sow;

        if (!startDate || !endDate) {
          return true;
        }

        if (new Date(startDate).getTime() > new Date(endDate).getTime()) {
          return "Start Date must be less than or equal to End Date";
        }

        return true;
      });

      return rules;
    }
  },
  methods: {
    ...mapActions("sow", ["getSelectors", "createSow"]),
    ...mapActions("layout", ["setSnackbar", "setCommonErrorToast"]),
    async init() {
      await this.getSelectors(REQUEST_BODY);
      this.initSelectors = { ...this.selectors };
    },
    create() {
      const isValidForm = this.$refs.form.validate();

      if (isValidForm) {
        this.createSow(this.sow)
          .then(() => {
            if (this.callbackOnClose) {
              this.callbackOnClose();
            }

            this.close();

            this.setSnackbar({
              text: "Created successfully!"
            });
          })
          .catch(() => {
            this.setCommonErrorToast();
          });
      }
    },
    open({ callbackOnClose }) {
      if (callbackOnClose) {
        this.callbackOnClose = callbackOnClose;
      }

      this.showDialog = true;
    },
    close() {
      this.sow = {};
      this.callbackOnClose = null;
      this.$refs.form.reset();
      this.showDialog = false;
    },
    pickStartDate(val) {
      this.startDateFormatted = this.formatDate(val);
      this.showStart = false;
    },
    pickEndDate(val) {
      this.endDateFormatted = this.formatDate(val);
      this.showEnd = false;
    },
    formatDate(date) {
      if (!date) return null;

      return format(new Date(date), DATE_FORMAT);
    },
    parseDate(date) {
      if (!date) return null;

      return format(new Date(date), DATE_PARSE);
    }
  },
  created() {
    try {
      this.init();
    } catch (e) {
      console.error(e);
    }
  }
};
</script>
<template>
  <v-dialog v-model="showDialog" @click:outside="close" max-width="800">
    <v-card>
      <v-form ref="form">
        <v-card-title primary-title class="headline grey lighten-2"
          >Create SoW</v-card-title
        >

        <v-divider></v-divider>

        <v-container>
          <v-row no-gutters>
            <v-col cols="12" md="6">
              <v-autocomplete
                dense
                outlined
                v-model="sow.clientId"
                :items="initSelectors.Client"
                :rules="rules.clientRules"
                item-text="name"
                item-value="value"
                label="Client Name"
                class="px-2"
              ></v-autocomplete>
            </v-col>
            <v-col cols="12" md="6">
              <v-autocomplete
                dense
                outlined
                v-model="sow.departmentId"
                :items="initSelectors.Department"
                clearable
                item-text="name"
                item-value="value"
                label="Division"
                class="px-2"
              ></v-autocomplete>
            </v-col>
            <v-col cols="12" md="6">
              <v-menu
                v-model="showStart"
                :close-on-content-click="false"
                transition="scale-transition"
                offset-y
                min-width="290px"
                class="px-2"
              >
                <template v-slot:activator="{ on }">
                  <v-text-field
                    dense
                    outlined
                    clearable
                    label="Start Date"
                    placeholder="dd/mm/yyyy"
                    class="px-2"
                    v-on="on"
                    :rules="dateRules"
                    v-model="startDateFormatted"
                    @blur="sow.startDate = parseDate(startDateFormatted)"
                  ></v-text-field>
                </template>
                <v-date-picker
                  v-model="sow.startDate"
                  @change="pickStartDate"
                  no-title
                ></v-date-picker>
              </v-menu>
            </v-col>
            <v-col cols="12" md="6">
              <v-menu
                v-model="showEnd"
                :close-on-content-click="false"
                transition="scale-transition"
                offset-y
                min-width="290px"
              >
                <template v-slot:activator="{ on }">
                  <v-text-field
                    dense
                    outlined
                    clearable
                    label="End Date"
                    placeholder="dd/mm/yyyy"
                    class="px-2"
                    v-on="on"
                    :rules="dateRules"
                    v-model="endDateFormatted"
                    @blur="sow.endDate = parseDate(endDateFormatted)"
                  ></v-text-field>
                </template>
                <v-date-picker
                  v-model="sow.endDate"
                  @change="pickEndDate"
                  no-title
                ></v-date-picker>
              </v-menu>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model.trim="sow.sowCode"
                label="SoW Code"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model.trim="sow.version"
                label="Version"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-autocomplete
                dense
                outlined
                v-model="sow.projectId"
                :items="initSelectors.Project"
                :rules="rules.projectRules"
                item-text="name"
                item-value="value"
                label="Project"
                class="px-2"
              ></v-autocomplete>
            </v-col>
            <v-col cols="12">
              <v-text-field
                dense
                outlined
                :rules="rules.nameRules"
                v-model.trim="sow.name"
                required
                label="SoW Name"
                class="px-2"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-container>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="close">Cancel</v-btn>
          <v-btn color="primary" @click="create">Create</v-btn>
        </v-card-actions>
      </v-form>
    </v-card>
  </v-dialog>
</template>
