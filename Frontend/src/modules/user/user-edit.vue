<script>
import Layout from "../_layouts/default.layout";
import { mapActions, mapGetters } from "vuex";
const REQUEST_BODY = ["Department", "WorkingHourRule"];
export default {
  components: {
    Layout
  },
  data() {
    return {
      isLoading: false,
      title: "Edit User Page",
      initForm: {
        user: {},
        resource: {}
      }
    };
  },
  mounted() {
    this.getEmployeeByUserId(this.$route.params.id)
      .then(res => {
        this.initForm.resource = res || {};
      })
      .catch(() => {});
  },
  computed: {
    ...mapGetters("user", ["user", "userRoles", "selectors"])
  },
  methods: {
    ...mapActions("user", [
      "editUser",
      "getUserById",
      "getRoles",
      "getEmployeeByUserId",
      "editResource",
      "getSelectors"
    ]),
    ...mapActions("layout", ["setSnackbar", "setCommonErrorToast"]),
    async init() {
      this.isLoading = true;
      Promise.all([
        this.getSelectors(REQUEST_BODY),
        this.getUserById(this.$route.params.id),
        // this.getEmployeeByUserId(this.$route.params.id),
        this.getRoles()
      ])
        .then(() => {
          this.initForm.user = { ...this.user };
          // this.initForm.resource = { ...this.resource };
          this.initRoles = { ...this.userRoles };
          this.isLoading = false;
        })
        .catch(() => {
          this.setCommonErrorToast();
        });
    },

    hanlderSubmit() {
      const isFormValid = this.$refs.form.validate();
      if (isFormValid) {
        var success = true;
        this.editUser(this.initForm.user)
          .then(() => {})
          .catch(() => {
            success = false;
          });

        this.editResource(this.initForm.resource)
          .then(() => {})
          .catch(() => {
            success = false;
          });
        if (success) {
          this.setSnackbar({ text: "Edit successfully!" });
        } else {
          this.setCommonErrorToast();
        }
      }
    },
    selected() {},
    backToList() {
      this.$router.push("/user");
    }
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
      <v-row justify="center">
        <v-col class="white">
          <v-form ref="form" @submit.prevent="hanlderSubmit">
            <v-row justify="center">
              <v-col cols="12" sm="3">
                <v-text-field
                  dense
                  outlined
                  v-model="initForm.user.name"
                  required
                  label="Name"
                  class="px-2"
                  disabled
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="3">
                <v-text-field
                  dense
                  outlined
                  v-model="initForm.user.userName"
                  required
                  label="Username"
                  class="px-2"
                  disabled
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="3">
                <v-text-field
                  dense
                  outlined
                  v-model="initForm.user.emailAddress"
                  required
                  label="Email"
                  class="px-2"
                  disabled
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row justify="center">
              <v-col cols="12" sm="3">
                <v-text-field
                  dense
                  outlined
                  v-model="initForm.resource.employeeCode"
                  required
                  label="Employee code"
                  class="px-2"
                ></v-text-field>
              </v-col>

              <v-col cols="12" sm="3">
                <v-autocomplete
                  dense
                  outlined
                  v-model="initForm.resource.departmentId"
                  :items="selectors.Department"
                  clearable
                  item-text="name"
                  item-value="value"
                  label="Division"
                  class="px-2"
                ></v-autocomplete>
              </v-col>
              <v-col cols="12" sm="3">
                <v-autocomplete
                  dense
                  outlined
                  v-model="initForm.resource.WorkingHourRuleId"
                  :items="selectors.WorkingHourRule"
                  clearable
                  item-text="name"
                  item-value="value"
                  label="Working hour rule"
                  class="px-2"
                ></v-autocomplete>
              </v-col>
            </v-row>
            <v-row no-gutters class="mb-4">
              <v-col cols="12">
                <v-label>Resource Property</v-label>
              </v-col>
              <v-col cols="12" sm="3">
                <v-checkbox
                  hide-details
                  class="mt-0"
                  v-model="initForm.resource.isKAM"
                  label="Is KAM"
                ></v-checkbox>
              </v-col>
            </v-row>
            <v-row no-gutters>
              <v-col cols="12">
                <v-label>User Role</v-label>
              </v-col>
              <v-col cols="2" v-for="role in initRoles" :key="role.name">
                <v-checkbox
                  hide-details
                  class="mt-0"
                  v-model="initForm.user.roleNames"
                  :label="role.displayName"
                  :value="role.normalizedName"
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col class="text-right">
                <v-btn @click="backToList" color="primary" class="mx-2"
                  >Cancel</v-btn
                >
                <v-btn type="submit" color="primary" class="mx-2">Save</v-btn>
              </v-col>
            </v-row>
          </v-form>
        </v-col>
      </v-row>
    </v-container>
  </Layout>
</template>
