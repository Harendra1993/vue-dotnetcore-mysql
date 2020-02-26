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

          <a href="/" class="navbar-brand">
            <img class="logo" src="/img/logo/without_text.png" />
          </a>
        </div>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <!-- <ul class="nav navbar-top-links navbar-right">
            <li>
              <a title="Switch between English/Arabic" id="lang">
                <b>En</b>
                <i class></i>
                <b>Ar</b>
              </a>
            </li>
          </ul>-->
          <div class="col-md-6 col-sm-8 col-xs-11 navbar-left"></div>
        </div>
      </nav>
    </div>
    <div class="wrapper wrapper-content">
      <div class="row">
        <div class="col-md-12">
          <child />
          <release-notes ref="popup"></release-notes>
        </div>
      </div>
      <bottombar @onShowReleaseNotes="showReleaseNotes" />
    </div>
  </div>
</template>

<script>
import Bottombar from "@/components/Bottombar";
import ReleaseNotes from "@/pages/ReleaseNotes";

export default {
  name: "SimpleLayout",

  components: {
    Bottombar,
    ReleaseNotes
  },
  methods: {
    showReleaseNotes() {
      this.$refs.popup.show();
    }
  }
};
</script>
<style lang="css" scoped>
body {
  font-family: "open sans", "Helvetica Neue", Helvetica, Arial, sans-serif;
  background-color: #f3f3f4;
  font-size: 13px;
  color: #676a6c;
}
.logo {
  height: 60px;
  width: 60px;
  padding-bottom: 30px;
}
</style>
