import Service from "../../core/service";
import { ApiUrl } from "../../core/api-url";
import findIndex from "lodash/findIndex";
import cloneDeep from "lodash/cloneDeep";

let service = new Service("client", this);

/** State Definition */
const state = {
  client: {},
  clients: []
};

/** Getters - Return State */
const getters = {
  client(state) {
    return state.client;
  },
  clients(state) {
    return state.clients;
  }
};

/** Mutations - Synchronous */
const mutations = {
  ["UPDATE_CLIENT"](state, client) {
    state.client = client;
  },
  ["UPDATE_CLIENTS"](state, clients) {
    state.clients = clients;
  }
};

/** Actions - Asynchronous */
const actions = {
  getClients({ commit }, params) {
    return service
      .get(ApiUrl.GetClients, params)
      .then(res => {
        commit("UPDATE_CLIENTS", res.result.items);
        return res.result;
      })
      .catch(err => {
        throw err;
      });
  },
  getClientById({ commit }, Id) {
    return service
      .get(ApiUrl.GetClientDetail, { Id })
      .then(res => {
        commit("UPDATE_CLIENT", res.result);
      })
      .catch(err => {
        throw err;
      });
  },
  createClient({ commit, state }, payload) {
    return service
      .post(ApiUrl.CreateClient, payload)
      .then(res => {
        const clients = cloneDeep(state.clients);
        clients.push(res.result);
        commit("UPDATE_CLIENTS", clients);
      })
      .catch(err => {
        throw err;
      });
  },
  editClient({ commit }, payload) {
    return service
      .put(ApiUrl.UpdateClient, payload)
      .then(res => commit("UPDATE_CLIENT", res.result))
      .catch(err => {
        throw err;
      });
  },
  deleteClient({ commit, state }, Id) {
    return service
      .delete(ApiUrl.DeleteClient, { Id })
      .then(() => {
        const clients = cloneDeep(state.clients);
        const deletedIndex = findIndex(clients, item => item.id === Id);

        if (deletedIndex >= 0) {
          clients.splice(deletedIndex, 1);

          commit("UPDATE_CLIENTS", clients);
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
