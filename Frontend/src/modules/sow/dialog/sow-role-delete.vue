<script>
import { mapActions } from "vuex";

export default {
  data() {
    return {
      callbackOnClose: null,
      showDialog: false,
      sowRole: {}
    };
  },
  computed: {},
  methods: {
    ...mapActions("sow", ["deleteSowRole"]),
    ...mapActions("layout", ["setSnackbar", "setCommonErrorToast"]),
    openDialog({ sowRole, callbackOnClose }) {
      if (callbackOnClose) {
        this.callbackOnClose = callbackOnClose;
      }

      this.sowRole = { ...sowRole };
      this.showDialog = true;
    },
    close() {
      this.sowRole = {};
      this.callbackOnClose = null;
      this.showDialog = false;
    },
    remove() {
      this.deleteSowRole(this.sowRole.id)
        .then(() => {
          if (this.callbackOnClose) {
            this.callbackOnClose();
          }

          this.close();
          this.setSnackbar({ text: "Deleted successfully!" });
        })
        .catch(() => {
          this.setCommonErrorToast();
        });
    }
  }
};
</script>
<template>
  <v-dialog :value="showDialog" @click:outside="close" max-width="600px">
    <v-card>
      <v-card-title primary-title class="headline grey lighten-2"
        >Delete sow role</v-card-title
      >
      <v-divider></v-divider>
      <v-container>
        <v-card-text>Are you sure you want to delete this item?</v-card-text>
      </v-container>
      <v-divider></v-divider>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="remove">Yes</v-btn>
        <v-btn color="primary" @click="close">No</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
