import Service from "../../core/service";
import { ApiUrl } from "../../core/api-url";
import { saveAs } from "file-saver";
import findIndex from "lodash/findIndex";
import cloneDeep from "lodash/cloneDeep";
import { format } from "date-fns";
import { XLS_DOWNLOAD_DATE } from "../../constants";

let service = new Service("sow", this);

/** State Definition */
const state = {
  selectors: {},
  sow: {},
  sows: [],
  sowRoles: [],
  test: [],
  billings: []
};

/** Getters - Return State */
const getters = {
  selectors(state) {
    return { ...state.selectors };
  },
  sow(state) {
    return state.sow;
  },
  sows(state) {
    return state.sows;
  },
  sowRoles(state) {
    return state.sowRoles;
  },
  billings(state) {
    return state.billings;
  }
};

/** Mutations - Synchronous */
const mutations = {
  ["UPDATE_SELECTORS"](state, payload) {
    state.selectors = payload || {};
  },
  ["UPDATE_SOW"](state, payload) {
    state.sow = payload || {};
  },
  ["UPDATE_SOWS"](state, payload) {
    state.sows = payload || [];
  },
  ["UPDATE_SOW_ROLES"](state, payload) {
    state.sowRoles = payload || [];
  },
  ["UPDATE_BILLINGS"](state, payload) {
    state.billings = payload || [];
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
  getSowById({ commit }, Id) {
    return service
      .get(ApiUrl.GetSowDetail, { Id })
      .then(res => {
        commit("UPDATE_SOW", res.result);
        commit("UPDATE_SOW_ROLES", cloneDeep(res.result.sowRoles));
      })
      .catch(err => {
        throw err;
      });
  },
  getSows({ commit }, params) {
    return service
      .get(ApiUrl.GetSows, params)
      .then(res => {
        commit("UPDATE_SOWS", res.result.items);
        return res.result;
      })
      .catch(err => {
        throw err;
      });
  },
  getSowRoleDetailListById(context, id) {
    return service
      .get(ApiUrl.GetSowRoleDetailList, { sowId: id })
      .then(res => {
        return res.result;
      })
      .catch(err => {
        throw err;
      });
  },
  createSow(context, payload) {
    return service.post(ApiUrl.CreateSow, payload);
  },
  editSow({ commit }, payload) {
    return service
      .put(ApiUrl.UpdateSow, payload)
      .then(res => commit("UPDATE_SOW", res.result))
      .catch(err => {
        throw err;
      });
  },
  deleteSow(context, Id) {
    return service.delete(ApiUrl.DeleteSow, { Id });
  },
  createSowRole(context, payload) {
    return service.post(ApiUrl.CreateSowRole, payload);
  },
  updateSowRole({ commit, state }, payload) {
    return service
      .put(ApiUrl.UpdateSowRole, payload)
      .then(res => {
        const sowRoles = cloneDeep(state.sowRoles);
        const editedIndex = findIndex(sowRoles, item => item.id === payload.id);

        if (editedIndex >= 0) {
          sowRoles[editedIndex] = res.result;

          commit("UPDATE_SOW_ROLES", sowRoles);
        }
      })
      .catch(err => {
        throw err;
      });
  },
  deleteSowRole({ commit, state }, sowRoleId) {
    return service
      .delete(ApiUrl.DeleteSowRole, { Id: sowRoleId })
      .then(() => {
        const sowRoles = cloneDeep(state.sowRoles);
        const deletedIndex = findIndex(sowRoles, item => item.id === sowRoleId);

        if (deletedIndex >= 0) {
          sowRoles.splice(deletedIndex, 1);

          commit("UPDATE_SOW_ROLES", sowRoles);
        }
      })
      .catch(err => {
        throw err;
      });
  },
  dowloadSow(context, sow) {
    const options = {
      responseType: "arraybuffer"
    };

    return service
      .get(ApiUrl.DownloadSow, { Id: sow.id }, options)
      .then(data => {
        const blob = new Blob([data], { type: "application/xlsx" });
        const filename = `SoW_${sow.name}_${format(
          new Date(),
          XLS_DOWNLOAD_DATE
        )}.xlsx`;

        saveAs(blob, filename);
      })
      .catch(err => {
        throw err;
      });
  },
  createNewVersion(context, parentId) {
    return service
      .post(ApiUrl.CreateSowNewVersion, null, { parentId })
      .then(res => {
        return res.result;
      });
  },
  getAllBillingRate({ commit }, params) {
    params = {
      BillType: params.billingType,
      ResourceRole: params.roleName,
      RateType: params.rateType,
      Currency: params.currency,
      EffectiveDate: new Date()
    };

    return service
      .get(ApiUrl.GetAllBillingRate, params)
      .then(res => {
        commit("UPDATE_BILLINGS", res.result.items);
        return res.result;
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
