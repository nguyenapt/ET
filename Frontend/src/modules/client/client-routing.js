import { MODULE_ENTRY_ROLES } from "../../core/config";

const ClientPage = () =>
  import(/* webpackChunkName: "client" */ "./client.vue");
const ClientListPage = () =>
  import(/* webpackChunkName: "client" */ "./client-list.vue");

export default [
  {
    path: "/client",
    component: ClientPage,
    children: [
      {
        path: "",
        name: "ClientList",
        component: ClientListPage,
        meta: {
          title: "Client List Page",
          entryRoles: MODULE_ENTRY_ROLES.CLIENT
        }
      }
    ]
  }
];
