<script>
import Layout from "../_layouts/default.layout";
import ProjectAdd from "./dialog/project-add";
import ProjectDeleteDialog from "./dialog/project-delete";
import ProjectEditDialog from "./dialog/project-edit";
import findIndex from "lodash/findIndex";
import { mapActions, mapGetters } from "vuex";

export default {
  components: {
    Layout,
    ProjectAdd,
    ProjectEditDialog,
    ProjectDeleteDialog
  },
  data() {
    return {
      isLoading: false,
      title: "Project Page",
      headers: [
        {
          text: "Project ID",
          align: "left",
          value: "id"
        },
        { text: "Project Code", value: "projectCode" },
        { text: "Project Name", value: "name" },
        { text: "Project Type", value: "projectType" },
        { text: "Client", value: "client.name" },
        { text: "Division", value: "department.name" },
        { text: "Project Description", value: "description" },
        {
          text: "Actions",
          value: "actions",
          sortable: false,
          width: 90,
          align: "center"
        }
      ],
      rowsPerPageItems: [20, 50, 100, -1],
      pagination: {
        rowsPerPage: 50
      },
      projectList: {}
    };
  },
  methods: {
    ...mapActions("project", ["getProjects"]),
    async init() {
      this.isLoading = true;

      await this.getProjects();

      this.projectList = { ...this.project };

      this.isLoading = false;
    },
    getIndex(item) {
      const index = findIndex(this.projects, project => project.id === item.id);
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
    ...mapGetters("project", ["projects"])
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
  <Layout>
    <v-skeleton-loader
      v-if="isLoading"
      ref="skeleton"
      type="table"
      class="mx-auto"
    ></v-skeleton-loader>
    <v-container v-if="!isLoading">
      <v-row justify="center">
        <h4>Project Page</h4>
      </v-row>
      <v-row>
        <v-col cols="12" class="text-right">
          <v-btn @click="openCreateClientDialog" color="primary"
            >Create Project</v-btn
          >
        </v-col>
      </v-row>
      <v-row justify="center">
        <v-col cols="12">
          <!-- TABLE - Client -->
          <ProjectDeleteDialog ref="deleteClientDialog"></ProjectDeleteDialog>
          <ProjectEditDialog ref="editClientDialog"></ProjectEditDialog>

          <v-data-table
            :headers="headers"
            :items="projects"
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
      <ProjectAdd ref="dialog"></ProjectAdd>
    </v-container>
  </Layout>
</template>
