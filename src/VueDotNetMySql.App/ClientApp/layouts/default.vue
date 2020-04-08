<template>
  <div>
    <topbar></topbar>
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
import Topbar from "@/components/Topbar";
import Bottombar from "@/components/Bottombar";
import ReleaseNotes from "@/pages/ReleaseNotes";

export default {
  name: "DefaultLayout",

  components: {
    Topbar,
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

<style lang="css">
body {
  font-family: "open sans", "Helvetica Neue", Helvetica, Arial, sans-serif;
  background-color: #f3f3f4;
  font-size: 13px;
  color: #676a6c;
}
</style>
