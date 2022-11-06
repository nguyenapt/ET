<script>
import { mapActions } from "vuex";
import { BLANK, DATE_FORMAT } from "../../../constants";
import { format, parseISO } from "date-fns";

const HEADERS = [
  { text: "ID", align: "left", value: "id", width: 60, notSelect: true },
  { text: "Bill", value: "isBillable", width: 60 },
  { text: "Bill Type", value: "billingType", width: 90 },
  { text: "Role", value: "roleName", width: 60 },
  { text: "Rate Type", value: "rateType", width: 90 },
  { text: "Currency", value: "currency", width: 90 },
  { text: "Standard Rate", value: "standardRate", width: 90 },
  { text: "Actual Rate", value: "actualRate", width: 120 },
  { text: "FTE", value: "fte", width: 60 },
  { text: "Total Hours", value: "totalHours", width: 120 },
  { text: "Total Hours/Month", value: "totalHoursPerMonth", width: 150 },
  { text: "Start Date", value: "startDate", width: 120, align: "center" },
  { text: "End Date", value: "endDate", width: 120 },
  { text: "USD", value: "", width: 90 },
  { text: "SEK", value: "", width: 90 },
  { text: "AUD", value: "", width: 90 },
  { text: "USD", value: "", width: 90 },
  { text: "SEK", value: "", width: 90 },
  { text: "AUD", value: "", width: 90 },
  { text: "USD", value: "", width: 90 },
  { text: "SEK", value: "", width: 90 },
  { text: "AUD", value: "", width: 90 },
  { text: "USD", value: "", width: 90 },
  { text: "SEK", value: "", width: 90 },
  { text: "AUD", value: "", width: 90 },
  { text: "Term (days)", value: "term", width: 90 },
  { text: "Billing Note", value: "description", width: 200 }
];

export default {
  data: () => ({
    isLoading: false,
    headers: HEADERS,
    sowRoles: [],
    totals: {}
  }),

  methods: {
    ...mapActions("sow", ["getSowById", "getSowRoleDetailListById"]),
    ...mapActions("layout", ["setCommonErrorToast"]),
    async init() {
      this.isLoading = true;

      const result = await this.getSowRoleDetailListById(this.$route.params.id);

      this.sowRoles = result.items;
      this.totals = result.totals;
      this.isLoading = false;
    },
    getFeeByCurrency(currency, fees) {
      let fee;

      fees.forEach(item => {
        if (item.currency === currency) {
          fee = item.fee;
        }
      });

      return fee ? fee : BLANK;
    }
  },

  filters: {
    formatDate(value) {
      return value ? format(parseISO(value), DATE_FORMAT) : BLANK;
    }
  },

  created() {
    try {
      this.init();
    } catch (e) {
      console.error(e);
      this.setCommonErrorToast();
    }
  }
};
</script>

