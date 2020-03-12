function page(path) {
  return () =>
    import( /* webpackChunkName: '' */ `@/pages/${path}`).then(
      m => m.default || m
    );
}

export default [
  {
    path: "/admin/dashboard",
    name: "dashboard",
    component: page("admin/Dashboard.vue"),
    meta: {
      authorize: ["Admin"]
    }
  },
  {
    path: "/admin/users",
    name: "users",
    component: page("admin/UsersList.vue"),
    meta: {
      authorize: ["Admin"]
    }
  },

  {
    path: "*",
    component: page("404.vue")
  }
];
