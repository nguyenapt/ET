<script>
import { mapActions, mapGetters } from "vuex";

const REQUEST_BODY = ["Client", "Department", "ProjectType"];

export default {
  data() {
    return {
      isLoading: false,
      showDialog: false,
      project: {},
      rules: {
        nameRules: [v => !!v || "Name is required"],
        clientRules: [v => !!v || "Client is required"],
        departmentRules: [v => !!v || "Department is required"]
      }
    };
  },
  computed: {
    ...mapGetters("project", ["selectors"])
  },
  methods: {
    ...mapActions("project", ["getSelectors", "createProject"]),
    ...mapActions("layout", ["setSnackbar", "setCommonErrorToast"]),
    async init() {
      this.isLoading = true;

      await this.getSelectors(REQUEST_BODY);

      this.isLoading = false;
    },
    create() {
      const isValidForm = this.$refs.form.validate();

      if (isValidForm) {
        this.createProject(this.project)
          .then(() => {
            if (this.callbackOnClose) {
              this.callbackOnClose();
            }
            this.close();
            this.setSnackbar({ text: "Created successfully!" });
          })
          .catch(() => {
            this.close();
            this.setCommonErrorToast();
          });
      }
    },
    open({ callbackOnClose }) {
      if (callbackOnClose) {
        this.callbackOnClose = callbackOnClose;
      }

      this.showDialog = true;
    },
    close() {
      this.callbackOnClose = null;
      this.$refs.form.reset();
      this.showDialog = false;
    }
  },
  created() {
    this.init();
  }
};
</script>

<template>
  <v-dialog v-model="showDialog" @click:outside="close" max-width="800">
    <v-card>
      <v-form ref="form">
        <v-card-title primary-title class="headline grey lighten-2"
          >Create Project</v-card-title
        >

        <v-divider></v-divider>

        <v-container v-if="!isLoading">
          <v-row no-gutters>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="project.name"
                :rules="rules.nameRules"
                label="Project Name"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-autocomplete
                dense
                outlined
                v-model="project.projectType"
                :items="selectors.ProjectType"
                item-text="name"
                item-value="value"
                label="Project Type"
                class="px-2"
              ></v-autocomplete>
            </v-col>
            <v-col cols="12" md="6">
              <v-autocomplete
                dense
                outlined
                v-model="project.clientId"
                :items="selectors.Client"
                :rules="rules.clientRules"
                item-text="name"
                item-value="value"
                label="Client Name"
                class="px-2"
              ></v-autocomplete>
            </v-col>
            <v-col cols="12" md="6">
              <v-autocomplete
                dense
                outlined
                v-model="project.departmentId"
                :items="selectors.Department"
                clearable
                item-text="name"
                item-value="value"
                label="Division"
                class="px-2"
              ></v-autocomplete>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="project.description"
                label="Description"
                class="px-2"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-container>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="close">Cancel</v-btn>
          <v-btn color="primary" @click="create">Create</v-btn>
        </v-card-actions>
      </v-form>
    </v-card>
  </v-dialog>
</template>