<template>
  <div>
    <v-skeleton-loader
      v-if="isLoading"
      ref="skeleton"
      type="table"
      class="mx-auto"
    ></v-skeleton-loader>
    <v-data-table
      v-else
      :headers="headers"
      :items="sowRoles"
      calculate-widths
      disable-sort
      :items-per-page="-1"
      hide-default-footer
    >
      <!-- HEADER CUSTOM -->
      <template v-slot:header>
        <thead>
          <tr>
            <th colspan="13"></th>
            <th colspan="6" class="text-center indigo--text">FIX FEE</th>
            <th colspan="6" class="text-center indigo--text">MONTHLY FEE</th>
            <th colspan="2"></th>
          </tr>
          <tr>
            <th colspan="9"></th>
            <th colspan="2" class="text-center">BUDGET</th>
            <th colspan="2"></th>
            <th colspan="3" class="text-center indigo--text">RC FEE</th>
            <th colspan="3" class="text-center indigo--text">FC FEE</th>
            <th colspan="3" class="text-center indigo--text">RATE CARD FEE</th>
            <th colspan="3" class="text-center indigo--text">FORECAST FEE</th>
            <th colspan="2"></th>
          </tr>
        </thead>
      </template>
      <!-- HEADER CUSTOM -->

      <!-- BODY CUSTOM -->
      <template v-slot:body="{ items }">
        <tbody>
          <tr v-for="(item, index) in items" :key="item.id">
            <td>{{ index + 1 }}</td>
            <td>
              <v-icon v-if="item.isBillable">mdi-check</v-icon>
              <v-icon v-else>mdi-close</v-icon>
            </td>
            <td>{{ item.billingType }}</td>
            <td>{{ item.roleName }}</td>
            <td>{{ item.rateType }}</td>
            <td>{{ item.currency }}</td>
            <td>{{ item.standardRate }}</td>
            <td>{{ item.actualRate }}</td>
            <td>{{ item.fte }}</td>
            <td>{{ item.totalHours }}</td>
            <td>{{ item.totalHoursPerMonth }}</td>
            <td>{{ item.startDate | formatDate }}</td>
            <td>{{ item.endDate | formatDate }}</td>

            <td>{{ getFeeByCurrency("USD", item.fixRateCardFee) }}</td>
            <td>{{ getFeeByCurrency("SEK", item.fixRateCardFee) }}</td>
            <td>{{ getFeeByCurrency("AUD", item.fixRateCardFee) }}</td>

            <td>{{ getFeeByCurrency("USD", item.fixForcastFee) }}</td>
            <td>{{ getFeeByCurrency("SEK", item.fixForcastFee) }}</td>
            <td>{{ getFeeByCurrency("AUD", item.fixForcastFee) }}</td>

            <td>{{ getFeeByCurrency("USD", item.monthlyRateCardFee) }}</td>
            <td>{{ getFeeByCurrency("SEK", item.monthlyRateCardFee) }}</td>
            <td>{{ getFeeByCurrency("AUD", item.monthlyRateCardFee) }}</td>

            <td>{{ getFeeByCurrency("USD", item.monthlyForcastFee) }}</td>
            <td>{{ getFeeByCurrency("SEK", item.monthlyForcastFee) }}</td>
            <td>{{ getFeeByCurrency("AUD", item.monthlyForcastFee) }}</td>

            <td>{{ item.term }}</td>
            <td>{{ item.description }}</td>
          </tr>
          <!-- BODY APPEND -->
          <tr>
            <td
              colspan="13"
              class="text-right font-weight-bold caption indigo--text"
            >
              TOTAL
            </td>
            <td class="font-weight-bold caption indigo--text">
              {{ getFeeByCurrency("USD", totals.fixRateCardFee) }}
            </td>
            <td class="font-weight-bold caption indigo--text">
              {{ getFeeByCurrency("SEK", totals.fixRateCardFee) }}
            </td>
            <td class="font-weight-bold caption indigo--text">
              {{ getFeeByCurrency("AUD", totals.fixRateCardFee) }}
            </td>

            <td class="font-weight-bold caption indigo--text">
              {{ getFeeByCurrency("USD", totals.fixForcastFee) }}
            </td>
            <td class="font-weight-bold caption indigo--text">
              {{ getFeeByCurrency("SEK", totals.fixForcastFee) }}
            </td>
            <td class="font-weight-bold caption indigo--text">
              {{ getFeeByCurrency("AUD", totals.fixForcastFee) }}
            </td>

            <td class="font-weight-bold caption indigo--text">
              {{ getFeeByCurrency("USD", totals.monthlyRateCardFee) }}
            </td>
            <td class="font-weight-bold caption indigo--text">
              {{ getFeeByCurrency("SEK", totals.monthlyRateCardFee) }}
            </td>
            <td class="font-weight-bold caption indigo--text">
              {{ getFeeByCurrency("AUD", totals.monthlyRateCardFee) }}
            </td>

            <td class="font-weight-bold caption indigo--text">
              {{ getFeeByCurrency("USD", totals.monthlyForcastFee) }}
            </td>
            <td class="font-weight-bold caption indigo--text">
              {{ getFeeByCurrency("SEK", totals.monthlyForcastFee) }}
            </td>
            <td class="font-weight-bold caption indigo--text">
              {{ getFeeByCurrency("AUD", totals.monthlyForcastFee) }}
            </td>
          </tr>
          <!-- BODY APPEND -->
        </tbody>
      </template>
      <!-- BODY CUSTOM -->
    </v-data-table>
  </div>
</template>
<style lang="scss" scoped>
.v-icon:hover {
  opacity: 0.6;
}
</style>
