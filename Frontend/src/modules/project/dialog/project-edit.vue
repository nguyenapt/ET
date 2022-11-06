<script>
import { mapActions, mapGetters } from "vuex";

const REQUEST_BODY = ["Client", "Department", "ProjectType"];

export default {
  data() {
    return {
      showDialog: false,
      callbackOnClose: null,
      rules: {
        nameRules: [v => !!v || "Name is required"],
        descriptionRules: [v => !!v || "Project description is required"]
      },
      initProjectDetail: {}
    };
  },
  computed: {
    ...mapGetters("project", ["selectors"])
  },
  methods: {
    ...mapActions("project", ["getSelectors", "editProject", "getProjectById"]),
    ...mapActions("layout", ["setSnackbar", "setCommonErrorToast"]),
    async init() {
      this.isLoading = true;

      await this.getSelectors(REQUEST_BODY);

      this.isLoading = false;
    },
    openDialog({ item, callbackOnClose }) {
      this.initProjectDetail = { ...item };
      if (callbackOnClose) {
        this.callbackOnClose = callbackOnClose;
      }
      this.showDialog = true;
    },
    submit() {
      const isValidForm = this.$refs.form.validate();

      if (isValidForm) {
        this.editProject(this.initProjectDetail)
          .then(() => {
            if (this.callbackOnClose) {
              this.callbackOnClose();
            }
            this.close();
            this.setSnackbar({
              text: "Edited Successfully!"
            });
          })
          .catch(() => {
            this.setCommonErrorToast();
          });
      }
    },
    open() {
      this.showDialog = true;
    },
    close() {
      this.callbackOnClose = null;
      this.$refs.form.reset();
      this.showDialog = false;
    }
  }
};
</script>

<template>
  <v-dialog v-model="showDialog" @click:outside="close" max-width="800">
    <v-card>
      <v-form ref="form">
        <v-card-title primary-title class="headline grey lighten-2"
          >Edit Project</v-card-title
        >

        <v-divider></v-divider>

        <v-container>
          <v-row no-gutters>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="initProjectDetail.name"
                :rules="rules.nameRules"
                label="Project Name"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-autocomplete
                dense
                outlined
                v-model="initProjectDetail.projectType"
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
                v-model="initProjectDetail.clientId"
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
                v-model="initProjectDetail.departmentId"
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
                v-model="initProjectDetail.description"
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
          <v-btn color="primary" @click="submit" text>Save</v-btn>
        </v-card-actions>
      </v-form>
    </v-card>
  </v-dialog>
</template>
