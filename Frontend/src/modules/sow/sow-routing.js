import { MODULE_ENTRY_ROLES } from "../../core/config";

const SowPage = () => import(/* webpackChunkName: "sow" */ "./sow.vue");
const SowListPage = () =>
  import(/* webpackChunkName: "sow" */ "./sow-list.vue");
const SowDetailPage = () =>
  import(/* webpackChunkName: "sow" */ "./sow-detail.vue");
const SowEditPage = () =>
  import(/* webpackChunkName: "sow" */ "./sow-edit.vue");

export default [
  {
    path: "/sow",
    component: SowPage,
    children: [
      {
        path: "",
        name: "SowList",
        component: SowListPage,
        meta: {
          title: "Sow List Page",
          entryRoles: MODULE_ENTRY_ROLES.SOW
        }
      },
      {
        path: ":id",
        name: "SowDetail",
        component: SowDetailPage,
        meta: {
          title: "Sow Detail Page",
          entryRoles: MODULE_ENTRY_ROLES.SOW
        }
      },
      {
        path: "edit/:id",
        name: "SowEdit",
        component: SowEditPage,
        meta: {
          title: "Sow Edit Page",
          entryRoles: MODULE_ENTRY_ROLES.SOW
        }
      }
    ]
  }
];
