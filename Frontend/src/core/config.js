export const UserRole = {
  Admin: 1,
  PM: 2,
  DM: 3
};

export const IsProduction = process.env.NODE_ENV === "production";

export const TokenKey = {
  AuthToken: "auth.token"
};
export const CurrentUserId = "CurrentUserId";
export const CurrentUserRoles = "CurrentUserRoles";

export const ROLES = {
  ADMIN: "Admin",
  PMO: "PMO",
  DM: "DM",
  PM: "PM",
  HR: "HR",
  EMPLOYEE: "Employee"
};

export const MODULE_ENTRY_ROLES = {
  ["404"]: [],
  ["403"]: [],
  CLIENT: [ROLES.ADMIN, ROLES.PMO],
  HOME: [],
  LOGIN: [],
  PROFILE: [ROLES.ADMIN, ROLES.PMO],
  PROJECT: [ROLES.ADMIN, ROLES.PMO],
  SETTINGS: [ROLES.ADMIN, ROLES.PMO],
  SOW: [ROLES.ADMIN, ROLES.PMO],
  USER: [ROLES.ADMIN, ROLES.PMO]
};
