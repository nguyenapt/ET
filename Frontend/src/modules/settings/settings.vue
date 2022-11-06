<script>
import Layout from "../_layouts/default.layout";
import { mapActions } from "vuex";

export default {
  components: {
    Layout
  },
  data() {
    return {
      title: "Settings page",
      submitted: false,
      authenticateErrorMsg: "",
      dics: null,
      tab: null,
      listItems: [],
      tabs: ["tab1", "tab2", "tab3"]
    };
  },
  mounted() {
    this.getSettings()
      .then(res => {
        this.dics = res;
      })
      .catch(err => {
        this.authenticateErrorMsg = err;
      });
  },
  computed: {},

  methods: {
    ...mapActions("settings", ["getSettings", "changeSettings"]),
    ...mapActions("layout", ["setSnackbar", "setCommonErrorToast"]),
    hanlderSubmit(e) {
      e && e.preventDefault();
      this.submitted = true;
      if (this.dics) {
        this.listItems = this.listItems.concat.apply(
          [],
          Object.values(this.dics)
        );
        return this.changeSettings({ dics: this.listItems })
          .then(res => {
            if (res) {
              this.setSnackbar({ text: "Edit successfully!" });
            } else {
              this.setCommonErrorToast();
            }
          })
          .catch(() => {
            this.setCommonErrorToast();
          });
      }
    }
  }
};
</script>

<template>
  <Layout>
    <v-container>
      <v-row justify="center">
        <h4>{{ title }}</h4>
      </v-row>
      <v-row>
        <span class="error-msg">{{ authenticateErrorMsg }}</span>
      </v-row>
      <v-tabs v-model="tab" class="mb-5">
        <v-tabs-slider></v-tabs-slider>

        <v-tab v-for="(arrs, propertyName) in dics" :key="propertyName">{{
          propertyName
        }}</v-tab>
      </v-tabs>
      <v-tabs-items v-model="tab">
        <v-tab-item v-for="(arrs, propertyName) in dics" :key="propertyName">
          <v-form ref="form" v-if="authenticateErrorMsg == ''">
            <v-row>
              <v-col v-for="item in arrs" :key="item.name" md="6" cols="12">
                <v-text-field
                  hide-details
                  dense
                  outlined
                  v-model="item.value"
                  :label="item.displayName"
                  class="px-2"
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-btn color="primary" @click="hanlderSubmit" class="ml-2"
                  >Save</v-btn
                >
              </v-col>
            </v-row>
          </v-form>
        </v-tab-item>
      </v-tabs-items>
    </v-container>
  </Layout>
</template>
