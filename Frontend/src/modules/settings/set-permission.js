import Service from "../../core/service";
import { ApiUrl } from "../../core/api-url";
import cloneDeep from "lodash/cloneDeep";
let service = new Service("setpermission", this);

/** State Definition */
const state = {
  selectors: {},
  userPermissions: [],
  userRoles: []
};

/** Getters - Return State */
const getters = {
  userPermissions(state) {
    return state.userPermissions;
  },
  userRoles(state) {
    return state.userRoles;
  }
};

/** Mutations - Synchronous */
const mutations = {
  ["UPDATE_USER_PERMISSIONS"](state, payload) {
    state.userPermissions = payload || [];
  },
  ["UPDATE_USER_ROLES"](state, payload) {
    state.userRoles = payload || [];
  }
};

/** Actions - Asynchronous */
const actions = {
  getUserRoles({ commit }) {
    return service
      .get(ApiUrl.GetAllRoles)
      .then(res => {
        commit("UPDATE_USER_ROLES", res.result.items);
        return res.result.items;
      })
      .catch(err => {
        throw err;
      });
  },
  getUserPermissions({ commit }) {
    return service
      .get(ApiUrl.GetAllPermissions)
      .then(res => {
        commit("UPDATE_USER_PERMISSIONS", cloneDeep(res.result.items));
        return res.result.items;
      })
      .catch(err => {
        throw err;
      });
  },
  updateRolePermissions(context, payload) {
    return service.put(ApiUrl.UpdateRolePermissions, payload).catch(err => {
      throw err;
    });
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
