<script>
import Layout from "../_layouts/default.layout";
import { mapActions } from "vuex";

export default {
  components: {
    Layout
  },
  data() {
    return {
      title: "Home page title"
    };
  },
  computed: {},
  created() {
    // this.getRoles();
  },
  methods: {
    ...mapActions("home", ["getRoles"])
  }
};
</script>

<template>
  <Layout>
    <div class="login-page"></div>
    <h4>Home page {{ title }}</h4>
  </Layout>
</template>
