<script>
import { mapActions, mapGetters } from "vuex";
import SowRoleEditDialog from "../dialog/sow-role-edit";
import SowRoleDeleteDialog from "../dialog/sow-role-delete";
import find from "lodash/find";
import findIndex from "lodash/findIndex";
import { BLANK, DATE_FORMAT } from "../../../constants";
import { format, parseISO } from "date-fns";

const FULL_HEADERS = [
  { text: "ID", align: "left", value: "id", width: 60, notSelect: true },
  { text: "Bill", value: "isBillable", width: 60 },
  { text: "Bill Type", value: "billingType", width: 90 },
  { text: "Role", value: "roleName", width: 60 },
  { text: "Rate Type", value: "rateType", width: 90 },
  { text: "Currency", value: "currency", width: 90 },
  { text: "Standard Rate", value: "standardRate", width: 100 },
  { text: "Actual Rate", value: "actualRate", width: 100 },
  { text: "FTE", value: "fte", width: 40 },
  { text: "Total Hours", value: "totalHours", align: "center", width: 60 },
  {
    text: "Total Hours/Month",
    value: "totalHoursPerMonth",
    align: "center",
    width: 80
  },
  { text: "Start Date", value: "startDate", width: 120 },
  { text: "End Date", value: "endDate", width: 120 },
  { text: "Created Date", value: "creationTime", width: 120 },
  { text: "Term (days)", value: "term", width: 90 },
  { text: "Billing Note", value: "description", width: 200 },
  {
    text: "Actions",
    value: "actions",
    sortable: false,
    width: 120,
    align: "center",
    notSelect: true
  }
];

export default {
  components: {
    SowRoleEditDialog,
    SowRoleDeleteDialog
  },
  props: {
    isDisabled: {
      type: Boolean,
      default: true
    }
  },
  data: () => ({
    allHeaders: FULL_HEADERS,
    headers: [],
    allSelectedColumns: FULL_HEADERS.filter(item => !item.notSelect),
    selectedColumns: [],
    options: {},
    displayColumns: {}
  }),

  computed: {
    ...mapGetters("sow", ["sowRoles", "selectors"]),
    likesAllColumn() {
      return this.selectedColumns.length === this.allSelectedColumns.length;
    },
    likesSomeColumn() {
      return this.selectedColumns.length > 0 && !this.likesAllColumn;
    },
    icon() {
      if (this.likesAllColumn) return "mdi-close-box";
      if (this.likesSomeColumn) return "mdi-minus-box";
      return "mdi-checkbox-blank-outline";
    }
  },

  methods: {
    ...mapActions("sow", ["createSowRole", "getSowById"]),
    ...mapActions("layout", ["setSnackbar", "setCommonErrorToast"]),
    editItem(sowRole) {
      this.$refs.addSowRoleDialog.openDialog(sowRole);
    },
    deleteItem(sowRole) {
      this.$refs.deleteSowRoleDialog.openDialog({
        sowRole,
        callbackOnClose: () => this.getSowById(this.$route.params.id)
      });
    },
    duplicate(sowRole) {
      this.createSowRole(sowRole)
        .then(() => {
          // reload page
          this.getSowById(this.$route.params.id);

          this.setSnackbar({ text: "Duplicate row successfully" });
        })
        .catch(() => {
          this.setCommonErrorToast();
        });
    },
    getNameFromSelector(types, value) {
      const obj = find(types, type => type.value === value);

      return obj ? obj.name : BLANK;
    },
    getIndex(item) {
      const index = findIndex(this.sowRoles, sowRole => sowRole.id === item.id);

      return (this.options.page - 1) * this.options.itemsPerPage + index + 1;
    },
    updateColums() {
      for (const columnKey in this.displayColumns) {
        if (this.displayColumns.hasOwnProperty(columnKey)) {
          this.displayColumns[columnKey] =
            this.selectedColumns.indexOf(columnKey) >= 0;
        }
      }

      this.headers = this.allHeaders.filter(item => {
        if (item.notSelect) {
          return true;
        }

        if (this.selectedColumns.indexOf(item.value) >= 0) {
          return true;
        }

        return false;
      });
    },
    toggleCheckAll() {
      this.$nextTick(() => {
        if (this.likesAllColumn) {
          this.selectedColumns = [];
        } else {
          this.selectedColumns = this.allSelectedColumns.map(
            item => item.value
          );
        }
      });
    }
  },

  filters: {
    formatDate(value) {
      return value ? format(parseISO(value), DATE_FORMAT) : BLANK;
    }
  },

  created() {
    // init displayColumns
    this.allHeaders.forEach(item => (this.displayColumns[item.value] = true));
    // init headers
    this.headers = this.allHeaders.filter(() => true);
  }
};
</script>

