function page(path) {
  return () =>
    import(/* webpackChunkName: '' */ `@/pages/${path}`).then(
      m => m.default || m
    );
}

export default [
  {
    path: "/",
    name: "home",
    component: page("home/Home.vue")
  },
  {
    path: "/search",
    name: "search",
    component: page("search/Results.vue")
  },
  {
    path: "/profile",
    name: "profile",
    component: page("profile/Profile.vue")
  },
  {
    path: "/contacts",
    name: "contacts",
    component: page("contacts/Contacts.vue")
  },
  {
    path: "/messengers",
    name: "messengers",
    component: page("mobile/messengers/MessengersList.vue")
  },
  {
    path: "/sms",
    name: "sms",
    component: page("mobile/sms/SmsList.vue")
  },
  {
    path: "/attachments",
    name: "attachments",
    component: page("mobile/attachments/Attachments.vue")
  },
  {
    path: "/emails",
    name: "emails",
    component: page("emails/EmailsList.vue")
  },
  {
    path: "/emails-old",
    name: "emails",
    component: page("emails-old/EmailsList.vue")
  },
  {
    path: "/tagged",
    name: "tagged",
    component: page("reports/tagged/Tagged.vue")
  },
  {
    path: "/untagged/files",
    name: "untagged.files",
    component: page("reports/untagged/Files.vue")
  },
  {
    path: "/untagged/ppt",
    name: "untagged.ppt",
    component: page("reports/untagged/Ppt.vue")
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
