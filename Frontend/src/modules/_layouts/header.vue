<script>
import { mapActions } from "vuex";
import { JwtHelper } from "../../core/jwt-helper";
export default {
  data() {
    return {
      title: "This is header component",
      isActiveUserArea: false,
      items: [
        { title: "My Profile", icon: "", link: "/myprofile", click: "" },
        { title: "Logout", icon: "", link: "", click: () => this.onLogout() }
      ]
    };
  },
  mounted() {
    this.isActiveUserArea = JwtHelper.isAuthenticated();
  },
  computed: {
    expandedSidebar() {
      return this.$store.state.layout.expandedSidebar;
    }
  },
  methods: {
    ...mapActions("auth", ["logOut"]),
    ...mapActions("layout", ["toggleSidebar"]),
    onToggleUserArea() {
      this.isActiveUserArea = JwtHelper.isAuthenticated();
    },
    onLogout() {
      this.logOut()
        .then(() => {
          this.isActiveUserArea = false;
          this.$router.push("/login");
        })
        .catch(err => {
          console.error(err);
        });
    },
    onToggleSidebar() {
      this.toggleSidebar();
    }
  }
};
</script>

<template>
  <v-app-bar
    :clipped-left="$vuetify.breakpoint.lgAndUp"
    app
    color="blue darken-3"
    dark
  >
    <v-app-bar-nav-icon @click.stop="onToggleSidebar" />
    <v-toolbar-title style="width: 300px" class="ml-0 pl-4">
      Efficient Time
    </v-toolbar-title>
    <v-text-field
      flat
      solo-inverted
      hide-details
      prepend-inner-icon="mdi-magnify"
      label="Search"
      class="hidden-sm-and-down"
    />
    <v-spacer />
    <v-btn icon>
      <v-icon>mdi-bell</v-icon>
    </v-btn>
    <v-btn v-if="!isActiveUserArea" text color="white" href="/login"
      >Login</v-btn
    >
    <v-menu offset-y v-if="isActiveUserArea">
      <template v-slot:activator="{ on }">
        <v-btn icon v-on="on">
          <v-icon>mdi-dots-vertical</v-icon>
        </v-btn>
      </template>

      <v-list>
        <v-list-item
          v-for="item in items"
          :key="item.title"
          :to="item.link"
          @click="item.click"
          :prepend-icon="item.icon"
        >
          <v-list-item-title>{{ item.title }}</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-menu>
  </v-app-bar>
</template>
