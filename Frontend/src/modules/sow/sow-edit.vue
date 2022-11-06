<script>
import Layout from "../_layouts/default.layout";
import SowRole from "./table/sow-role";
import SowRoleAddDialog from "./dialog/sow-role-add";
import { format } from "date-fns";
import { mapActions, mapGetters } from "vuex";
import {
  BLANK,
  DATE_FORMAT,
  DATE_PARSE,
  SELECTORS_STATUS,
  STATUS
} from "../../constants";

const REQUEST_BODY = [
  "Client",
  "Department",
  "Project",
  "Resource",
  "BillingType",
  "ResourceRole",
  "RateType",
  "Currency"
];

export default {
  components: {
    Layout,
    SowRole,
    SowRoleAddDialog
  },
  data: () => ({
    isLoading: false,
    showStart: false, // show start date picker
    showEnd: false, // show end date picker
    startDateFormatted: BLANK,
    endDateFormatted: BLANK,
    title: "Edit Sow Page",
    selectorsStatus: [],
    isDisabled: false,
    rules: {
      nameRules: [v => (!v || !v.trim()) && "Name is required"]
    },
    initSowDetail: {}
  }),
  computed: {
    ...mapGetters("sow", ["sow", "selectors"]),
    dateRules() {
      const rules = [];

      rules.push(() => {
        const { startDate, endDate } = this.initSowDetail;

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
    ...mapActions("sow", [
      "getSelectors",
      "editSow",
      "getSowById",
      "dowloadSow",
      "createNewVersion"
    ]),
    ...mapActions("layout", ["setSnackbar", "setCommonErrorToast"]),
    init() {
      this.isLoading = true;

      Promise.all([
        this.getSelectors(REQUEST_BODY),
        this.getSowById(this.$route.params.id)
      ])
        .then(() => {
          this.initSowDetail = { ...this.sow };
          this.selectorsStatus = SELECTORS_STATUS[this.sow.status];
          this.isDisabled = this.sow.status !== STATUS.DRAFT;
          this.startDateFormatted = this.formatDate(
            this.initSowDetail.startDate
          );
          this.endDateFormatted = this.formatDate(this.initSowDetail.endDate);
          this.isLoading = false;
        })
        .catch(() => {
          this.setCommonErrorToast();
        });
    },
    submit() {
      const isFormValid = this.$refs.form.validate();

      if (isFormValid) {
        this.editSow(this.initSowDetail)
          .then(() => {
            this.init();
            this.setSnackbar({ text: "Edit successfully!" });
          })
          .catch(() => {
            this.setCommonErrorToast();
          });
      }
    },
    openCreateRowDialog() {
      this.$refs.dialog.open({
        callbackOnClose: this.init
      });
    },
    viewDetail() {
      this.$router.push(`/sow/${this.$route.params.id}`);
    },
    backToList() {
      this.$router.push("/sow");
    },
    download() {
      this.dowloadSow(this.initSowDetail);
    },
    handlerNewVersion() {
      this.createNewVersion(this.$route.params.id)
        .then(newSow => {
          this.setSnackbar({ text: "Create new version successfully!" });
          this.$router.push(`/sow/edit/${newSow.id}`);
          this.init();
        })
        .catch(() => {
          this.setCommonErrorToast();
        });
    },
    formatDate(date) {
      if (!date) return null;

      return format(new Date(date), DATE_FORMAT);
    },
    parseDate(date) {
      if (!date) return null;

      return format(new Date(date), DATE_PARSE);
    },
    pickStartDate(val) {
      this.startDateFormatted = this.formatDate(val);
      this.showStart = false;
    },
    pickEndDate(val) {
      this.endDateFormatted = this.formatDate(val);
      this.showEnd = false;
    }
  },
  created() {
    this.init();
  }
};
</script>

<template>
  <Layout>
    <v-skeleton-loader
      v-if="isLoading"
      ref="skeleton"
      type="table"
      class="mx-auto"
    ></v-skeleton-loader>
    <v-container v-else>
      <v-row justify="center">
        <h4>{{ title }}</h4>
      </v-row>
      <v-row justify="center">
        <v-col cols="12" class="white">
          <v-form ref="form">
            <v-row no-gutters>
              <v-col cols="12" sm="3">
                <v-autocomplete
                  :disabled="isDisabled"
                  dense
                  outlined
                  v-model="initSowDetail.clientId"
                  :items="selectors.Client"
                  item-text="name"
                  item-value="value"
                  label="Client Name"
                  class="px-2"
                ></v-autocomplete>
              </v-col>
              <v-col cols="12" sm="3">
                <v-autocomplete
                  :disabled="isDisabled"
                  dense
                  outlined
                  v-model="initSowDetail.departmentId"
                  :items="selectors.Department"
                  clearable
                  item-text="name"
                  item-value="value"
                  label="Division"
                  class="px-2"
                ></v-autocomplete>
              </v-col>
              <v-col cols="12" sm="3">
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
                      :disabled="isDisabled"
                      dense
                      outlined
                      v-model="startDateFormatted"
                      :rules="dateRules"
                      label="Start Date"
                      placeholder="dd/mm/yyyy"
                      clearable
                      v-on="on"
                      class="px-2"
                      @blur="
                        initSowDetail.startDate = parseDate(startDateFormatted)
                      "
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    v-model="initSowDetail.startDate"
                    @change="pickStartDate"
                    no-title
                  ></v-date-picker>
                </v-menu>
              </v-col>
              <v-col cols="12" sm="3">
                <v-menu
                  v-model="showEnd"
                  :close-on-content-click="false"
                  transition="scale-transition"
                  offset-y
                  min-width="290px"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      :disabled="isDisabled"
                      dense
                      outlined
                      :rules="dateRules"
                      v-model="endDateFormatted"
                      label="End Date"
                      placeholder="dd/mm/yyyy"
                      clearable
                      v-on="on"
                      class="px-2"
                      @blur="
                        initSowDetail.endDate = parseDate(endDateFormatted)
                      "
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    v-model="initSowDetail.endDate"
                    @change="pickEndDate"
                    no-title
                  ></v-date-picker>
                </v-menu>
              </v-col>
              <v-col cols="12" sm="3">
                <v-text-field
                  :disabled="isDisabled"
                  dense
                  outlined
                  v-model="initSowDetail.fileUrl"
                  required
                  label="File URL"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="3">
                <v-autocomplete
                  dense
                  outlined
                  v-model="initSowDetail.status"
                  :items="selectorsStatus"
                  label="Status"
                  class="px-2"
                ></v-autocomplete>
              </v-col>
              <v-col cols="12" sm="3">
                <v-autocomplete
                  :disabled="isDisabled"
                  dense
                  outlined
                  v-model="initSowDetail.kamResourceId"
                  :items="selectors.Resource"
                  clearable
                  item-text="name"
                  item-value="value"
                  label="KAM name"
                  class="px-2"
                ></v-autocomplete>
              </v-col>
              <v-col cols="12" sm="3">
                <v-text-field
                  :disabled="isDisabled"
                  dense
                  outlined
                  v-model="initSowDetail.clientPONumber"
                  required
                  label="Client PO Number"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-autocomplete
                  :disabled="isDisabled"
                  dense
                  outlined
                  v-model="initSowDetail.projectId"
                  :items="selectors.Project"
                  item-text="name"
                  item-value="value"
                  label="Project"
                  class="px-2"
                ></v-autocomplete>
              </v-col>
              <v-col cols="12" sm="6">
                <v-text-field
                  :disabled="isDisabled"
                  dense
                  outlined
                  :rules="rules.nameRules"
                  v-model.trim="initSowDetail.name"
                  required
                  label="SoW Name"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-text-field
                  dense
                  outlined
                  :disabled="isDisabled"
                  v-model.trim="initSowDetail.sowCode"
                  label="SoW Code"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-text-field
                  dense
                  outlined
                  :disabled="isDisabled"
                  v-model.trim="initSowDetail.version"
                  required
                  label="Version"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-textarea
                  :disabled="isDisabled"
                  outlined
                  v-model="initSowDetail.description"
                  label="Input your sow note here"
                  :rows="3"
                  class="px-2"
                ></v-textarea>
              </v-col>
            </v-row>
            <v-row>
              <v-col class="text-right">
                <v-btn @click="backToList" color="primary" class="mx-2"
                  >Back to list</v-btn
                >
                <v-btn @click="download" color="primary" class="mx-2"
                  >Export</v-btn
                >
                <v-btn @click="viewDetail" color="primary" class="mx-2"
                  >Show SoW</v-btn
                >
                <v-btn @click="openCreateRowDialog" color="primary" class="mx-2"
                  >Add row</v-btn
                >
                <v-btn @click="handlerNewVersion" color="primary" class="mx-2"
                  >New Version</v-btn
                >
                <v-btn @click="submit" color="primary" class="mx-2">Save</v-btn>
              </v-col>
            </v-row>
          </v-form>
        </v-col>
      </v-row>
      <v-row justify="center">
        <v-col cols="12">
          <!-- TABLE - SOW ROLES -->
          <SowRole :isDisabled="isDisabled"></SowRole>
          <!-- TABLE - SOW ROLES -->
        </v-col>
      </v-row>

      <!-- MODAL - ADD SOW-ROLE -->
      <SowRoleAddDialog ref="dialog"></SowRoleAddDialog>
      <!-- MODAL - ADD SOW-ROLE -->
    </v-container>
  </Layout>
</template>
