import axios from "axios";
import { ApiUrl } from "./api-url";
import { JwtHelper } from "./jwt-helper";
import { TokenKey, CurrentUserId, CurrentUserRoles } from "./config";
import Service from "./service";
let service = new Service("auth", this);

export const state = {
  authToken: getSavedState(TokenKey.AuthToken),
  currentUserId: getSavedState(CurrentUserId),
  currentUserRoles: JSON.parse(getSavedState(CurrentUserRoles)) || []
};

export const mutations = {
  SET_ACCESS_TOKEN(state, newAuthToken) {
    state.authToken = newAuthToken;
    saveState(TokenKey.AuthToken, newAuthToken);
  },
  SET_CURRENT_USERID(state, userId) {
    state.currentUserId = userId;
    saveState(CurrentUserId, userId);
  },
  SET_CURRENT_USER_ROLES(state, roles) {
    state.currentUserRoles = roles || [];
    saveState(CurrentUserRoles, JSON.stringify(roles));
  },
  REMOVE_ALL_TOKEN() {
    localStorage.clear();
  }
};

export const getters = {
  // Whether the user is currently logged in.
  loggedIn(state) {
    return !!state.authToken;
  },
  currentUserRoles(state) {
    return [...state.currentUserRoles];
  }
};

export const actions = {
  // This is automatically run in `src/state/store.js` when the app
  // starts, along with any other actions named `init` in other modules.
  init({ dispatch }) {
    dispatch("validate");
  },

  // Logs in the current user.
  logIn({ commit, getters }, { username, password } = {}) {
    if (getters.loggedIn) {
      //TODO: Do something with current token
      // return dispatch('validate');
    }

    return service
      .post(ApiUrl.Login, { userNameOrEmailAddress: username, password })
      .then(res => {
        const accessToken = res.result && res.result.accessToken;
        const userId = res.result && res.result.userId;
        const roles = res.result && res.result.roles;
        if (accessToken) {
          commit("SET_ACCESS_TOKEN", accessToken);
        }
        if (userId) {
          commit("SET_CURRENT_USERID", userId);
        }
        if (roles) {
          commit("SET_CURRENT_USER_ROLES", roles);
        }
        return res.result;
      })
      .catch(err => {
        throw err;
      });
  },
  getProfile() {
    if (state.currentUserId) {
      return service
        .get(ApiUrl.UserGetProfile, { userId: state.currentUserId })
        .then(res => {
          return res.result;
        })
        .catch(err => {
          throw err;
        });
    }
    return null;
  },
  // Logs out the current user.
  logOut({ commit }) {
    return new Promise(resolve => {
      commit("SET_ACCESS_TOKEN", null);
      commit("SET_CURRENT_USERID", null);
      commit("SET_CURRENT_USER_ROLES", null);
      commit("REMOVE_ALL_TOKEN");
      resolve("Logout Success!");
    });
  },

  // Validates the current user's token and refreshes it
  // with new data from the API.
  validate({ commit, state }) {
    if (!state.authToken) return Promise.resolve(null);
    return axios
      .get("/api/session")
      .then(response => {
        const user = response.data;
        commit("SET_ACCESS_TOKEN", user);
        return user;
      })
      .catch(error => {
        if (error.response && error.response.status === 401) {
          commit("SET_ACCESS_TOKEN", null);
        } else {
          console.warn(error);
        }
        return null;
      });
  },
  loginFake({ commit }, { username, password } = {}) {
    return new Promise((resolve, reject) => {
      if (!!username && !!password) {
        let data = {
          username: username,
          token: "Fake"
        };
        let token = JwtHelper.createSigningToken(data);
        commit("SET_ACCESS_TOKEN", token);
        resolve(data);
      } else {
        reject("Fake Data");
      }
    });
  }
};

// ===
// Private helpers
// ===

function getSavedState(key) {
  return window.localStorage.getItem(key);
}

function saveState(key, state) {
  window.localStorage.setItem(key, state);
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
};
