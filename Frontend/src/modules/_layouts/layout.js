/** State Definition */
const state = {
  expandedSidebar: false,
  snackbars: []
};

/** Getters - Return State */
const getters = {
  sideBarState(state) {
    return state.expandedSidebar;
  }
};

/** Mutations - Synchronous */
const mutations = {
  ["TOGGLE_SIDEBAR"](state) {
    state.expandedSidebar = !state.expandedSidebar;
  },
  ["SET_SNACKBAR"](state, snackbar) {
    state.snackbars.push(snackbar);
  }
};

/** Actions - Asynchronous */
const actions = {
  toggleSidebar({ commit }) {
    commit("TOGGLE_SIDEBAR");
  },
  setSnackbar({ commit }, snackbar) {
    snackbar.showing = true;
    snackbar.color = snackbar.color || "success";
    snackbar.timeout = snackbar.timeout || 3000;
    commit("SET_SNACKBAR", snackbar);
  },
  setCommonErrorToast({ dispatch }) {
    const snackbar = {
      color: "error",
      text: "Something went wrong, please try again."
    };
    dispatch("setSnackbar", snackbar);
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
