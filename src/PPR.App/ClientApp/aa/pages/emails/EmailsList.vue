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
        @rowClick="showEmail"
      ></data-table>
    </div>

    <div id="message">
      <email ref="email"></email>
    </div>
    <headers ref="headers" />
  </div>
</template>

<script>
import Loading from "vue-loading-overlay";
import { mapGetters } from "vuex";

import DataTable from "@/components/DataTable";
import { helpers } from "@/utils";
import Email from "./Email";
import Headers from "./Headers";

export default {
  components: {
    Loading,
    DataTable,
    Email,
    Headers
  },

  data() {
    return {
      helpers: helpers,
      response: [],
      fullPage: true,
      isLoading: true,
      limit: 30,
      page: 1,
      keyword: "",
      isTableLoading: false,
      bodyLoaded: false,
      emailBodyFlag: false,
      rowID: 0,
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
        lengthMenu: [
          [15, 100, 500, 1000, -1],
          [15, 100, 500, 1000, "All"]
        ],
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
        "_source.emaildate": { label: "Date", sortable: true },
        "_source.emailsubject": {
          label: "Subject",
          sortable: false,
          searchable: true,
          render: function(data, type, full, meta) {
            // if (data != null && data != "") {
            //   if (data.length <= 100) return `<p>${data}<p>`;
            //   else return `<p>${data.substr(0, 100)}...<p>`;
            // } else {
            //   return `<p>N/A</p>`;
            // }
            data = helpers.htmlEncode(data);
            return helpers.strip(data, 60);
          }
        },
        "_source.folder": {
          label: "Folder",
          sortable: true,
          searchable: true,
          defaultOrder: "desc",
          render: function(data, type, full, meta) {
            if (typeof data != "undefined") {
              let cls = "label-default";
              switch (data.toLowerCase()) {
                case "inbox":
                  cls = "label-primary";
                  break;

                case "sent":
                  cls = "label-success";
                  break;

                case "draft":
                  cls = "label-info";
                  break;

                case "archive":
                  cls = "label-warning";
                  break;

                case "trash":
                  cls = "label-warning";
                  break;

                case "spam":
                  cls = "label-danger";
                  break;

                case "junk":
                  cls = "label-danger";
                  break;

                default:
                  cls = "label-default";
                  break;
              }

              return `<span class="label ${cls}"><b>${data}</b></span>`;
            } else {
              return "N/A";
            }
          }
        },
        "_source.emailid": {
          label: "From",
          render: function(data, type, full, meta) {
            const folder =
              full._source.folder != undefined
                ? full._source.folder.toLowerCase()
                : "";
            if (folder == "sent" || folder == "draft") {
              return data;
            } else {
              return full._source.otheremailid;
            }
          }
        },
        "_source.otheremailid": {
          label: "To",
          render: function(data, type, full, meta) {
            const folder =
              full._source.folder != undefined
                ? full._source.folder.toLowerCase()
                : "";
            if (folder == "sent" || folder == "draft") {
              let emails = data.split(",");
              if (emails.length > 1) {
                const to = emails[0];
                return `${to} <a class="m-b block" data-toggle="tooltip" title="${emails
                  .slice(1)
                  .join(", ")}"><small>+ ${emails.length - 1} More</small></a>`;
              } else {
                return data;
              }
            } else {
              return full._source.emailid;
            }
          }
        },

        "_source.attachmentscount": {
          label: "<i class='fa fa-paperclip'></i>",
          render: function(data, type, full, meta) {
            if (data > 0) {
              return `<span class="label label-primary">${data}</span>`;
            } else {
              return `<span class="label label-default">${data}</span>`;
            }
          }
        }
      },
      rowClick: {
        //condition: `tr td:not(:nth-child(3),:nth-child(4))`
      }
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
    const table = vm.$refs.table;
    vm.$root.$on("onSearchBoxChange", () => {
      vm.doLoadTable();
      vm.hide();
    });

    vm.$root.$on("onEmailShowHeaders", id => {
      vm.$refs.headers.show(id);
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
    showEmail(data, row, tr) {
      const vm = this;
      const table = vm.$refs.table;
      if (tr.hasClass("selected")) {
        tr.removeClass("selected");
        vm.hide();
      } else {
        table.dataTable.$("tr.selected").removeClass("selected");
        tr.addClass("selected");
        if (data) {
          vm.$refs.email.show(data._source);
          vm.show();
        }
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

      vm.helpers.post("/api/email/search", vm.requestParms).then(({ data }) => {
        if (data.hits.length > 0) {
          table.setTableData(data.hits);
        } else {
          table.setTableData([]);
        }
        vm.isLoading = false;
      }, 100);
    }
  }
};
</script>

<style lang="scss">
@import "./resources/sass/slide.scss";
</style>
