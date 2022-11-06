<script>
import Layout from "../_layouts/default.layout";
import { mapActions, mapGetters } from "vuex";

export default {
  components: {
    Layout
  },
  data() {
    return {
      isLoading: false,
      title: "Set Permission Page",
      initRoleDetail: null
    };
  },
  computed: {
    ...mapGetters("setpermission", ["userPermissions", "userRoles"])
  },
  methods: {
    ...mapActions("setpermission", [
      "updateRolePermissions",
      "getUserRoles",
      "getUserPermissions"
    ]),
    ...mapActions("layout", ["setSnackbar", "setCommonErrorToast"]),
    async init() {
      this.isLoading = true;

      await this.getUserRoles();
      await this.getUserPermissions();
      this.isLoading = false;
    },

    hanlderSubmit() {
      const isFormValid = this.$refs.form.validate();
      if (isFormValid) {
        this.updateRolePermissions(this.initRoleDetail)
          .then(() => {
            this.setSnackbar({ text: "Update successfully!" });
          })
          .catch(() => {
            this.setCommonErrorToast();
          });
      }
    },
    selected() {}
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
    <v-container v-else>
      <v-row justify="center">
        <h4>{{ title }}</h4>
      </v-row>
      <v-row>
        <v-col cols="12" sm="8" class="white">
          <v-form ref="form" @submit.prevent="hanlderSubmit" class="px-2">
            <v-row>
              <v-col cols="12" sm="3">
                <v-select
                  dense
                  outlined
                  v-model="initRoleDetail"
                  :items="userRoles"
                  item-text="name"
                  return-object
                  label="Select Role"
                ></v-select>
              </v-col>
            </v-row>
            <v-row>
              <v-col v-if="!!this.initRoleDetail">
                <v-label>Permissions</v-label>
                <div
                  v-for="permission in userPermissions"
                  :key="permission.name"
                >
                  <v-checkbox
                    v-model="initRoleDetail.grantedPermissions"
                    :label="permission.displayName"
                    :value="permission.name"
                  />
                </div>
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-btn type="submit" color="primary">Save</v-btn>
              </v-col>
            </v-row>
          </v-form>
        </v-col>
      </v-row>
    </v-container>
  </Layout>
</template>