<template>
  <div>
    <SowRoleEditDialog ref="addSowRoleDialog"></SowRoleEditDialog>
    <SowRoleDeleteDialog ref="deleteSowRoleDialog"></SowRoleDeleteDialog>
    <v-data-table
      :headers="headers"
      :items="sowRoles"
      item-key="id"
      :options.sync="options"
      disable-sort
      calculate-widths
    >
      <!-- SELECTED COLUMNS -->
      <template v-slot:top>
        <v-row justify="end">
          <v-col cols="12" md="2">
            <v-autocomplete
              dense
              outlined
              multiple
              hide-details
              v-model="selectedColumns"
              :items="allSelectedColumns"
              item-text="text"
              item-value="value"
              label="Selected columns"
            >
              <template v-slot:selection="{ item, index }">
                <span v-if="index === 0">
                  <span>{{ item.text }}</span>
                </span>
                <span v-if="index === 1" class="grey--text caption"
                  >(+{{ selectedColumns.length - 1 }} others)</span
                >
              </template>
              <template v-slot:prepend-item>
                <v-list-item ripple @click="toggleCheckAll">
                  <v-list-item-action>
                    <v-icon
                      :color="
                        selectedColumns.length > 0 ? 'indigo darken-4' : ''
                      "
                      >{{ icon }}</v-icon
                    >
                  </v-list-item-action>
                  <v-list-item-content>
                    <v-list-item-title>Select All</v-list-item-title>
                  </v-list-item-content>
                </v-list-item>
                <v-divider class="mt-2"></v-divider>
              </template>
            </v-autocomplete>
          </v-col>
          <v-col cols="12" md="1">
            <v-btn @click="updateColums" color="primary">Apply</v-btn>
          </v-col>
        </v-row>
      </template>
      <!-- SELECTED COLUMNS -->

      <!-- BODY TABLE -->
      <template v-slot:body="{ items }">
        <tbody>
          <tr v-for="item in items" :key="item.id">
            <td>{{ getIndex(item) }}</td>
            <td v-if="displayColumns['isBillable']">
              <v-icon v-if="item.isBillable">mdi-check</v-icon>
              <v-icon v-else>mdi-close</v-icon>
            </td>
            <td v-if="displayColumns['billingType']">
              {{ getNameFromSelector(selectors.BillingType, item.billingType) }}
            </td>
            <td v-if="displayColumns['roleName']">
              {{ getNameFromSelector(selectors.ResourceRole, item.roleName) }}
            </td>
            <td v-if="displayColumns['rateType']">
              {{ getNameFromSelector(selectors.RateType, item.rateType) }}
            </td>

            <td v-if="displayColumns['currency']">
              {{ getNameFromSelector(selectors.Currency, item.currency) }}
            </td>
            <td v-if="displayColumns['standardRate']">
              {{ item.standardRate }}
            </td>
            <td v-if="displayColumns['actualRate']">{{ item.actualRate }}</td>
            <td v-if="displayColumns['fte']">{{ item.fte }}</td>
            <td v-if="displayColumns['totalHours']" class="text-center">
              {{ item.totalHours }}
            </td>
            <td v-if="displayColumns['totalHoursPerMonth']" class="text-center">
              {{ item.totalHoursPerMonth }}
            </td>
            <td v-if="displayColumns['startDate']">
              {{ item.startDate | formatDate }}
            </td>
            <td v-if="displayColumns['endDate']">
              {{ item.endDate | formatDate }}
            </td>
            <td v-if="displayColumns['creationTime']">
              {{ item.creationTime | formatDate }}
            </td>
            <td v-if="displayColumns['term']">{{ item.term }}</td>
            <td v-if="displayColumns['description']">{{ item.description }}</td>

            <td class="text-center">
              <v-icon
                class="mr-2"
                @click="editItem(item)"
                :disabled="isDisabled"
                >mdi-pencil</v-icon
              >
              <v-icon
                class="mr-2"
                @click="duplicate(item)"
                :disabled="isDisabled"
                >mdi-file-multiple</v-icon
              >
              <v-icon @click="deleteItem(item)" :disabled="isDisabled"
                >mdi-delete</v-icon
              >
            </td>
          </tr>
        </tbody>
      </template>
      <!-- BODY TABLE -->
    </v-data-table>
  </div>
</template>
<style lang="scss" scoped>
.v-icon:hover {
  opacity: 0.6;
}
</style>
