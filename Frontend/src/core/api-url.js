export const ApiUrl = {
  /** Shared*/
  GetFormSelectors: "/api/services/app/Shared/GetFormSelectors",

  /** User*/
  Login: "/api/TokenAuth/Authenticate",
  UserGetProfile: "/api/services/app/Resource/GetResourceByUserId",
  GetResourceForEdit: "/api/services/app/Resource/GetResourceForEdit",

  /** Role */
  GetAllPermissions: "api/services/app/Role/GetAllPermissions",
  GetAllRoles: "/api/services/app/Role/GetAll",
  UpdateRolePermissions: "/api/services/app/Role/Update",

  /** Sow */
  CreateSow: "/api/services/app/Sow/Create",
  UpdateSow: "/api/services/app/Sow/Update",
  GetSowDetail: "/api/services/app/Sow/Get",
  GetSows: "/api/services/app/Sow/GetAll",
  DeleteSow: "/api/services/app/Sow/Delete",
  CreateSowNewVersion: "/api/services/app/Sow/CreateNewVersion",
  DownloadSow: "FileDownload/SoWXlsx",

  /** Sow-role */
  GetSowRoleDetailList: "/api/services/app/SOWRole/GetSoWRoleDetailList",
  CreateSowRole: "/api/services/app/SOWRole/Create",
  UpdateSowRole: "/api/services/app/SOWRole/Update",
  DeleteSowRole: "/api/services/app/SOWRole/Delete",

  /** Client */
  CreateClient: "/api/services/app/Client/Create",
  UpdateClient: "/api/services/app/Client/Update",
  GetClientDetail: "/api/services/app/Client/Get",
  GetClients: "/api/services/app/Client/GetAll",
  DeleteClient: "/api/services/app/Client/Delete",

  /** Project */
  CreateProject: "/api/services/app/Project/Create",
  UpdateProject: "/api/services/app/Project/Update",
  GetProjectDetail: "/api/services/app/Project/Get",
  GetProjects: "/api/services/app/Project/GetAll",
  DeleteProject: "/api/services/app/Project/Delete",

  /** Access rights */
  GetAllUsers: "/api/services/app/User/GetAll",
  GetUserById: "/api/services/app/User/Get",
  GetUserRoles: "/api/services/app/User/GetRoles",
  UpdateUser: "/api/services/app/User/Update",
  UpdateResource: "/api/services/app/Resource/Update",

  GetSiteSettingDefinitions:
    "/api/services/app/SiteSetting/GetSiteSettingDefinitions",
  ChangeSiteSettingDefinitions:
    "/api/services/app/SiteSetting/ChangeSiteSettingDefinitions",

  /** Import data */
  ImportHolidayBank: "/api/services/app/HolidayBankDataImport/ImportData",
  ImportWorkingHour: "/api/services/app/WorkingHoursDataImport/ImportData",
  ImportRateForRole: "/api/services/app/BillingRateDataImport/ImportData",
  ImportBeneficialInformation:
    "/api/services/app/BeneficiaryInformationDataImport/ImportData",
  ImportResourceRole: "/api/services/app/ResourceRoleDataImport/ImportData",
  ImportResource: "/api/services/app/ResourceDataImport/ImportData",

  /** Access rights */
  GetAllBillingRate: "/api/services/app/BillingRate/GetAll"
};
