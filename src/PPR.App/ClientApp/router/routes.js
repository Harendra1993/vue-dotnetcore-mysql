import admin from "./admin.routes";
import superadmin from "./super.routes";
import user from "./user.routes";

function page(path) {
  return () =>
    import( /* webpackChunkName: '' */ `@/pages/${path}`).then(
      m => m.default || m
    );
}

const routes = [
  {
    path: "/",
    name: "home",
    component: page("Home.vue"),
    meta: {
      authorize: []
    }
  }, {
    path: "/login",
    name: "login",
    component: page("Login.vue")
  },
  {
    path: "/release-notes",
    name: "release",
    component: page("ReleaseNotes.vue")
  },
  {
    path: "/test",
    name: "test",
    component: page("Test.vue")
  },

  {
    path: "*",
    component: page("404.vue")
  }
];

export default [...routes, ...admin, ...superadmin, ...user]
