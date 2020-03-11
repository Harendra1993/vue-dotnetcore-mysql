<template> </template>
<script>
import Bottombar from "@/components/Bottombar";
import ReleaseNotes from "@/pages/ReleaseNotes";

export default {
  layout: "blank",
  data() {
    return {
      user: { username: "", password: "" },
      loading: false,
      message: "",
      time: "",
      date: ""
    };
  },
  computed: {
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
    roles() {
      return this.$store.state.auth.user.userRoles;
    }
  },
  created() {},

  mounted() {
    const vm = this;
    if (this.loggedIn) {
      if (this.roles.includes("Super")) this.$router.push("/super/dashboard");
      else if (this.roles.includes("Admin"))
        this.$router.push("/admin/dashboard");
      else if (this.roles.includes("User"))
        this.$router.push("/user/dashboard");
    }
  },

  destroyed() {
    clearInterval(this.interval);
  },

  methods: {}
};
</script>
