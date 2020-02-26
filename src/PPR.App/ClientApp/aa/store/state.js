let state = {
  globals: {
    keyword: "",
    searchType: "Partial",
    searchCounts: {
      profile: 0,
      contacts: 0,
      messengers: 0,
      sms: 0,
      mobile_attachments: 0,
      emails: 0,
      tagged: 0,
      untagged_files: 0,
      untagged_ppts: 0
    },
    lang: "en"
  },

  profile: {
    viewType: "card"
  },
  mobile_attachments: {
    viewType: "card"
  },
  reports: {
    untagged: {
      files: {
        viewType: "card"
      },
      ppts: {
        viewType: "card"
      }
    }
  }
};
export default state;
