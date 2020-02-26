import { mapGetters } from "vuex";

export const countMixin = {
  computed: {
    ...mapGetters(["globals"]),
    requestParms() {
      const vm = this;
      return {
        keyword: vm.globals.keyword,
        type: vm.globals.searchType
      };
    }
  },
  mounted() {
    const vm = this;
    vm.profileCounts();
    vm.contactsCounts();
    vm.messengersCounts();
    vm.smsCounts();
    vm.emailsCounts();
    vm.mobileAttachmentsCounts();
    vm.taggedCounts();
    vm.untaggedFilesCounts();
    vm.untaggedPptsCounts();
  },
  methods: {
    profileCounts() {
      const vm = this;
      vm.helpers
        .post("/api/count/profile", {
          ...vm.requestParms
        })
        .then(({ data }) => {
          vm.$store.commit("CHANGE_COUNTS_PROFILE", data.count);
        });
    },
    contactsCounts() {
      const vm = this;
      vm.helpers
        .post("/api/count/contacts", {
          ...vm.requestParms
        })
        .then(({ data }) => {
          vm.$store.commit("CHANGE_COUNTS_CONTACTS", data.count);
        });
    },
    messengersCounts() {
      const vm = this;
      vm.helpers
        .post("/api/count/messengers", {
          ...vm.requestParms
        })
        .then(({ data }) => {
          vm.$store.commit("CHANGE_COUNTS_MOBILE_MESSENGERS", data.count);
        });
    },
    smsCounts() {
      const vm = this;
      vm.helpers
        .post("/api/count/sms", {
          ...vm.requestParms
        })
        .then(({ data }) => {
          vm.$store.commit("CHANGE_COUNTS_MOBILE_SMS", data.count);
        });
    },
    emailsCounts() {
      const vm = this;
      vm.helpers
        .post("/api/count/emails", {
          ...vm.requestParms
        })
        .then(({ data }) => {
          vm.$store.commit("CHANGE_COUNTS_EMAILS", data.count);
        });
    },
    mobileAttachmentsCounts() {
      const vm = this;
      vm.helpers
        .post("/api/count/mobile_attachments", {
          ...vm.requestParms
        })
        .then(({ data }) => {
          vm.$store.commit("CHANGE_COUNTS_MOBILE_ATTACHMENTS", data.count);
        });
    },
    taggedCounts() {
      const vm = this;
      vm.helpers
        .post("/api/count/tagged", {
          ...vm.requestParms
        })
        .then(({ data }) => {
          vm.$store.commit("CHANGE_COUNTS_TAGGED_REPORTS", data.count);
        });
    },
    untaggedFilesCounts() {
      const vm = this;
      vm.helpers
        .post("/api/count/untagged_files", {
          ...vm.requestParms
        })
        .then(({ data }) => {
          vm.$store.commit("CHANGE_COUNTS_UNTAGGED_REPORTS_FILES", data.count);
        });
    },
    untaggedPptsCounts() {
      const vm = this;
      vm.helpers
        .post("/api/count/untagged_ppts", {
          ...vm.requestParms
        })
        .then(({ data }) => {
          vm.$store.commit("CHANGE_COUNTS_UNTAGGED_REPORTS_PPTS", data.count);
        });
    }
  }
};
