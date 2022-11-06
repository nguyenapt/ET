<script>
import { mapActions } from "vuex";

export default {
  data() {
    return {
      callbackOnClose: null,
      showDialog: false,
      project: {}
    };
  },
  computed: {},
  methods: {
    ...mapActions("project", ["deleteProject"]),
    ...mapActions("layout", ["setSnackbar", "setCommonErrorToast"]),
    openDialog({ item, callbackOnClose }) {
      this.project = { ...item };
      if (callbackOnClose) {
        this.callbackOnClose = callbackOnClose;
      }
      this.showDialog = true;
    },
    close() {
      this.callbackOnClose = null;
      this.showDialog = false;
    },
    remove() {
      this.deleteProject(this.project.id)
        .then(() => {
          if (this.callbackOnClose) {
            this.callbackOnClose();
          }
          this.close();
          this.setSnackbar({
            text: "Deleted Successfully!"
          });
        })
        .catch(err => {
          if (err.message) {
            this.setSnackbar({
              text: err.message,
              color: "error",
              timeout: 5000
            });
          } else {
            this.setCommonErrorToast();
          }
        });
    }
  }
};
</script>
<template>
  <v-dialog :value="showDialog" @click:outside="close" max-width="600px">
    <v-card>
      <v-card-title primary-title class="headline grey lighten-2"
        >Delete Project</v-card-title
      >

      <v-divider></v-divider>

      <v-container>Are you sure to delete this project?</v-container>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="remove">Yes</v-btn>
        <v-btn color="primary" @click="close">No</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
