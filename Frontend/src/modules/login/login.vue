<script>
import { authMethods } from "../../core/helpers";
import { JwtHelper } from "../../core/jwt-helper";
// import { TokenKey } from "../../core/config";
export default {
  data() {
    return {
      userName: "",
      passWord: "",
      submitted: false,
      isRemember: false,
      authErrorMsg: "",
      isLoggingIn: false
    };
  },
  beforeCreate() {
    if (JwtHelper.isAuthenticated()) {
      this.$router.push(this.$route.query.redirect || "/");
    }
  },
  computed: {
    placeHolders() {
      return {
        userName: "Email or Username",
        passWord: "Password"
      };
    }
  },
  created() {},
  methods: {
    ...authMethods,
    hanlderSubmit(e) {
      e && e.preventDefault();
      this.submitted = true;
      if (!!this.userName && !!this.passWord) {
        this.isLoggingIn = true;
        return this.logIn({
          username: this.userName,
          password: this.passWord
        })
          .then(() => {
            this.isLoggingIn = false;
            this.$router.push(this.$route.query.redirect || "/");
          })
          .catch(err => {
            this.isLoggingIn = false;
            this.authErrorMsg = err.message || "Login failed!";
          });
      }
    }
  }
};
</script>

<template>
  <v-container class="fill-height" fluid>
    <v-row align="center" justify="center">
      <v-col cols="12" sm="8" md="4">
        <v-card class="elevation-12">
          <v-toolbar color="primary" dark flat>
            <v-toolbar-title>Efficient Time</v-toolbar-title>
            <v-spacer />
          </v-toolbar>
          <v-card-text>
            <v-form @keyup.native.enter="hanlderSubmit">
              <v-alert type="error" dense outlined v-if="!!authErrorMsg">{{
                authErrorMsg
              }}</v-alert>
              <v-text-field
                dense
                outlined
                label="Username"
                name="Username"
                v-model="userName"
                type="text"
                :rules="[v => !!v || 'Username is required']"
              />
              <v-text-field
                dense
                outlined
                id="password"
                label="Password"
                name="password"
                v-model="passWord"
                type="password"
                :rules="[v => !!v || 'Password is required']"
              />
            </v-form>
          </v-card-text>
          <v-card-actions>
            <v-spacer />
            <v-btn color="primary" type="submit" @click="hanlderSubmit"
              >Login</v-btn
            >
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>
