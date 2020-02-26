let getters = {
  globals: state => {
    return state.globals;
  },
  profile: state => {
    return state.profile;
  },
  mobile_attachments: state => {
    return state.mobile_attachments;
  },
  untagged_files: state => {
    return state.reports.untagged.files;
  },
  untagged_ppts: state => {
    return state.reports.untagged.ppts;
  }
};
export default getters;
