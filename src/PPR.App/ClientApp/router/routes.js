function page(path) {
  return () =>
    import( /* webpackChunkName: '' */ `@/pages/${path}`).then(
      m => m.default || m
    );
}

export default [{
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
  path: "/admin/dashboard",
  name: "dashboard",
  component: page("admin/Dashboard.vue"),
  meta: {
    authorize: ["Admin"]
  }
}, {
  path: "/user/dashboard",
  name: "dashboard",
  component: page("user/Dashboard.vue"),
  meta: {
    authorize: ["User"]
  }
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
