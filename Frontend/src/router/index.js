import Vue from "vue";
import Router from "vue-router";
import { MODULE_ENTRY_ROLES } from "../core/config";
import { JwtHelper } from "../core/jwt-helper";
import store from "../core/store";
import { UtilityService } from "../core/utility.service";

import SowRoutingModule from "../modules/sow/sow-routing";
import ClientRoutingModule from "../modules/client/client-routing";
import ProjectRoutingModule from "../modules/project/project-routing";
import UserRoutingModule from "../modules/user/user-routing";
import qs from "qs";

// Setting
const SettingsPage = () => import("../modules/settings/settings.vue");
const HomePage = () => import("../modules/home/home.vue");
const LoginPage = () => import("../modules/login/login.vue");
const NotFoundPage = () => import("../modules/404/404.vue");
const ImportDataPage = () => import("../modules/settings/import-data.vue");
const ProfilePage = () => import("../modules/profile/profile.vue");
const SetPermissionPage = () =>
  import("../modules/settings/set-permission.vue");

Vue.use(Router);

export const router = new Router({
  mode: "history",
  routes: [
    {
      path: "/login",
      name: "Login",
      component: LoginPage,
      meta: {
        title: "Login Page"
      }
    },
    ...SowRoutingModule,
    ...ClientRoutingModule,
    ...ProjectRoutingModule,
    ...UserRoutingModule,

    // Setting
    {
      path: "/importdata",
      name: "ImportData",
      component: ImportDataPage,
      meta: {
        entryRoles: MODULE_ENTRY_ROLES.SETTINGS,
        title: "Import Data Page"
      }
    },
    {
      path: "/setpermission",
      name: "SetPermission",
      component: SetPermissionPage,
      meta: {
        entryRoles: MODULE_ENTRY_ROLES.SETTINGS,
        title: "Set Permission Page"
      }
    },
    {
      path: "/settings",
      name: "Settings",
      component: SettingsPage,
      meta: {
        entryRoles: MODULE_ENTRY_ROLES.SETTINGS,
        title: "Login Page"
      }
    },
    {
      path: "/",
      name: "HomePage",
      component: HomePage,
      // beforeEnter: AuthGuard,
      meta: {
        title: "Home Page"
      }
    },
    {
      path: "/myprofile",
      name: "MyProfile",
      component: ProfilePage,
      meta: {
        entryRoles: MODULE_ENTRY_ROLES.PROFILE,
        title: "Profile Page"
      }
    },
    {
      path: "/404",
      name: "404",
      component: NotFoundPage
    },
    {
      path: "*",
      redirect: "404"
    }
  ],
  parseQuery(query) {
    return qs.parse(query);
  },
  stringifyQuery(query) {
    const result = qs.stringify(query);

    return result ? `?${result}` : "";
  }
});

router.beforeEach((to, from, next) => {
  document.title = to.meta.title || "Efficient Time";

  if (!JwtHelper.isAuthenticated() && to.path !== "/login") {
    next("/login");
  } else {
    if (UtilityService.checkRoleEntryRight(to.meta.entryRoles)) {
      next();
    } else {
      store.dispatch("layout/setSnackbar", {
        text: "You don't have permission to access!",
        color: "error",
        timeout: 5000
      });
      next("/");
    }
  }
});

// let entryUrl = "";
// function AuthGuard(to, from, next) {
//   if (JwtHelper.isAuthenticated()) {
//     if (entryUrl) {
//       const url = entryUrl;
//       entryUrl = null;
//       return next(url); // go to stored url
//     } else {
//       return next();
//     }
//   }
//   // await store.dispatch('checkAuth');
//   // we use await as this async request has to finish
//   // before we can be sure

//   if (JwtHelper.isAuthenticated()) {
//     next();
//   } else {
//     entryUrl = to.path; // store entry url before redirect
//     next("/login");
//   }
// }
