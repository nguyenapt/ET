<script>
export default {
  data() {
    return {
      copyTitle: "EfficientTime © 2020 Ltd.",
      version: ""
    };
  }
};
</script>
<template>
  <v-footer app>
    <v-spacer></v-spacer>
    <label>
      <a
        href="https://provideithere.com/"
        target="_blank"
        title="Efficient Time"
        class="text-muted"
      >
        <span>{{ copyTitle }}</span>
        <b>{{ version }}</b>
        &nbsp;
      </a>
    </label>
  </v-footer>
</template>
