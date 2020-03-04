function page(path) {
  return () =>
    import( /* webpackChunkName: '' */ `@/pages/${path}`).then(
      m => m.default || m
    );
}

export default [{
    path: "/",
    name: "login",
    component: page("Login.vue")
  },
  {
    path: "/dashboard",
    name: "dashboard",
    component: page("user/Dashboard.vue")
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
