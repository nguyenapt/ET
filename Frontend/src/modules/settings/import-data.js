import Service from "../../core/service";
let service = new Service("importdata", this);
/** State Definition */
const state = {};

/** Getters - Return State */
const getters = {};

/** Mutations - Synchronous */
const mutations = {};

/** Actions - Asynchronous */
const actions = {
  importDatas(context, { link, file } = {}) {
    var formData = new FormData();
    formData.append("file", file);
    return service
      .post(link, formData, {
        headers: {
          "Content-Type": "multipart/form-data"
        }
      })
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
