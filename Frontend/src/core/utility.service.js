import store from "../core/store";

export const UtilityService = {
  checkRoleEntryRight(entryRoles) {
    const currentUserRoles = store.getters["auth/currentUserRoles"];

    if (!entryRoles || !entryRoles.length) {
      return true;
    }

    if (!currentUserRoles || !currentUserRoles.length) {
      return false;
    }

    return entryRoles.some(role => currentUserRoles.indexOf(role) >= 0);
  }
};
