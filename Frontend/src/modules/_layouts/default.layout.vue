<script>
import { mapState } from "vuex";
import Header from "./header.vue";
import Footer from "./footer.vue";
import Sidebar from "./sidebar.vue";

export default {
  components: {
    Header,
    Footer,
    Sidebar
  },
  data() {
    return {};
  },
  computed: {
    ...mapState("layout", ["snackbars"]),
    expandedSidebar() {
      return this.$store.state.layout.expandedSidebar;
    }
  }
};
</script>
<template>
  <div>
    <Sidebar />
    <Header />
    <v-content>
      <v-container fluid class="fill-height white">
        <slot />
      </v-container>
    </v-content>
    <Footer />
    <v-snackbar
      v-for="(snackbar, index) in snackbars.filter(s => s.showing)"
      :key="snackbar.text + Math.random()"
      v-model="snackbar.showing"
      top
      right
      :timeout="snackbar.timeout"
      :color="snackbar.color"
      :style="`top: ${index * 60 + 80}px`"
    >
      {{ snackbar.text }}
    </v-snackbar>
  </div>
</template>
