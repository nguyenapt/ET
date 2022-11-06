<script>
import { mapActions } from "vuex";

export default {
  data() {
    return {
      showDialog: false,
      client: {},
      rules: {
        nameRules: [v => !!v || "Name is required"],
        emailRules: [v => !!v || "Email is required"],
        addressRules: [v => !!v || "Client Address is required"]
      }
    };
  },
  methods: {
    ...mapActions("client", ["createClient"]),
    ...mapActions("layout", ["setSnackbar", "setCommonErrorToast"]),
    create() {
      const isValidForm = this.$refs.form.validate();

      if (isValidForm) {
        this.createClient(this.client)
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
      this.client = {};
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
          >Create Client</v-card-title
        >

        <v-divider></v-divider>

        <v-container>
          <v-row no-gutters>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="client.name"
                :rules="rules.nameRules"
                label="Client Name"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="client.email"
                :rules="rules.emailRules"
                label="Email"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="client.description"
                label="Description"
                class="px-2"
              ></v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                dense
                outlined
                v-model="client.address"
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
                v-model="client.website"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-container>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="showDialog = false">Cancel</v-btn>
          <v-btn color="primary" @click="create">Create</v-btn>
        </v-card-actions>
      </v-form>
    </v-card>
  </v-dialog>
</template>
