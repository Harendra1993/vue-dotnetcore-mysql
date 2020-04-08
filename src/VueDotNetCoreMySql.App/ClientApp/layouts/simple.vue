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

          <a href="/" class="navbar-brand">VueDotNetCoreMySql</a>
        </div>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="nav navbar-top-links navbar-right">
            <li>
              <a @click="handleLogout">
                <i class="fa fa-sign-out"></i> Logout
              </a>
            </li>
          </ul>
          <div class="col-md-6 col-sm-8 col-xs-11 navbar-left"></div>
        </div>
      </nav>
    </div>
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
import Bottombar from "@/components/Bottombar";

export default {
  name: "SimpleLayout",

  components: {
    Bottombar
  },
  methods: {
    handleLogout() {
      this.$store.dispatch("auth/logout").then(
        () => {
          this.$router.push("/");
        },
        error => {
          this.loading = false;
          this.message =
            (error.response && error.response.data) ||
            error.message ||
            error.toString();
        }
      );
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
