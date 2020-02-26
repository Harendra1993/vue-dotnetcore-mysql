function page(path) {
  return () =>
    import( /* webpackChunkName: '' */ `@/pages/${path}`).then(
      m => m.default || m
    );
}

export default [{
    path: "/",
    name: "home",
    component: page("home/Home.vue")
  },
  {
    path: "/contacts",
    name: "contacts",
    component: page("contacts/Contacts.vue")
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
