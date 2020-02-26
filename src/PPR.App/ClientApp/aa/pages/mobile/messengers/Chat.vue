<template>
  <div v-if="obj">
    <loading :active.sync="isLoading" :is-full-page="false"></loading>
    <div v-show="!isLoading">
      <div class="chat-header">
        <span>
          To:
          <span class="name">
            {{
            helpers.blankSpaceAndNullCheck(obj._source.to)
            }}
          </span>
        </span>

        <span class="pull-right">
          <i class="fa fa-user"></i>&nbsp;
          <span class="name">{{ obj._source.userid }}</span>
        </span>
      </div>
      <div class="right">
        <div class="chat active-chat" v-if="data.length > 0">
          <div v-for="({ _source }, index) in data" :key="({ _source }, index)">
            <template v-if="isDifferentDay(index)">
              <div class="conversation-start">
                <span class="label label-default" style="color:#000;">
                  {{
                  getMessageDate(index)
                  }}
                </span>
              </div>
            </template>

            <message-thread :obj="_source"></message-thread>
          </div>
          <!-- Before -->
          <div class="infinite-wrapper">
            <infinite-loading
              spinner="spiral"
              @infinite="beforeInfiniteHandler"
              ref="BeforeInfiniteLoading"
              :distance="200"
              direction="top"
            >
              <div slot="no-results"></div>
              <div slot="spinner"></div>
              <div slot="no-more"></div>
            </infinite-loading>
          </div>
          <!-- Before -->
          <!-- After -->
          <div class="infinite-wrapper">
            <infinite-loading
              spinner="spiral"
              @infinite="afterInfiniteHandler"
              ref="AfterInfiniteLoading"
              :distance="200"
            >
              <div slot="no-results"></div>
              <div slot="spinner"></div>
              <div slot="no-more"></div>
            </infinite-loading>
          </div>

          <!-- After -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Loading from "vue-loading-overlay";
import InfiniteLoading from "vue-infinite-loading";
import Cookies from "js-cookie";

import { helpers } from "@/utils";
import MessageThread from "./MessageThread";

export default {
  components: {
    Loading,
    InfiniteLoading,
    MessageThread
  },
  data: function() {
    return {
      helpers: helpers,
      res: [],
      open: false,
      data: [],
      obj: undefined,
      timestamp: undefined,
      limit: 10,
      beforePage: 1,
      afterPage: 1,
      fullPage: false, // loader config
      isLoading: false // loader config
    };
  },

  computed: {
    jq() {
      return this.jquery || window.jQuery;
    }
  },

  methods: {
    redirect: function(userid, to) {
      let cookie = userid + "`" + to;
      Cookies.set("g_mobile", helpers.utoa(cookie), {
        path: "",
        domain: "mobile.cnc.com"
      });
      window.open(
        "http://mobile.cnc.com/hawkmobile_demo/app/user/chats.php",
        "_blank"
      );
      //console.log(Cookies.get("g_mobile"));
    },
    show: function(obj) {
      const vm = this;

      vm.open = true;
      vm.reset();

      vm.obj = obj;
      vm.data.push(obj);
      vm.data[0]._source.selected = true;
      vm.timestamp = obj.sort[0];
    },

    reset() {
      const vm = this;
      vm.data = [];
      vm.beforePage = 1;
      vm.afterPage = 1;
      // console.log(vm.$refs);
      if (vm.$refs.BeforeInfiniteLoading)
        vm.$refs.BeforeInfiniteLoading.stateChanger.reset();
      if (vm.$refs.AfterInfiniteLoading)
        vm.$refs.AfterInfiniteLoading.stateChanger.reset();
    },

    beforeInfiniteHandler($state) {
      const vm = this;
      // const obj = { ...vm.data[0]._source };
      //if (vm.page === 1) vm.isLoading = true;
      vm.helpers
        .post("/api/mobile/messengers/before", {
          userid: vm.obj._source.userid,
          to: vm.obj._source.to,
          datetime: vm.obj._source.datetime,
          limit: vm.limit,
          currentPage: vm.beforePage
        })
        .then(({ data }) => {
          if (data.hits.length == 0) {
            $state.complete();
          } else {
            vm.data.unshift(...data.hits.reverse());
            $state.loaded();
            vm.beforePage += 1;
          }
          // vm.isLoading = false;
        });
    },
    afterInfiniteHandler($state) {
      const vm = this;
      // const obj = { ...vm.data[0]._source };
      // if (vm.page === 1) vm.isLoading = true;
      vm.helpers
        .post("/api/mobile/messengers/after", {
          userid: vm.obj._source.userid,
          to: vm.obj._source.to,
          datetime: vm.obj._source.datetime,
          limit: vm.limit,
          currentPage: vm.afterPage
        })
        .then(({ data }) => {
          if (data.hits.length == 0) {
            $state.complete();
          } else {
            vm.data.push(...data.hits);
            $state.loaded();
            vm.afterPage += 1;
          }
          //vm.isLoading = false;
        });
    },
    isDifferentDay(messageIndex) {
      if (messageIndex === 0) return true;

      const d1 = new Date(this.data[messageIndex - 1]._source.datetime);
      const d2 = new Date(this.data[messageIndex]._source.datetime);

      return (
        d1.getFullYear() !== d2.getFullYear() ||
        d1.getMonth() !== d2.getMonth() ||
        d1.getDate() !== d2.getDate()
      );
    },

    getMessageDate(messageIndex) {
      let dateToday = new Date().toDateString();
      let longDateYesterday = new Date();
      longDateYesterday.setDate(new Date().getDate() - 1);
      let dateYesterday = longDateYesterday.toDateString();
      let today = dateToday.slice(0, dateToday.length - 5);
      let yesterday = dateYesterday.slice(0, dateToday.length - 5);

      const wholeDate = new Date(
        this.data[messageIndex]._source.datetime
      ).toDateString();

      let messageDateString = wholeDate.slice(0, wholeDate.length - 5);

      //console.log(this.data[messageIndex]._source.datetime);

      if (
        new Date(this.data[messageIndex]._source.datetime).getFullYear() ===
        new Date().getFullYear()
      ) {
        if (messageDateString === today) {
          return "Today";
        } else if (messageDateString === yesterday) {
          return "Yesterday";
        } else {
          return messageDateString;
        }
      } else {
        return wholeDate;
      }
    },

    callback(msg) {
      //this.$notify(`Modal dismissed with msg '${msg}'.`);
    },

    bind(key, i = 0) {
      let value = this.response[i]["_source"][key];
      return this.helpers.blankSpaceAndNullCheck(value);
    },
    imageSrc(attPath) {
      let hostPath = process.env.MIX_MOBILE_ATTACHMENTS_HOST_FOLDER;

      return (
        "http://192.168.50.205/" +
        attPath.replace(hostPath, "").replace("/mobile/filesystem", "")
      );
    },
    imgErrorPlaceholder(e) {
      e.target.src = "/img/not-found.png";
    }
  }
};
</script>

<style lang="scss">
@import "./resources/sass/chat.scss";
</style>
