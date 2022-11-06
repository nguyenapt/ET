import Service from "../../core/service";
import { ApiUrl } from "../../core/api-url";
let service = new Service("home", this);
/** State Definition */
const state = {
  profile: {},
  roles: []
};

/** Getters - Return State */
const getters = {
  profile(state) {
    return state.profile;
  },
  roles(state) {
    return state.roles;
  }
};

/** Mutations - Synchronous */
const mutations = {
  ["UPDATE_USER_PROFILE"](state, profile) {
    state.profile = profile;
  },
  ["UPDATE_ROLES"](state, roles) {
    state.roles = roles;
  }
};

/** Actions - Asynchronous */
const actions = {
  // getProfile({ commit }, { userName }) {
  //   service.rest("get").then((data) => {
  //       commit("UPDATE", data);
  //   }).catch((err) => {
  //       toastr.error(err.message);
  //   });
  // },
  getRoles({ commit }) {
    return service.get(ApiUrl.GetAllPermissions).then(
      res => {
        if (res.result && res.result.items) {
          commit("UPDATE_ROLES", res.result.items);
        } else {
          commit("UPDATE_ROLES", []);
        }
        return res;
      },
      err => {
        throw err;
      }
    );
  }
};

/** Private helpers */

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
};
