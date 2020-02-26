require("./bootstrap");

// Installed modules import
import Vue from "vue";
import * as uiv from "uiv";

// import VueSplit from "vue-split-panel";

// Custom modules import
import store from "../js/store/index";
import router from "@/router";
import App from "@/components/App";

import "@/components";

Vue.use(uiv);

/** Vue JS Configurations */
Vue.config.productionTip = false;

Vue.config.devtools = true;

/** Rendering  */
new Vue({
  store,
  router,
  ...App
});
