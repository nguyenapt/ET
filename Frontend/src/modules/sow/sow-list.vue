<script>
import { mapActions, mapGetters } from "vuex";
import Layout from "../_layouts/default.layout";
import SowAdd from "./dialog/sow-add";
import SowDeleteDialog from "./dialog/sow-delete";
import { format } from "date-fns";
import { BLANK, COMMA, DATE_FORMAT, DATE_PARSE, STATUS } from "../../constants";
import find from "lodash/find";
import findIndex from "lodash/findIndex";

const REQUEST_BODY = [
  "Client",
  "Department",
  "Project",
  "Resource",
  "Beneficiary",
  "BillingType",
  "ResourceRole",
  "RateType",
  "Currency"
];

const HEADERS_COLUMNS = [
  {
    text: "ID",
    value: "id"
  },
  { text: "Client", value: "clientId" },
  { text: "Project", value: "projectId" },
  { text: "SoW Name", value: "name" },
  { text: "Status", value: "status" },
  { text: "SoW Code", value: "sowCode" },
  { text: "Version", value: "version" },
  { text: "Start Date", value: "startDate" },
  { text: "End Date", value: "endDate" },
  {
    text: "Created Date",
    value: "creationTime"
  },
  {
    text: "Actions",
    value: "actions",
    align: "center",
    width: 120
  }
];

const STATUSES = [
  STATUS.DRAFT,
  STATUS.OPEN,
  STATUS.CONFIRMED,
  STATUS.REJECTED,
  STATUS.SIGNED,
  STATUS.CLOSED
];

