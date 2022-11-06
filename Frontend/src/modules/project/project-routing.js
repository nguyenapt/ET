import { MODULE_ENTRY_ROLES } from "../../core/config";

const ProjectPage = () =>
  import(/* webpackChunkName: "project" */ "./project.vue");
const ProjectListPage = () =>
  import(/* webpackChunkName: "project" */ "./project-list.vue");

export default [
  {
    path: "/project",
    component: ProjectPage,
    children: [
      {
        path: "",
        name: "ProjectList",
        component: ProjectListPage,
        meta: {
          title: "Project List Page",
          entryRoles: MODULE_ENTRY_ROLES.PROJECT
        }
      }
    ]
  }
];
