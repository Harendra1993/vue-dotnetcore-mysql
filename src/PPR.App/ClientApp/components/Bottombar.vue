<template>
  <div class="footer shadow-md-up" style="position:fixed !important;">
    <!--<div class="pull-right">
        10GB of <strong>250GB</strong> Free.
    </div>-->
    <div class="pull-right">
      <a @click="showReleaseNotes" :style="{color:link_color}">
        <strong>{{data.version}}</strong>
      </a>
    </div>

    <!-- <div>
      <strong>Copyright</strong>
      GENIE SEARCH &copy; 2017-{{
      new Date().getFullYear()
      }}
    </div>-->
  </div>
</template>

<script>
import json from "@/releases.json";

export default {
  props: {
    link_color: ""
  },

  data() {
    return {
      data: json[0]
    };
  },
  methods: {
    showReleaseNotes() {
      this.$emit("onShowReleaseNotes");
    }
  }
};
</script>

<style scoped>
.exact-active {
  font-weight: bold;
  font-size: 16px;
}
</style>