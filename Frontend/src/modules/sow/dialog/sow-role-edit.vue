<script>
import { mapActions, mapGetters } from "vuex";
import { format } from "date-fns";
import {
  BLANK,
  DATE_FORMAT,
  DATE_PARSE,
  FIELD_NAME,
  VALIDATE_SOW_ROLE,
  VALIDATE_STATUS,
  VALIDATE_RATE_TYPE
} from "../../../constants";

export default {
  data() {
    return {
      startDateFormatted: BLANK,
      endDateFormatted: BLANK,
      isLoading: false,
      showDialog: false,
      showStart: false,
      showEnd: false,
      sowRole: {
        standardRate: 0
      },
      rules: {
        billingType: [v => !!v || "Bill Type is required!"],
        roleName: [v => !!v || "Role is required!"],
        rateType: [v => !!v || "Rate Type is required!"],
        currency: [v => !!v || "Currency is required!"],
        actual: [v => !!v || "Actual is required!"],
        fte: [],
        startDate: [v => !!v || "Start date is required!"],
        term: [],
        endDate: [],
        totalHours: [],
        totalHoursPerMonth: []
      },
      disabled: {}
    };
  },
  computed: {
    ...mapGetters("sow", ["selectors"]),
    dateRules() {
      const rules = [];

      rules.push(() => {
        const { startDate, endDate } = this.sowRole;

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
    ...mapActions("sow", ["updateSowRole", "getAllBillingRate"]),
    ...mapActions("layout", ["setSnackbar", "setCommonErrorToast"]),
    openDialog(item) {
      this.sowRole = { ...item };

      (this.startDateFormatted = this.formatDate(this.sowRole.startDate)),
        (this.endDateFormatted = this.formatDate(this.sowRole.endDate)),
        (this.showDialog = true);
    },
    close() {
      this.sowRole = {};
      this.showDialog = false;
    },
    save() {
      const isValidForm = this.$refs.form.validate();

      if (isValidForm) {
        this.updateSowRole(this.sowRole)
          .then(() => {
            this.close();
            this.setSnackbar({ text: "Updated successfully!" });
          })
          .catch(err => {
            let text = BLANK;

            if (err.validationErrors && err.validationErrors.length) {
              err.validationErrors.forEach((item, index) => {
                text += index === 0 ? item.message : `; ${item.message}`;
              });

              this.setSnackbar({ text, color: "error", timeout: 5000 });
            } else {
              this.setCommonErrorToast();
            }
          });
      }
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
    },
    getBillingRate() {
      const { billingType, rateType, roleName, currency } = this.sowRole;

      if (!billingType || !rateType || !roleName || !currency) {
        return;
      }

      this.getAllBillingRate({
        billingType,
        rateType,
        roleName,
        currency
      }).then(res => {
        if (res.items && res.items[0] && res.items[0].value) {
          this.sowRole.standardRate = res.items[0].value;
        } else {
          this.sowRole.standardRate = 0;
        }
      });
    }
  },
  watch: {
    "sowRole.billingType": function(val) {
      const rule = VALIDATE_SOW_ROLE[val];
      this.sowRole.rateType = VALIDATE_RATE_TYPE[val];

      // reset rules and disabled
      this.disabled = {};
      this.rules.fte = [];
      this.rules.term = [];
      this.rules.endDate = [];
      this.rules.totalHours = [];
      this.rules.totalHoursPerMonth = [];
      if (this.$refs.form) {
        this.$refs.form.resetValidation();
      }

      for (const key in rule) {
        if (rule.hasOwnProperty(key)) {
          switch (rule[key]) {
            case VALIDATE_STATUS.REQUIRED:
              this.rules[key] = [v => !!v || `${FIELD_NAME[key]} is required!`];
              break;
            case VALIDATE_STATUS.DISABLED:
              this.sowRole[key] = null;
              this.disabled[key] = true;
              break;
          }
        }
      }

      this.getBillingRate();
    },
    "sowRole.roleName": function() {
      this.getBillingRate();
    },
    "sowRole.currency": function() {
      this.getBillingRate();
    }
  }
};
</script>
<template>
  <v-dialog :value="showDialog" @click:outside="close" max-width="600px">
    <v-card>
      <v-form ref="form">
        <v-card-title primary-title class="headline grey lighten-2">
          Edit sow role
        </v-card-title>

        <v-divider></v-divider>

        <v-container>
          <v-row no-gutters>
            <v-col cols="12" md="6">
              <v-autocomplete
                dense
                outlined
                v-model="sowRole.billingType"
                :items="selectors.BillingType"
                item-text="name"
                item-value="value"
                label="Bill Type"
                class="px-2"
              ></v-autocomplete>
            </v-col>
            <v-col cols="12" md="6">
              <v-autocomplete
                dense
                outlined
                v-model="sowRole.roleName"
                :items="selectors.ResourceRole"
                :rules="rules.roleName"
                item-text="name"
                item-value="value"
                label="Role"
                class="px-2"
              ></v-autocomplete>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                :readonly="true"
                dense
                outlined
                v-model="sowRole.rateType"
                label="Rate Type"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-autocomplete
                dense
                outlined
                v-model="sowRole.currency"
                :items="selectors.Currency"
                :rules="rules.currency"
                item-text="name"
                item-value="value"
                label="Currency"
                class="px-2"
              ></v-autocomplete>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                :readonly="true"
                v-model="sowRole.standardRate"
                label="Standard Rate"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="sowRole.actualRate"
                :rules="rules.actual"
                required
                type="number"
                label="Actual Rate"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="sowRole.totalHours"
                :disabled="disabled.totalHours"
                :rules="rules.totalHours"
                label="Total Hours"
                type="number"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="sowRole.totalHoursPerMonth"
                :rules="rules.totalHoursPerMonth"
                :disabled="disabled.totalHoursPerMonth"
                label="Total Hours/Month"
                type="number"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="sowRole.fte"
                :rules="rules.fte"
                label="FTE"
                type="number"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="sowRole.term"
                :rules="rules.term"
                :disabled="disabled.term"
                label="Term (days)"
                type="number"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-menu
                v-model="showStart"
                :close-on-content-click="false"
                transition="scale-transition"
                offset-y
                min-width="290px"
              >
                <template v-slot:activator="{ on }">
                  <v-text-field
                    dense
                    outlined
                    v-model="startDateFormatted"
                    :rules="[...rules.startDate, ...dateRules]"
                    label="Start Date"
                    placeholder="dd/mm/yyyy"
                    clearable
                    @blur="sowRole.startDate = parseDate(startDateFormatted)"
                    v-on="on"
                    class="px-2"
                  ></v-text-field>
                </template>
                <v-date-picker
                  v-model="sowRole.startDate"
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
                    v-model="endDateFormatted"
                    :rules="[...rules.endDate, ...dateRules]"
                    :disabled="disabled.endDate"
                    label="End Date"
                    placeholder="dd/mm/yyyy"
                    clearable
                    v-on="on"
                    class="px-2"
                    @blur="sowRole.endDate = parseDate(endDateFormatted)"
                  ></v-text-field>
                </template>
                <v-date-picker
                  v-model="sowRole.endDate"
                  @change="pickEndDate"
                  no-title
                ></v-date-picker>
              </v-menu>
            </v-col>

            <v-col cols="12" md="12">
              <v-textarea
                outlined
                v-model="sowRole.description"
                label="Billing Note"
                :rows="3"
                class="px-2"
              ></v-textarea>
            </v-col>

            <v-col cols="12" md="12">
              <v-checkbox
                v-model="sowRole.isBillable"
                label="Bill"
                class="px-2"
              ></v-checkbox>
            </v-col>
          </v-row>
        </v-container>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="close">Cancel</v-btn>
          <v-btn color="primary" @click="save">Save</v-btn>
        </v-card-actions>
      </v-form>
    </v-card>
  </v-dialog>
</template>
