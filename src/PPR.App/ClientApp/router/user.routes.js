function page(path) {
  return () =>
    import( /* webpackChunkName: '' */ `@/pages/${path}`).then(
      m => m.default || m
    );
}

export default [
  {
    path: "/user/dashboard",
    name: "dashboard",
    component: page("user/Dashboard.vue"),
    meta: {
      authorize: ["User"]
    }
  },
  {
    path: "*",
    component: page("404.vue")
  }
];
