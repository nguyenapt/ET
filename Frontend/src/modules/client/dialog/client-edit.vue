<script>
import { mapActions } from "vuex";

export default {
  data() {
    return {
      showDialog: false,
      callbackOnClose: null,
      rules: {
        nameRules: [v => !!v || "Name is required"],
        emailRules: [v => !!v || "Email is required"],
        addressRules: [v => !!v || "Client address is required"]
      },
      initClientDetail: {}
    };
  },
  methods: {
    ...mapActions("client", ["editClient", "getClientById"]),
    ...mapActions("layout", ["setSnackbar", "setCommonErrorToast"]),
    openDialog({ item, callbackOnClose }) {
      this.initClientDetail = { ...item };
      if (callbackOnClose) {
        this.callbackOnClose = callbackOnClose;
      }
      this.showDialog = true;
    },
    submit() {
      const isValidForm = this.$refs.form.validate();

      if (isValidForm) {
        this.editClient(this.initClientDetail)
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
    close() {
      this.clientDetail = {};
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
          >Edit Client</v-card-title
        >

        <v-divider></v-divider>

        <v-container>
          <v-row no-gutters>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="initClientDetail.name"
                :rules="rules.nameRules"
                label="Client Name"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="initClientDetail.email"
                :rules="rules.emailRules"
                label="Email"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="initClientDetail.description"
                label="Description"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="initClientDetail.address"
                :rules="rules.addressRules"
                required
                label="Client Address"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                label="Website"
                class="px-2"
                v-model="initClientDetail.website"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-container>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="showDialog = false">Cancel</v-btn>
          <v-btn color="primary" @click="submit" text>Save</v-btn>
        </v-card-actions>
      </v-form>
    </v-card>
  </v-dialog>
</template>
