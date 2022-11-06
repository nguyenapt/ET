<script>
import Layout from "../_layouts/default.layout";
import { authMethods } from "../../core/helpers";
import { mapActions } from "vuex";

export default {
  components: {
    Layout
  },
  data() {
    return {
      title: "My Profile",
      profile: {}
    };
  },
  created() {
    this.getProfile()
      .then(res => {
        this.profile = res || {};
      })
      .catch(() => {
        this.setCommonErrorToast();
      });
  },
  computed: {},
  methods: {
    ...mapActions("layout", ["setCommonErrorToast"]),
    ...authMethods
  }
};
</script>

<template>
  <Layout>
    <v-container fluid>
      <v-row>
        <h4>{{ title }}</h4>
      </v-row>
      <v-layout column>
        <v-row v-if="!!profile">
          <v-col cols="12">
            <v-row v-if="!!profile.user">
              <v-col>
                <v-label>User name</v-label>
              </v-col>
              <v-col cols="9">
                <v-label>{{ profile.user.userName }}</v-label>
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-label>First name</v-label>
              </v-col>
              <v-col cols="9">
                <v-label>{{ profile.firstName }}</v-label>
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-label>Last name</v-label>
              </v-col>
              <v-col cols="9">
                <v-label>{{ profile.lastName }}</v-label>
              </v-col>
            </v-row>
            <v-row v-if="!!profile.user">
              <v-col>
                <v-label>Email</v-label>
              </v-col>
              <v-col cols="9">
                <v-label>{{ profile.user.emailAddress }}</v-label>
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-label>Employee code</v-label>
              </v-col>
              <v-col cols="9">
                <v-label>{{ profile.employeeCode }}</v-label>
              </v-col>
            </v-row>
            <v-row v-if="!!profile.role">
              <v-col>
                <v-label>Role</v-label>
              </v-col>
              <v-col cols="9">
                <v-label>{{ profile.role.normalizedName }}</v-label>
              </v-col>
            </v-row>
          </v-col>
        </v-row>
      </v-layout>
    </v-container>
  </Layout>
</template>
