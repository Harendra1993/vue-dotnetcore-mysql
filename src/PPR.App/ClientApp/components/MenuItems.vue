<template>
  <ul class="nav navbar-top-links">
    <li>
      <router-link to="/dashboard">
        <i class="fa fa-users"></i> Dashboard
      </router-link>
    </li>
    <li>
      <router-link to="/upload">
        <i class="fa fa-address-book"></i> Upload
      </router-link>
    </li>

    <li>
      <router-link to="/actions">
        <i class="fa fa-comments"></i> View / Edit
      </router-link>
    </li>

    <li class="dropdown">
      <a class="dropdown-toggle" id="untagged" data-toggle="dropdown" href="#">
        <i class="fa fa-folder"></i>
        Analysis
        <i class="caret"></i>
      </a>

      <ul class="dropdown-menu">
        <li>
          <router-link to="/analysis">
            <i class="fa fa-file-search"></i> Overall Analysis
          </router-link>
        </li>

        <li>
          <router-link to="/team-chart">
            <i class="fa fa-file-powerpoint"></i> Team Chart
          </router-link>
        </li>

        <li>
          <router-link to="/team-chart">
            <i class="fa fa-file-powerpoint"></i> Assignment Chart
          </router-link>
        </li>

        <li>
          <router-link to="/team-chart">
            <i class="fa fa-file-powerpoint"></i> Scorecard
          </router-link>
        </li>
      </ul>
    </li>

    <ul class="nav navbar-top-links navbar-right">
      <li>
        <a @click="handleLogout"> <i class="fa fa-sign-out"></i> Logout </a>
      </li>
    </ul>
  </ul>
</template>

<script>
export default {
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

<style></style>
