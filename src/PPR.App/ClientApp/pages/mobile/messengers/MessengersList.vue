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
        @iteration="iteration"
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
    const vm = this;
    return {
      helpers: helpers,
      response: [],
      fullPage: true,
      isLoading: false,
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
        pageLength: 13,
        lengthMenu: [
          [13, 15, 100, 500, 1000, -1],
          [13, 15, 100, 500, 1000, "All"]
        ],
        searchDelay: 15,
        destroy: true,
        ordering: true,
        order: [1, "desc"],
        lengthChange: true,
        serverSide: false,
        fixedHeader: true,
        saveState: false
      },
      fields: {
        "_source.table_name": {
          label: "Category",
          sortable: true,
          searchable: true,
          defaultOrder: "desc",
          render: function(data, type, full, meta) {
            if (typeof data != "undefined") {
              if (data == "generic_chat") {
                return "genr";
              } else {
                data = data.split("_");

                let cls = "";
                let clr = "";
                switch (data[1].toLowerCase()) {
                  case "whatsapp":
                    cls = "whatsapp";
                    clr = "#25D366";
                    break;

                  case "facebookchat":
                    cls = "facebook";
                    clr = "#4267b2";
                    break;

                  case "viber":
                    return ``;
                    break;

                  default:
                    cls = "";
                    break;
                }

                return `<span class="fa-stack fa-md">
                          <i class="fa fa-circle fa-stack-2x"  style="color:${clr}"></i>
                          <i class="fab fa-${cls} fa-stack-2x" title="${cls}" style="color:#fff"></i>
                        </span>`;
              }
            } else {
              return "N/A";
            }
          }
        },
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
            data = helpers.htmlEncode(data);
            let cls = "label-default";
            switch (full._source.type.toLowerCase()) {
              case "text":
                return data.length > 50 ? data.substr(0, 50) + " ..." : data;
                break;

              case "image":
                cls = "label-success";
                break;

              case "gif":
                cls = "label-info";
                break;

              case "video":
                cls = "label-warning";
                break;

              default:
                cls = "label-danger";
                const path = full._source.attachments_path;
                if (path != null && path != "") {
                  full._source.type = helpers
                    .getExtensionFromStr(full._source.attachments_path)
                    .toUpperCase();
                } else {
                  return data.length > 50 ? data.substr(0, 50) + " ..." : data;
                }

                break;
            }

            return `<span class="label ${cls}"><b>${
              full._source.type
            }</b></span> ${
              data.length > 50 ? data.substr(0, 50) + " ..." : data
            }`;
          }
        }
      },
      rowClick: {
        //condition: `tr td:not(:nth-child(4),:nth-child(5))`
      }
    };
  },

  computed: {
    ...mapGetters(["globals"]),
    requestParms() {
      const vm = this;
      return {
        keyword: vm.globals.keyword,
        type: vm.globals.searchType
      };
    }
  },

  mounted() {
    const vm = this;
    const table = vm.$refs.table;

    vm.$root.$on("onSearchBoxChange", () => {
      vm.doLoadTable();
      vm.hide();
    });

    window.$("#main > .overlay").on("click", function() {
      window.$("#" + table.tableId + " tbody tr").removeClass("selected");
      vm.hide();
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

    // DT
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

      vm.helpers
        .post("/api/mobile/messengers", vm.requestParms)
        .then(({ data }) => {
          // console.log(data.hits);
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
