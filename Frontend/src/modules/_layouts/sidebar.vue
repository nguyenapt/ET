<script>
import { MODULE_ENTRY_ROLES } from "../../core/config";
import { UtilityService } from "../../core/utility.service";

export default {
  data() {
    return {
      items: [
        {
          icon: "mdi-home-city",
          title: "Home",
          link: "/"
        },
        {
          icon: "mdi-view-dashboard",
          title: "SoW",
          link: "/sow",
          entryRoles: MODULE_ENTRY_ROLES.SOW
        },
        {
          icon: "mdi-account",
          title: "Clients",
          link: "/client",
          entryRoles: MODULE_ENTRY_ROLES.CLIENT
        },
        {
          icon: "mdi-file-document",
          title: "Projects",
          link: "/project",
          entryRoles: MODULE_ENTRY_ROLES.PROJECT
        },
        {
          icon: "mdi-gavel",
          title: "Settings",
          link: "/settings",
          entryRoles: MODULE_ENTRY_ROLES.SETTINGS
        },
        {
          icon: "mdi-gavel",
          title: "Permissions",
          link: "/setpermission",
          entryRoles: MODULE_ENTRY_ROLES.SETTINGS
        },
        {
          icon: "mdi-gavel",
          title: "Import data",
          link: "/importdata",
          entryRoles: MODULE_ENTRY_ROLES.SETTINGS
        },
        {
          icon: "mdi-account-box",
          title: "Users",
          link: "/user",
          entryRoles: MODULE_ENTRY_ROLES.USER
        }
      ]
    };
  },
  computed: {
    expandedSidebar() {
      return this.$store.state.layout.expandedSidebar;
    }
  },
  methods: {
    toggleMiniDrawer() {
      this.mini = !this.mini;
    },
    checkRoleEntryRight(entryRoles) {
      return UtilityService.checkRoleEntryRight(entryRoles);
    }
  }
};
</script>

<template>
  <v-navigation-drawer
    app
    :clipped="$vuetify.breakpoint.lgAndUp"
    :mini-variant="expandedSidebar"
  >
    <v-card max-width="500" class="mx-auto">
      <v-list>
        <template v-for="item in items">
          <v-list-item
            v-if="checkRoleEntryRight(item.entryRoles)"
            :key="item.title"
            :to="item.link"
            :prepend-icon="item.icon"
          >
            <v-list-item-icon>
              <v-icon v-text="item.icon"></v-icon>
            </v-list-item-icon>
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item>
        </template>
      </v-list>
    </v-card>
  </v-navigation-drawer>
</template>
