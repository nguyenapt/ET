import Service from "../../core/service";
import { ApiUrl } from "../../core/api-url";
import findIndex from "lodash/findIndex";
import cloneDeep from "lodash/cloneDeep";

let service = new Service("project", this);

/** State Definition */
const state = {
  selectors: [],
  project: {},
  projects: []
};

/** Getters - Return State */
const getters = {
  selectors(state) {
    return { ...state.selectors };
  },
  project(state) {
    return state.project;
  },
  projects(state) {
    return state.projects;
  }
};

/** Mutations - Synchronous */
const mutations = {
  ["UPDATE_SELECTORS"](state, payload) {
    state.selectors = payload || {};
  },
  ["UPDATE_PROJECT"](state, project) {
    state.project = project || {};
  },
  ["UPDATE_PROJECTS"](state, projects) {
    state.projects = projects || [];
  }
};

/** Actions - Asynchronous */
const actions = {
  getSelectors({ commit }, payload) {
    return service
      .post(ApiUrl.GetFormSelectors, payload)
      .then(res => {
        commit("UPDATE_SELECTORS", res.result);
      })
      .catch(err => {
        throw err;
      });
  },
  getProjects({ commit }, params) {
    return service
      .get(ApiUrl.GetProjects, params)
      .then(res => {
        commit("UPDATE_PROJECTS", res.result.items);
        return res.result;
      })
      .catch(err => {
        throw err;
      });
  },
  getProjectById({ commit }, Id) {
    return service
      .get(ApiUrl.GetProjectDetail, { Id })
      .then(res => {
        commit("UPDATE_PROJECT", res.result);
      })
      .catch(err => {
        throw err;
      });
  },
  createProject({ commit, state }, payload) {
    return service
      .post(ApiUrl.CreateProject, payload)
      .then(res => {
        const projects = cloneDeep(state.projects);
        projects.push(res.result);
        commit("UPDATE_PROJECTS", projects);
      })
      .catch(err => {
        throw err;
      });
  },
  editProject({ commit }, payload) {
    return service
      .put(ApiUrl.UpdateProject, payload)
      .then(res => commit("UPDATE_PROJECT", res.result))
      .catch(err => {
        throw err;
      });
  },
  deleteProject({ commit, state }, Id) {
    return service
      .delete(ApiUrl.DeleteProject, { Id })
      .then(() => {
        const projects = cloneDeep(state.projects);
        const deletedIndex = findIndex(projects, item => item.id === Id);

        if (deletedIndex >= 0) {
          projects.splice(deletedIndex, 1);

          commit("UPDATE_PROJECTS", projects);
        }
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
