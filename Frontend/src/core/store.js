import Vue from "vue";
import Vuex from "vuex";
import auth from "../core/auth";
import home from "../modules/home/home";
import sow from "../modules/sow/sow";
import client from "../modules/client/client";
import project from "../modules/project/project";
import user from "../modules/user/user";
import settings from "../modules/settings/settings";
import importdata from "../modules/settings/import-data";
import setpermission from "../modules/settings/set-permission";
import layout from "../modules/_layouts/layout";
Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    auth,
    home,
    sow,
    client,
    project,
    user,
    settings,
    importdata,
    setpermission,
    layout
  }
});
