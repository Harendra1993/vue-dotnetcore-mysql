<template>
  <div>
    <div class="row border-bottom white-bg">
      <nav class="navbar navbar-static-top shadow-md-down" role="navigation">
        <div class="navbar-header">
          <button
            aria-controls="navbar"
            aria-expanded="false"
            data-target="#navbar"
            data-toggle="collapse"
            class="navbar-toggle collapsed"
            type="button"
          >
            <i class="fa fa-reorder"></i>
          </button>

          <router-link to="/" class="navbar-brand">
            <img class="logo" src="/img/logo/without_text.png" />
          </router-link>
        </div>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <div class="col-md-6 col-sm-8 col-xs-11 navbar-left">
            <search-box></search-box>
          </div>
        </div>
      </nav>
    </div>
    <div class="wrapper wrapper-content animated">
      <div class="row">
        <div class="col-lg-7 col-lg-offset-1">
          <h2>
            {{totalCounts}} results found for:
            <span class="text-navy">“{{globals.keyword}}”</span>
          </h2>
          <!-- <small>Request time (0.23 seconds)</small> -->
          <statistics />
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { mapGetters } from "vuex";

import SearchBox from "@/components/SearchBox";
import Bottombar from "@/components/Bottombar";
import ReleaseNotes from "@/pages/ReleaseNotes";
import Statistics from "@/pages/Statistics";

export default {
  layout: "blank",
  data() {
    return {
      keyword: this.$store.state.globals.keyword,
      type: this.$store.state.globals.searchType
    };
  },
  components: {
    SearchBox,
    Bottombar,
    ReleaseNotes,
    Statistics
  },
  computed: {
    ...mapGetters(["globals"]),
    totalCounts() {
      const vm = this;
      const obj = vm.globals.searchCounts;
      let count = 0;
      for (const value of Object.values(obj)) {
        count += value;
      }
      return count;
    }
  },
  mounted() {
    const vm = this;
  },
  destroyed() {},

  methods: {
    showReleaseNotes() {
      this.$refs.popup.show();
    }
  }
};
</script>
<style lang="css" scoped>
.logo {
  height: 60px;
  width: 60px;
  padding-bottom: 30px;
}
.text-right {
  text-align: right !important;
}
</style>