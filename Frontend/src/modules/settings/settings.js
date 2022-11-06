import Service from "../../core/service";
import { ApiUrl } from "../../core/api-url";
let service = new Service("settings", this);
/** State Definition */
const state = {
  settingDefinitions: []
};

/** Getters - Return State */
const getters = {
  settingDefinitions(state) {
    return state.settingDefinitions;
  }
};

/** Mutations - Synchronous */
const mutations = {
  ["GET_SETTINGDINITIONS"](state, settingDefinitions) {
    state.settingDefinitions = settingDefinitions;
  }
};

/** Actions - Asynchronous */
const actions = {
  getSettings({ commit }) {
    return service.get(ApiUrl.GetSiteSettingDefinitions).then(
      res => {
        if (res.result) {
          commit("GET_SETTINGDINITIONS", res.result);
        } else {
          commit("GET_SETTINGDINITIONS", []);
        }
        return res.result;
      },
      err => {
        throw err;
      }
    );
  },
  changeSettings(context, { dics } = {}) {
    var parsedobj = JSON.parse(JSON.stringify(dics));
    return service
      .post(ApiUrl.ChangeSiteSettingDefinitions, parsedobj)
      .then(res => {
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
