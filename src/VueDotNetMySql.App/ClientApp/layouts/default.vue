<template>
  <div>
    <topbar></topbar>
    <div class="wrapper wrapper-content">
      <div class="row">
        <div class="col-md-12">
          <child />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Topbar from "@/components/Topbar";
import Bottombar from "@/components/Bottombar";

export default {
  name: "DefaultLayout",

  components: {
    Topbar,
    Bottombar
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
