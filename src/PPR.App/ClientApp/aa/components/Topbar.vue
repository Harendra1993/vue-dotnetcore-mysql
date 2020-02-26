<template>
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
        <!-- <ul class="nav navbar-top-links navbar-right">
          <li>
            <a title="Switch between English/Arabic" id="lang">
              <b>En</b>
              <i class></i>
              <b>Ar</b>
            </a>
          </li>
        </ul>-->
        <div class="col-md-6 col-sm-8 col-xs-11 navbar-left">
          <search-box></search-box>
        </div>
        <menu-items></menu-items>
      </div>
    </nav>
  </div>
</template>

<script>
import SearchBox from "./SearchBox";
import MenuItems from "./MenuItems";
export default {
  components: {
    SearchBox,
    MenuItems
  }
};
</script>
<style lang="css" scoped>
.logo {
  height: 60px;
  width: 60px;
  padding-bottom: 30px;
}
</style>