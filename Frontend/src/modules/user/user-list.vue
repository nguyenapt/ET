<script>
import { mapActions, mapGetters } from "vuex";
import Layout from "../_layouts/default.layout";
import findIndex from "lodash/findIndex";

export default {
  components: {
    Layout
  },
  data: () => ({
    isLoading: false,
    title: "Search Users",
    headers: [
      {
        text: "ID",
        align: "left",
        value: "id"
      },
      { text: "UserName", value: "userName" },
      { text: "Email", value: "emailAddress" },
      { text: "Active", value: "isActive" },
      { text: "Role", value: "roleNames" },
      {
        text: "Actions",
        value: "actions",
        sortable: false,
        width: 90,
        align: "center"
      }
    ],
    searchObj: {
      keyWord: ""
    },
    // options for sort
    options: {
      page: 1,
      itemsPerPage: 10,
      sortBy: ["id"],
      sortDesc: [false]
    },
    totalCount: 0
  }),
  methods: {
    ...mapActions("user", ["getUserById", "getUsers", "editUser"]),

    resetSearch() {
      this.searchObj = {};
      this.search();
    },
    editItem(item) {
      this.$router.push(`/user/edit/${item.id}`);
    },

    getParams() {
      const { page, itemsPerPage } = this.options;
      return {
        ...this.searchObj,
        SkipCount: (page - 1) * itemsPerPage,
        MaxResultCount: itemsPerPage
      };
    },
    getIndex(item) {
      const index = findIndex(this.users, user => user.id === item.id);

      return (this.options.page - 1) * this.options.itemsPerPage + index + 1;
    },
    async init() {
      this.isLoading = true;
      await this.search();

      this.isLoading = false;
    },
    async search() {
      const params = this.getParams();
      const data = await this.getUsers(params);

      this.totalCount = data.totalCount;
    }
  },
  computed: {
    ...mapGetters("user", ["users"])
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
      async handler() {
        const { page, itemsPerPage } = this.options;
        const { keyWord } = this.searchObj;

        const params = {
          keyWord: keyWord,
          SkipCount: (page - 1) * itemsPerPage,
          MaxResultCount: itemsPerPage
        };

        const data = await this.getUsers(params);
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
        <v-col cols="12" sm="6" class="white">
          <v-form ref="form">
            <v-text-field
              dense
              outlined
              v-model="searchObj.keyWord"
              required
              label="Username"
              class="px-2"
            ></v-text-field>

            <v-row>
              <v-col class="text-right">
                <v-btn @click="search" class="mx-2" color="primary"
                  >Search</v-btn
                >
              </v-col>
            </v-row>
          </v-form>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12">
          <!-- TABLE - SOW -->
          <v-data-table
            :headers="headers"
            :items="users"
            must-sort
            item-key="id"
            :options.sync="options"
            :server-items-length="totalCount"
            disable-sort
          >
            <template v-slot:item.id="{ item }">{{ getIndex(item) }}</template>
            <template v-slot:item.userName="{ item }">{{
              item.userName
            }}</template>

            <template v-slot:item.emailAddress="{ item }">{{
              item.emailAddress
            }}</template>
            <template v-slot:item.isActive="{ item }">
              <v-icon v-if="item.isActive">mdi-check</v-icon>
            </template>
            <template v-slot:item.roleNames="{ item }"
              >{{ item.roleNames.join(", ") }}
            </template>
            <template v-slot:item.actions="{ item }">
              <v-icon class="mr-2" @click="editItem(item)">mdi-pencil</v-icon>
            </template>
          </v-data-table>
          <!-- TABLE - SOW -->
        </v-col>
      </v-row>
    </v-container>
  </Layout>
</template>
