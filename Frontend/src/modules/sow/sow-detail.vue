<script>
import Layout from "../_layouts/default.layout";
import SowRoleDetail from "./table/sow-role-detail";
import { format } from "date-fns";
import { mapActions, mapGetters } from "vuex";
import { DATE_FORMAT, SPACE, BLANK } from "../../constants";

export default {
  components: {
    Layout,
    SowRoleDetail
  },
  data: () => ({
    isLoading: false,
    title: "Sow Detail Page"
  }),
  computed: {
    ...mapGetters("sow", ["sow"])
  },
  methods: {
    ...mapActions("sow", ["getSowById", "dowloadSow"]),
    ...mapActions("layout", ["setCommonErrorToast"]),
    init() {
      this.isLoading = true;

      this.getSowById(this.$route.params.id)
        .then(() => {
          this.isLoading = false;
        })
        .catch(err => {
          console.error(err);
          this.setCommonErrorToast();
        });
    },
    formatDate(date) {
      if (!date) return null;

      return format(new Date(date), DATE_FORMAT);
    },
    backToList() {
      this.$router.push("/sow");
    },
    download() {
      this.dowloadSow(this.sow);
    }
  },
  filters: {
    getFullName(kamResource) {
      if (!kamResource || (!kamResource.firstName && !kamResource.lastName)) {
        return BLANK;
      }

      return kamResource.firstName + SPACE + kamResource.lastName;
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
                <v-text-field
                  readonly
                  dense
                  outlined
                  :value="sow.client && sow.client.name"
                  label="Client Name"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="3">
                <v-text-field
                  readonly
                  dense
                  outlined
                  :value="sow.department && sow.department.name"
                  label="Division"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="3">
                <v-text-field
                  readonly
                  dense
                  outlined
                  :value="formatDate(sow.startDate)"
                  label="Start Date"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="3">
                <v-text-field
                  readonly
                  dense
                  outlined
                  :value="formatDate(sow.endDate)"
                  label="End Date"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="3">
                <v-text-field
                  readonly
                  dense
                  outlined
                  :value="sow.fileUrl"
                  label="File URL"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="3">
                <v-text-field
                  readonly
                  dense
                  outlined
                  :value="sow.status"
                  label="Status"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="3">
                <v-text-field
                  readonly
                  dense
                  outlined
                  :value="sow.kamResource | getFullName"
                  label="KAM name"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="3">
                <v-text-field
                  readonly
                  dense
                  outlined
                  :value="sow.clientPONumber"
                  required
                  label="Client PO Number"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-text-field
                  readonly
                  dense
                  outlined
                  :value="sow.project && sow.project.name"
                  label="Project"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-text-field
                  readonly
                  dense
                  outlined
                  :value="sow.name"
                  label="SoW Name"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-text-field
                  readonly
                  dense
                  outlined
                  :value="sow.sowCode"
                  label="SoW Code"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-text-field
                  readonly
                  dense
                  outlined
                  :value="sow.version"
                  label="Version"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-textarea
                  readonly
                  outlined
                  :value="sow.description"
                  label="Input your sow note here"
                  :rows="3"
                  class="px-2"
                ></v-textarea>
              </v-col>
            </v-row>
          </v-form>
        </v-col>
      </v-row>
      <v-row justify="center">
        <v-col cols="12">
          <!-- TABLE - SOW ROLES DETAIL -->
          <SowRoleDetail></SowRoleDetail>
          <!-- TABLE - SOW ROLES DETAIL -->
        </v-col>
        <v-col cols="12" class="text-right">
          <v-btn @click="backToList" color="primary" class="mx-2"
            >Back to list</v-btn
          >
          <v-btn @click="download" color="primary" class="mx-2">Export</v-btn>
        </v-col>
      </v-row>
    </v-container>
  </Layout>
</template>
