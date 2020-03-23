function page(path) {
  return () =>
    import( /* webpackChunkName: '' */ `@/pages/${path}`).then(
      m => m.default || m
    );
}

export default [{
    path: "/account/users",
    name: "users",
    component: page("account/ListUsers.vue"),
    meta: {
      authorize: ["Super Admin", "Admin"]
    }
  },

  {
    path: "*",
    component: page("404.vue")
  }
];
