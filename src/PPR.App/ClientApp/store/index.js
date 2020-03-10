import Vue from "vue";
import Vuex from "vuex";
import createPersistedState from "vuex-persistedstate";
import Cookies from "js-cookie";

import {
  auth
} from './auth.module';


Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    auth
  },
  plugins: [
    createPersistedState({
      key: "p_persist",
      storage: {
        getItem: key => Cookies.get(key),
        setItem: (key, value) =>
          Cookies.set(key, value, {
            expires: 2147483647,
            secure: false
          }),
        removeItem: key => Cookies.remove(key)
      }
    })
  ]
});
