import Service from "../../core/service";
import { ApiUrl } from "../../core/api-url";
import cloneDeep from "lodash/cloneDeep";
let service = new Service("user", this);

/** State Definition */
const state = {
  selectors: {},
  user: {},
  resource: {},
  users: [],
  userRoles: [],
  test: []
};

/** Getters - Return State */
const getters = {
  user(state) {
    return state.user;
  },
  users(state) {
    return state.users;
  },
  userRoles(state) {
    return state.userRoles;
  },
  resource(state) {
    return state.resource;
  },
  selectors(state) {
    return { ...state.selectors };
  }
};

/** Mutations - Synchronous */
const mutations = {
  ["UPDATE_USER"](state, payload) {
    state.user = payload || {};
  },
  ["UPDATE_USERS"](state, payload) {
    state.users = payload || [];
  },
  ["UPDATE_USER_ROLES"](state, payload) {
    state.userRoles = payload || [];
  },
  ["UPDATE_RESOURCE"](state, payload) {
    state.resource = payload || {};
  },
  ["UPDATE_SELECTORS"](state, payload) {
    state.selectors = payload || {};
  }
};

/** Actions - Asynchronous */
const actions = {
  getUserById({ commit }, Id) {
    return service
      .get(ApiUrl.GetUserById, { Id })
      .then(res => {
        commit("UPDATE_USER", res.result);
      })
      .catch(err => {
        throw err;
      });
  },
  getEmployeeByUserId({ commit }, Id) {
    return service
      .get(ApiUrl.GetResourceForEdit, { userId: Id })
      .then(res => {
        commit("UPDATE_RESOURCE", res.result);
        return res.result;
      })
      .catch(err => {
        throw err;
      });
  },
  getUsers({ commit }, params) {
    return service
      .get(ApiUrl.GetAllUsers, params)
      .then(res => {
        commit("UPDATE_USERS", res.result.items);
        return res.result;
      })
      .catch(err => {
        throw err;
      });
  },
  getRoles({ commit }) {
    return service
      .get(ApiUrl.GetUserRoles)
      .then(res => {
        commit("UPDATE_USER_ROLES", cloneDeep(res.result.items));
        return res.result;
      })
      .catch(err => {
        throw err;
      });
  },
  editUser({ commit }, payload) {
    return service
      .put(ApiUrl.UpdateUser, payload)
      .then(res => {
        commit("UPDATE_USER", res.result);
      })
      .catch(err => {
        throw err;
      });
  },
  editResource({ commit }, payload) {
    console.log("edit resource");
    console.log(payload);
    return service
      .put(ApiUrl.UpdateResource, payload)
      .then(res => {
        commit("UPDATE_RESOURCE", res.result);
      })
      .catch(err => {
        throw err;
      });
  },
  getSelectors({ commit }, payload) {
    return service
      .post(ApiUrl.GetFormSelectors, payload)
      .then(res => {
        commit("UPDATE_SELECTORS", res.result);
      })
      .catch(err => {
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
