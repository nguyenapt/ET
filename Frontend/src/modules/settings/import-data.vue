<script>
import { mapActions } from "vuex";
import Layout from "../_layouts/default.layout";
import { ApiUrl } from "../../core/api-url";
export default {
  components: {
    Layout
  },
  data() {
    return {
      title: "Import data",
      importFile: null,
      importLink: null,
      importTypes: [
        {
          text: "Holiday bank",
          value: ApiUrl.ImportHolidayBank,
          exampleFile: "/HolidayBank.xlsx"
        },
        {
          text: "Working hour",
          value: ApiUrl.ImportWorkingHour,
          exampleFile: "/WorkingHours.xlsx"
        },
        {
          text: "Rate for role",
          value: ApiUrl.ImportRateForRole,
          exampleFile: "/BillingRate.xlsx"
        },
        {
          text: "Beneficial information",
          value: ApiUrl.ImportBeneficialInformation,
          exampleFile: "/BeneficialInformation.xlsx"
        },
        {
          text: "Resource role",
          value: ApiUrl.ImportResourceRole,
          exampleFile: "/ResourceRole.xlsx"
        },
        {
          text: "Resource",
          value: ApiUrl.ImportResource,
          exampleFile: "/ResourceImport.xlsx"
        }
      ]
    };
  },
  mounted() {},
  computed: {},
  methods: {
    ...mapActions("importdata", ["importDatas"]),
    ...mapActions("layout", ["setSnackbar", "setCommonErrorToast"]),

    download() {
      var option = this.importTypes.filter(o => o.value == this.importLink);
      if (option) {
        console.log(option[0].exampleFile);
        window.open(option[0].exampleFile, "_blank");
      }
    },

    onUpload() {
      if (this.importFile) {
        return this.importDatas({
          link: this.importLink,
          file: this.importFile
        })
          .then(res => {
            if (res != null) {
              if (res.success) {
                this.setSnackbar({ text: res.message });
              } else {
                this.setCommonErrorToast({ text: res.error });
              }
            } else {
              this.setCommonErrorToast();
            }
          })
          .catch(() => {
            this.setCommonErrorToast();
          });
      }
    }
  }
};
</script>

<template>
  <Layout>
    <v-container>
      <v-row justify="center">
        <h4>{{ title }}</h4>
      </v-row>
      <v-form ref="form">
        <v-row justify="center">
          <v-col cols="12" md="6">
            <v-select
              dense
              outlined
              v-model="importLink"
              :items="importTypes"
              item-text="text"
              item-value="value"
              label="Import types"
              class="px-2"
            ></v-select>
            <v-file-input
              v-model="importFile"
              label="Select Csv File..."
            ></v-file-input>
            <v-btn color="primary" @click="download" class="ml-2"
              >Example file</v-btn
            >
            <v-btn color="primary" @click="onUpload" class="ml-2">Import</v-btn>
          </v-col>
        </v-row>
      </v-form>
    </v-container>
  </Layout>
</template>
