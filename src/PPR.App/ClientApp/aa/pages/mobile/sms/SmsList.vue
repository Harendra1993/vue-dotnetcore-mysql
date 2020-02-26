<template>
  <div>
    <loading :active.sync="isLoading"></loading>
    <div id="main" v-show="!isLoading">
      <div class="overlay"></div>
      <data-table
        ref="table"
        :fields="fields"
        :opts="options"
        :rowClick="rowClick"
        @before-creating="doLoadTable"
        @rowClick="showChat"
      ></data-table>
    </div>

    <div id="message">
      <chat ref="chat"></chat>
    </div>
  </div>
</template>

<script>
import Loading from "vue-loading-overlay";
import { mapGetters } from "vuex";

import DataTable from "@/components/DataTable";
import { helpers } from "@/utils";
import Chat from "./Chat";

export default {
  components: { Loading, DataTable, Chat },

  data() {
    return {
      helpers: helpers,
      response: [],
      fullPage: true,
      isLoading: true,
      limit: 30,
      page: 1,
      dialog: false,
      index: 0,
      options: {
        exports: {
          title: "Messengers"
        },
        /*eslint-disable */
        dom: "<'html5buttons'B>lTfgirtp",
        /*eslint-enable */
        responsive: false,
        processing: true,
        searching: true,
        searchDelay: 15,
        destroy: true,
        order: [0, "desc"],
        ordering: true,
        lengthChange: true,
        serverSide: false,
        fixedHeader: true,
        saveState: false
      },
      fields: {
        "_source.datetime": { label: "Timestamp", sortable: true },
        "_source.userid": {
          label: "User Id",
          sortable: true,
          searchable: true,
          defaultOrder: "desc",
          render: function(data, type, full, meta) {
            if (typeof data != "undefined") {
              var str = data.replace("user", "");

              return str;
            } else {
              return "N/A";
            }
          }
        },
        "_source.from": {
          label: "From",
          render: function(data, type, full, meta) {
            if (data != null && data != "") {
              if (data.length <= 40) return data;
              else return data.substr(0, 40) + "...";
            } else {
              return "N/A";
            }
          }
        },
        "_source.to": {
          label: "To",
          render: function(data, type, full, meta) {
            if (data != null && data != "") {
              if (data.length <= 40) return data;
              else return data.substr(0, 40) + "...";
            } else {
              return "N/A";
            }
          }
        },
        "_source.message": {
          label: "Message",
          sortable: false,
          searchable: true,
          render: function(data, type, full, meta) {
            return helpers.strip(data, 60);
          }
        }
      },
      rowClick: {}
    };
  },

  computed: {
    ...mapGetters(["globals"]),
    requestParms() {
      const vm = this;
      return {
        keyword: vm.globals.keyword,
        type: vm.globals.searchType,
        limit: vm.limit,
        currentPage: vm.page
      };
    }
  },

  mounted() {
    const vm = this;
    vm.$root.$on("onSearchBoxChange", () => {
      vm.doLoadTable();
    });
  },
  beforeDestroy() {
    const vm = this;
    vm.$root.$off();
    vm.hide();
  },

  methods: {
    showChat(data, row, tr) {
      const vm = this;
      const table = vm.$refs.table;
      if (tr.hasClass("selected")) {
        tr.removeClass("selected");
        vm.hide();
      } else {
        table.dataTable.$("tr.selected").removeClass("selected");
        tr.addClass("selected");
        vm.$refs.chat.show(data);
        vm.show();
      }
    },
    show() {
      window.$("body").addClass("show-message");
      window.$("body").addClass("show-main-overlay");
    },
    hide() {
      window.$("body").removeClass("show-message");
      window.$("body").removeClass("show-main-overlay");
    },
    doLoadTable() {
      const vm = this;
      const table = vm.$refs.table;
      vm.isLoading = true;

      vm.helpers.post("/api/mobile/sms", vm.requestParms).then(({ data }) => {
        if (data.hits.length > 0) {
          table.setTableData(data.hits);
        } else {
          table.setTableData([]);
        }
        vm.isLoading = false;
      });
    },
    iteration(row) {}
  }
};
</script>

<style lang="scss">
@import "./resources/sass/slide.scss";
</style>
