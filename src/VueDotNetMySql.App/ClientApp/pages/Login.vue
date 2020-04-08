<template>
  <div class="h-body">
    <release-notes ref="popup" style="color:black;"></release-notes>
    <ul class="nav nav-pills">
      <!-- <li>
        <a target="_blank" class="firstsbuttons"></a>
      </li>-->
    </ul>
    <div class="page">
      <div
        class="middle-box text-center loginscreen animated fadeInDown"
        style="padding-top: 15%;width: 350px;"
      >
        <div>
          <center style="  color: white;">
            <h2 class="font-bold">Welcome to VueDotNetMySql</h2>
            <p>Login in. To see it in action.</p>
          </center>

          <form class="m-t" role="form" @submit.prevent="handleLogin">
            <div class="form-group" :class="{ 'has-error': submitted && $v.user.username.$error }">
              <input
                v-model.trim="$v.user.username.$model"
                type="text"
                name="username"
                class="form-control"
                placeholder="Enter Username"
              />
              <div v-if="submitted && !$v.user.username.required" class="help-block with-errors">
                <b>Username is required!</b>
              </div>
            </div>
            <div class="form-group" :class="{ 'has-error': submitted && $v.user.password.$error }">
              <input
                v-model.trim="$v.user.password.$model"
                type="password"
                name="password"
                class="form-control"
                placeholder="Enter Password"
              />
              <div v-if="submitted && !$v.user.password.required" class="help-block with-errors">
                <b>Password is required!</b>
              </div>
            </div>
            <button class="btn btn-primary block full-width m-b" :disabled="loading">
              <i class="fas fa-spinner-third fa-spin" v-if="loading"></i>
              <span>Login</span>
            </button>
            <div class="form-group has-error">
              <div v-if="message" class="help-block with-errors">
                <b>{{ message }}</b>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
    <bottombar style="background-color:transparent; color:white; border:none;" />
  </div>
</template>
<script>
import { required } from "vuelidate/lib/validators";

import Bottombar from "@/components/Bottombar";

export default {
  layout: "blank",
  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      loading: false,
      submitted: false,
      message: ""
    };
  },
  validations: {
    user: {
      username: { required },
      password: { required }
    }
  },
  components: {
    Bottombar
  },
  computed: {
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    }
  },
  created() {
    if (this.loggedIn) {
      this.$router.push("/dashboard");
    }
  },

  methods: {
    handleLogin() {
      this.submitted = true;

      // stop here if form is invalid
      this.$v.$touch();
      if (this.$v.$invalid) {
        return;
      }

      this.loading = true;
      if (this.user.username && this.user.password) {
        this.$store.dispatch("auth/login", this.user).then(
          data => {
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
  }
};
</script>
<style scoped>
.h-body {
  font-family: "open sans", "Helvetica Neue", Helvetica, Arial, sans-serif;
  font-size: 13px;
  background-image: url(/img/start/1.jpg);
  background-position: center center;
  background-repeat: no-repeat;
  background-attachment: fixed;
  background-size: cover;
  height: 100vh;
  width: 100wh;
  margin-top: -15px;
  padding: 0;
}

.left {
  float: left;
}

.nav > li > a {
  padding: 0;
  color: #000;
  opacity: 0.75;
}

.nav > li > .colorfix {
  opacity: 1;
}

.nav > li > a:hover,
.nav > li > a:visited,
.nav > li > a:focus {
  background-color: white;
  text-decoration: underline;
}

.nav-pills {
  margin: 15px 5px;
}

.nav li {
  float: right;
  margin-right: 15px;
}

.firstsbuttons {
  margin-top: 4.5px;
}

.nav > li > a > img {
  opacity: 0.75;
}

.nav > li > a > img:hover {
  opacity: 1;
  cursor: default;
}
</style>
