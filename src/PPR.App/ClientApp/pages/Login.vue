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
            <h2 class="font-bold">Welcome to PPR</h2>
            <p>Login in. To see it in action.</p>
          </center>

          <form class="m-t" role="form" @submit.prevent="handleLogin">
            <div class="form-group">
              <input
                v-model="user.username"
                v-validate="'required'"
                type="text"
                name="username"
                class="form-control"
                placeholder="Enter Username"
              />
              <div
                v-if="errors.has('username')"
                class="alert alert-danger"
                role="alert"
              >
                Username is required!
              </div>
            </div>
            <div class="form-group">
              <input
                v-model="user.password"
                v-validate="'required'"
                type="password"
                name="password"
                class="form-control"
                placeholder="Enter Password"
              />
              <div
                v-if="errors.has('password')"
                class="alert alert-danger"
                role="alert"
              >
                Password is required!
              </div>
            </div>
            <button
              class="btn btn-primary block full-width m-b"
              :disabled="loading"
            >
              <span
                v-show="loading"
                class="spinner-border spinner-border-sm"
              ></span>
              <span>Login</span>
            </button>
            <div class="form-group">
              <div v-if="message" class="alert alert-danger" role="alert">
                {{ message }}
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>

    <div class="date-time">
      <h1 class="time">{{ time }}</h1>
      <h1 class="date">{{ date }}</h1>
    </div>
    <bottombar
      @onShowReleaseNotes="showReleaseNotes"
      link_color="white"
      style="background-color:transparent; color:white; border:none;"
    />
  </div>
</template>
<script>
import Bottombar from "@/components/Bottombar";
import ReleaseNotes from "@/pages/ReleaseNotes";

export default {
  layout: "blank",
  data() {
    return {
      user: { username: "", password: "" },
      loading: false,
      message: "",
      time: "",
      date: ""
    };
  },
  components: {
    Bottombar,
    ReleaseNotes
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

  mounted() {
    const vm = this;
    vm.interval = setInterval(() => {
      vm.updateTimestamp();
    }, 1000);

    vm.updateTimestamp();
  },

  destroyed() {
    clearInterval(this.interval);
  },

  methods: {
    handleLogin() {
      this.loading = true;
      this.$validator.validateAll().then(isValid => {
        if (!isValid) {
          this.loading = false;
          return;
        }
        if (this.user.username && this.user.password) {
          this.$store.dispatch("auth/login", this.user).then(
            () => {
              this.$router.push("/dashboard");
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
      });
    },
    showReleaseNotes() {
      this.$refs.popup.show();
    },
    updateTimestamp() {
      var date = new Date();
      this.date = date.toGMTString().slice(0, 16);

      var hours = date.getHours();
      var minutes = date.getMinutes();
      var seconds = date.getSeconds();

      if (hours < 10) hours = "0" + hours;

      if (minutes < 10) minutes = "0" + minutes;

      //if (seconds < 10) seconds = "0" + seconds;

      this.time = hours + ":" + minutes;
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

.date-time {
  bottom: 5px;
  left: 15px;
  position: absolute;
  padding: 0;
  color: white;
}

.date {
  font-size: 4em;
  padding-left: 0.2em;
  margin-top: -5px;
}

.time {
  font-size: 10em;
}

.page {
  z-index: 9;
  text-align: center;
  width: 700px;
  left: 50%;
  top: 40%;
  position: absolute;
  -webkit-transform: translate(-50%, -50%);
  transform: translate(-50%, -50%);
  white-space: normal;
}

.search {
  max-width: 900px;
  margin: 20px auto;
}

.search input {
  height: 55px;
  border: 0px;
  border-radius: 3px;
  color: #000;
  font-family: "Open Sans", Helvetica, Arial, sans-serif;
  font-size: 18px;
  font-weight: 400;
  padding: 5px 20px;
}

.search .form-control:hover {
  border: 1px solid #b9b9b9;
  border-top-color: #a0a0a0;
}

.search .form-control:focus {
  outline: none;
  border: 2px solid #bb8b30;
}

.search .input-group-btn .btn-primary {
  background: #bb8b30;
  cursor: pointer;
  font-weight: bold;
  color: white;
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
