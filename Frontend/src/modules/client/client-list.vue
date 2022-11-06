<script>
import Layout from "../_layouts/default.layout";
import ClientAdd from "./dialog/client-add";
import ClientDeleteDialog from "./dialog/client-delete";
import ClientEditDialog from "./dialog/client-edit";
import findIndex from "lodash/findIndex";
import { mapActions, mapGetters } from "vuex";

export default {
  components: {
    Layout,
    ClientAdd,
    ClientEditDialog,
    ClientDeleteDialog
  },
  data() {
    return {
      isLoading: false,
      title: "Client Page",
      callbackOnClose: null,
      headers: [
        {
          text: "Client ID",
          align: "left",
          value: "id"
        },
        { text: "Client Name", value: "name" },
        { text: "Email", value: "email" },
        { text: "Website", value: "website" },
        { text: "Client Address", value: "address" },
        { text: "Client Description", value: "description" },
        {
          text: "Actions",
          value: "actions",
          sortable: false,
          width: 90,
          align: "center"
        }
      ],
      // options for sort
      options: {
        page: 1,
        itemsPerPage: 10,
        sortBy: ["id"],
        sortDesc: [false]
      },
      rowsPerPageItems: [20, 50, 100, -1],
      pagination: {
        rowsPerPage: 50
      },
      totalCount: 0,
      initClients: {}
    };
  },
  methods: {
    ...mapActions("client", ["getClients"]),
    async init() {
      this.isLoading = true;

      await this.getClients();

      this.isLoading = false;
    },
    getIndex(item) {
      const index = findIndex(this.clients, client => client.id === item.id);
      return index + 1;
    },
    openCreateClientDialog() {
      this.$refs.dialog.open({
        callbackOnClose: this.init
      });
    },
    editItem(item) {
      this.$refs.editClientDialog.openDialog({
        item,
        callbackOnClose: this.init
      });
    },
    deleteItem(item) {
      this.$refs.deleteClientDialog.openDialog({
        item,
        callbackOnClose: this.init
      });
    }
  },
  computed: {
    ...mapGetters("client", ["clients"])
  },
  created() {
    try {
      this.init();
    } catch (e) {
      console.error(e);
    }
  },
  watch: {
    options: {
      handler() {
        this.getClients();
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
        <h4>Client Page</h4>
      </v-row>
      <v-row justify="center">
        <v-col cols="12" class="text-right">
          <v-btn @click="openCreateClientDialog" color="primary"
            >Create Client</v-btn
          >
        </v-col>
      </v-row>
      <v-row justify="center">
        <v-col cols="12">
          <!-- TABLE - Client -->
          <ClientDeleteDialog ref="deleteClientDialog"></ClientDeleteDialog>
          <ClientEditDialog ref="editClientDialog"></ClientEditDialog>

          <v-data-table
            :headers="headers"
            :items="clients"
            :items-per-page="pagination.rowsPerPage"
            :footer-props="{
              'items-per-page-options': rowsPerPageItems
            }"
            class="elevation-1"
          >
            <template v-slot:item.id="{ item }">{{ getIndex(item) }}</template>
            <template v-slot:item.actions="{ item }">
              <v-icon class="mr-2" @click="editItem(item)">mdi-pencil</v-icon>
              <v-icon @click="deleteItem(item)">mdi-delete</v-icon>
            </template>
          </v-data-table>
          <!-- TABLE - Client -->
        </v-col>
      </v-row>
      <ClientAdd ref="dialog"></ClientAdd>
    </v-container>
  </Layout>
</template>
