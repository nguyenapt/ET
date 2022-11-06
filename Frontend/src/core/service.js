import axios from "axios";
import { JwtHelper } from "../core/jwt-helper";
import { TokenKey } from "./config";

// this the apiBaseUrl will be set based on octopus env variable
var apiBaseUrl = "#{WebsiteName.DataServices}";
if (encodeURIComponent(apiBaseUrl).indexOf("%23%7B") != -1) {
  // DEV
  apiBaseUrl = "http://et.provideithere:8001/";

  // UAT
  // apiBaseUrl = "http://et.provideithere.se:8003/";
}

const isHandlerEnabled = true;

const requestHandler = request => {
  if (isHandlerEnabled) {
    const token = localStorage.getItem(TokenKey.AuthToken);
    if (JwtHelper.isAuthenticated()) {
      request.headers.common["Authorization"] = "Bearer " + token;
    }
    // if (!request.headers.common.has('Content-Type') && !(request.body instanceof FormData)) {
    //     request.headers.common['Content-Type'] = 'application/json';
    // }
    request.headers.common["Accept"] = "application/json";
  }
  return request;
};

const successHandler = response => {
  if (isHandlerEnabled) {
    //TODO: Do Success Handler
  }
  return response;
};

const errorHandler = error => {
  if (isHandlerEnabled) {
    //TODO: Do Error Handler
  }
  console.log(error);
  return Promise.reject(error);
};

export default class Service {
  // eslint-disable-next-line
  constructor(namespace, vm, socketOpts) {
    this.namespace = namespace;
    this.axios = axios.create({
      baseURL: apiBaseUrl,
      responseType: "json"
    });

    //Enable request interceptor
    this.axios.interceptors.request.use(
      request => requestHandler(request),
      error => errorHandler(error)
    );

    //Response and Error handler
    this.axios.interceptors.response.use(
      response => successHandler(response),
      error => errorHandler(error)
    );
  }

  /**
   * Get Http Request
   * @param {any} action
   * @param {any} params
   * @param {any} options
   */
  get(action, params, options) {
    return new Promise((resolve, reject) => {
      this.axios
        .request(action, {
          method: "GET",
          params,
          ...options
        })
        .then(response => {
          if (response.data) {
            resolve(response.data);
          } else {
            reject(response);
          }
        })
        .catch(error => {
          if (
            error.response &&
            error.response.data &&
            error.response.data.error
          ) {
            console.error("REST request error!", error.response.data.error);
            reject(error.response.data.error);
          } else reject(error);
        });
    });
  }

  /**
   * Post Http Request
   * @param {any} action
   * @param {any} data
   * @param {any} params
   */
  post(action, data, params) {
    return new Promise((resolve, reject) => {
      this.axios
        .request(action, {
          method: "POST",
          data,
          params
        })
        .then(response => {
          if (response.data) {
            resolve(response.data);
          } else {
            reject(response);
          }
        })
        .catch(error => {
          if (
            error.response &&
            error.response.data &&
            error.response.data.error
          ) {
            console.error("REST request error!", error.response.data.error);
            reject(error.response.data.error);
          } else reject(error);
        });
    });
  }

  /**
   * Put Http Request
   * @param {any} action
   * @param {any} params
   */
  put(action, params) {
    return new Promise((resolve, reject) => {
      this.axios
        .request(action, {
          method: "PUT",
          data: params
        })
        .then(response => {
          if (response.data) {
            resolve(response.data);
          } else {
            reject(response);
          }
        })
        .catch(error => {
          if (
            error.response &&
            error.response.data &&
            error.response.data.error
          ) {
            console.error("REST request error!", error.response.data.error);
            reject(error.response.data.error);
          } else reject(error);
        });
    });
  }

  /**
   * Put Http Request
   * @param {any} action
   * @param {any} params
   */
  delete(action, params) {
    return new Promise((resolve, reject) => {
      this.axios
        .request(action, {
          method: "DELETE",
          params
        })
        .then(response => {
          if (response.data) {
            resolve(response.data);
          } else {
            reject(response);
          }
        })
        .catch(error => {
          if (
            error.response &&
            error.response.data &&
            error.response.data.error
          ) {
            console.error("REST request error!", error.response.data.error);
            reject(error.response.data.error);
          } else reject(error);
        });
    });
  }
}
