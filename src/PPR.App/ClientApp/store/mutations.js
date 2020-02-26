let mutations = {
  CHANGE_KEYWORD(state, payload) {
    state.globals.keyword = payload;
  },
  CHANGE_SEARCH_TYPE(state, payload) {
    state.globals.searchType = payload;
  },
  CHANGE_LANG(state, payload) {
    state.globals.lang = payload;
  },
  CHANGE_PROFILE_VIEW(state, payload) {
    state.profile.viewType = payload;
  },
  CHANGE_MOBILE_ATTACHMENTS_VIEW(state, payload) {
    state.mobile_attachments.viewType = payload;
  },
  CHANGE_UNTAGGED_FILES_VIEW(state, payload) {
    state.reports.untagged.files.viewType = payload;
  },
  CHANGE_UNTAGGED_PPTS_VIEW(state, payload) {
    state.reports.untagged.ppts.viewType = payload;
  },

  // For changing counts
  CHANGE_COUNTS_PROFILE(state, payload) {
    state.globals.searchCounts.profile = payload;
  },
  CHANGE_COUNTS_CONTACTS(state, payload) {
    state.globals.searchCounts.contacts = payload;
  },
  CHANGE_COUNTS_MOBILE_MESSENGERS(state, payload) {
    state.globals.searchCounts.messengers = payload;
  },
  CHANGE_COUNTS_MOBILE_SMS(state, payload) {
    state.globals.searchCounts.sms = payload;
  },
  CHANGE_COUNTS_MOBILE_ATTACHMENTS(state, payload) {
    state.globals.searchCounts.mobile_attachments = payload;
  },
  CHANGE_COUNTS_EMAILS(state, payload) {
    state.globals.searchCounts.emails = payload;
  },
  CHANGE_COUNTS_TAGGED_REPORTS(state, payload) {
    state.globals.searchCounts.tagged = payload;
  },
  CHANGE_COUNTS_UNTAGGED_REPORTS_FILES(state, payload) {
    state.globals.searchCounts.untagged_files = payload;
  },
  CHANGE_COUNTS_UNTAGGED_REPORTS_PPTS(state, payload) {
    state.globals.searchCounts.untagged_ppts = payload;
  }
};
export default mutations;
