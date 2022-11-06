import { JwtHelperService } from "./jwthelper.service";
import * as CryptoJS from "crypto-js";
import { TokenKey } from "./config";

const algorithm = "HS256";
const _expiredTime = Math.floor(Date.now() / 1000) + 60 * 60; // 1 hour

export const JwtHelper = {
  base64url(source) {
    // Encode in classical base64
    let encodedSource = CryptoJS.enc.Base64.stringify(source);

    // Remove padding equal characters
    encodedSource = encodedSource.replace(/=+$/, "");

    // Replace characters according to base64url specifications
    encodedSource = encodedSource.replace(/\+/g, "-");
    encodedSource = encodedSource.replace(/\//g, "_");

    return encodedSource;
  },
  createUnsignedToken(data, expiredTime) {
    let header = {
      alg: algorithm,
      typ: "JWT"
    };
    let exp = expiredTime ? expiredTime : _expiredTime;
    let stringifiedHeader = CryptoJS.enc.Utf8.parse(JSON.stringify(header));
    let encodedHeader = this.base64url(stringifiedHeader);
    let jwtData = { ...data, exp: exp };
    let stringifiedData = CryptoJS.enc.Utf8.parse(JSON.stringify(jwtData));
    let encodedData = this.base64url(stringifiedData);

    let token = encodedHeader + "." + encodedData;

    return token;
  },
  createSigningToken(data, expiredTime) {
    let token = this.CreateUnsignedToken(data, expiredTime);
    let secret = "My very confidential secret!";

    let signature = CryptoJS.HmacSHA256(token, secret);
    signature = this.base64url(signature);

    let signedToken = token + "." + signature;
    return signedToken;
  },
  decodeToken(token) {
    if (token == null) {
      return null;
    }
    try {
      let tokenPayload = JwtHelperService.decodeToken(token);
      if (tokenPayload) {
        return tokenPayload;
      }
    } catch (error) {
      return null;
    }
  },
  isExpired(key) {
    let token = localStorage.getItem(key);
    return JwtHelperService.isTokenExpired(token);
  },
  isAuthenticated() {
    return !this.isExpired(TokenKey.AuthToken);
  }
};