export default {
  components: {
    Layout,
    SowAdd,
    SowDeleteDialog
  },
  data: () => ({
    isLoading: false,
    title: "Sow Page",
    startDateFormatted: BLANK,
    endDateFormatted: BLANK,
    headers: HEADERS_COLUMNS,
    searchObj: {},
    showStart: false,
    showEnd: false,
    options: {
      page: 1,
      itemsPerPage: 10,
      sortBy: ["creationTime"],
      sortDesc: [true]
    },
    totalCount: 0,
    statuses: STATUSES
  }),
  methods: {
    ...mapActions("sow", ["getSelectors", "getSows"]),
    ...mapActions("layout", ["setCommonErrorToast"]),
    openCreateSowDialog() {
      this.$refs.dialog.open({
        callbackOnClose: this.search
      });
    },
    resetSearch() {
      this.searchObj = {};
      this.search();
    },
    editItem(sow) {
      this.$router.push(`/sow/edit/${sow.id}`);
    },
    viewDetail(sow) {
      this.$router.push(`/sow/${sow.id}`);
    },
    deleteItem(sow) {
      this.$refs.deleteSowDialog.openDialog({
        sow,
        callbackOnClose: this.search
      });
    },
    getSortBy() {
      return `${this.sort.by}${COMMA}${this.sort.desc ? "desc" : "asc"}`;
    },
    getNameFromSelector(types, value) {
      const obj = find(types, type => type.value === value);

      return obj ? obj.name : BLANK;
    },

    getParams() {
      const { sortBy, sortDesc, page, itemsPerPage } = this.options;

      const SortBy = `${sortBy[0]},${sortDesc[0] ? "desc" : "asc"}`;

      return {
        ...this.searchObj,
        CurrentPage: page,
        PageSize: itemsPerPage,
        SortBy
      };
    },
    getIndex(item) {
      const index = findIndex(this.sows, sow => sow.id === item.id);

      return (this.options.page - 1) * this.options.itemsPerPage + index + 1;
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
    init() {
      this.isLoading = true;

      Promise.all([this.getSelectors(REQUEST_BODY), this.search()])
        .then(() => {
          this.isLoading = false;
        })
        .catch(() => {
          this.setCommonErrorToast();
        });
    },
    isDisabled(status) {
      return status !== "Draft";
    },
    async search() {
      const params = this.getParams();
      const data = await this.getSows(params);

      this.totalCount = data.totalCount;
    }
  },
  computed: {
    ...mapGetters("sow", ["selectors", "sows"])
  },
  filters: {
    formatDate(value) {
      return value ? format(new Date(value), DATE_FORMAT) : BLANK;
    }
  },
  created() {
    this.init();
  },
  watch: {
    options: {
      async handler() {
        const { sortBy, sortDesc, page, itemsPerPage } = this.options;

        const SortBy = `${sortBy[0]},${sortDesc[0] ? "desc" : "asc"}`;

        const params = {
          ...this.searchObj,
          CurrentPage: page,
          PageSize: itemsPerPage,
          SortBy
        };

        const data = await this.getSows(params);
        this.totalCount = data.totalCount;
      },
      deep: true
    }
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
    <v-container v-if="!isLoading">
      <v-row justify="center">
        <h4>{{ title }}</h4>
      </v-row>
      <v-row justify="center">
        <v-col cols="12" class="white">
          <v-form ref="form">
            <v-row no-gutters>
              <v-col cols="12" sm="3">
                <v-autocomplete
                  dense
                  outlined
                  clearable
                  v-model="searchObj.ClientId"
                  :items="selectors.Client"
                  item-text="name"
                  item-value="value"
                  label="Client Name"
                  class="px-2"
                ></v-autocomplete>
              </v-col>
              <v-col cols="12" sm="3">
                <v-autocomplete
                  dense
                  outlined
                  clearable
                  v-model="searchObj.DepartmentId"
                  :items="selectors.Department"
                  item-text="name"
                  item-value="value"
                  label="Division"
                  class="px-2"
                ></v-autocomplete>
              </v-col>
              <v-col cols="12" sm="3">
                <v-autocomplete
                  dense
                  outlined
                  clearable
                  v-model="searchObj.Status"
                  :items="statuses"
                  label="Status"
                  class="px-2"
                ></v-autocomplete>
              </v-col>
              <v-col cols="12" sm="3">
                <v-autocomplete
                  dense
                  outlined
                  clearable
                  v-model="searchObj.KamResourceId"
                  :items="selectors.Resource"
                  item-text="name"
                  item-value="value"
                  label="KAM name"
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
                      dense
                      outlined
                      clearable
                      label="Start Date"
                      placeholder="dd/mm/yyyy"
                      class="px-2"
                      v-on="on"
                      v-model="startDateFormatted"
                      @blur="
                        searchObj.StartDate = parseDate(startDateFormatted)
                      "
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    v-model="searchObj.StartDate"
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
                      dense
                      outlined
                      v-model="endDateFormatted"
                      clearable
                      label="End Date"
                      placeholder="dd/mm/yyyy"
                      @blur="searchObj.EndDate = parseDate(endDateFormatted)"
                      v-on="on"
                      class="px-2"
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    v-model="searchObj.EndDate"
                    @change="pickEndDate"
                    no-title
                  ></v-date-picker>
                </v-menu>
              </v-col>
              <v-col cols="12" sm="3">
                <v-text-field
                  dense
                  outlined
                  clearable
                  v-model="searchObj.SowCode"
                  label="SoW Code"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="3">
                <v-text-field
                  dense
                  outlined
                  clearable
                  v-model="searchObj.Version"
                  label="Version"
                  class="px-2"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-autocomplete
                  dense
                  outlined
                  clearable
                  v-model="searchObj.ProjectId"
                  :items="selectors.Project"
                  item-text="name"
                  item-value="value"
                  label="Project"
                  class="px-2"
                ></v-autocomplete>
              </v-col>
              <v-col cols="12" sm="6">
                <v-text-field
                  dense
                  outlined
                  clearable
                  v-model="searchObj.name"
                  required
                  label="SoW Name"
                  class="px-2"
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col class="text-right">
                <v-btn @click="search" class="mx-2" color="primary"
                  >Search</v-btn
                >
                <v-btn @click="resetSearch" class="mx-2" color="primary"
                  >Reset</v-btn
                >
                <v-btn @click="openCreateSowDialog" class="mx-2" color="primary"
                  >Create sow</v-btn
                >
              </v-col>
            </v-row>
          </v-form>
        </v-col>
      </v-row>
      <v-row justify="center">
        <v-col cols="12">
          <!-- TABLE - SOW -->
          <SowDeleteDialog ref="deleteSowDialog"></SowDeleteDialog>

          <v-data-table
            :headers="headers"
            :items="sows"
            item-key="id"
            :options.sync="options"
            :server-items-length="totalCount"
            disable-sort
          >
            <template v-slot:item.id="{ item }">{{ getIndex(item) }}</template>
            <template v-slot:item.clientId="{ item }">
              {{ getNameFromSelector(selectors.Client, item.clientId) }}
            </template>
            <template v-slot:item.projectId="{ item }">
              {{ getNameFromSelector(selectors.Project, item.projectId) }}
            </template>
            <template v-slot:item.startDate="{ item }">
              {{ item.startDate | formatDate }}
            </template>
            <template v-slot:item.endDate="{ item }">
              {{ item.endDate | formatDate }}
            </template>
            <template v-slot:item.creationTime="{ item }">
              {{ item.creationTime | formatDate }}
            </template>
            <template v-slot:item.actions="{ item }">
              <v-icon class="mr-2" @click="viewDetail(item)"
                >mdi-clipboard-text-outline</v-icon
              >
              <v-icon class="mr-2" @click="editItem(item)">mdi-pencil</v-icon>
              <v-icon
                @click="deleteItem(item)"
                :disabled="isDisabled(item.status)"
                >mdi-delete</v-icon
              >
            </template>
          </v-data-table>
          <!-- TABLE - SOW -->
        </v-col>
      </v-row>

      <SowAdd ref="dialog"></SowAdd>
    </v-container>
  </Layout>
</template>
<style lang="scss" scoped>
.v-icon:hover {
  opacity: 0.6;
}
</style>
