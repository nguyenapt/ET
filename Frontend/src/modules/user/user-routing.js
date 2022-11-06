import { MODULE_ENTRY_ROLES } from "../../core/config";

const UserPage = () => import(/* webpackChunkName: "user" */ "./user.vue");
const UserListPage = () =>
  import(/* webpackChunkName: "user" */ "./user-list.vue");
const EditUserPage = () =>
  import(/* webpackChunkName: "user" */ "./user-edit.vue");

export default [
  {
    path: "/user",
    component: UserPage,
    children: [
      {
        path: "",
        name: "SearchUser",
        component: UserListPage,
        meta: {
          entryRoles: MODULE_ENTRY_ROLES.USER,
          title: "User List Page"
        }
      },
      {
        path: "edit/:id",
        name: "EditUser",
        component: EditUserPage,
        meta: {
          entryRoles: MODULE_ENTRY_ROLES.USER,
          title: "User Edit Page"
        }
      }
    ]
  }
];
