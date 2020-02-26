<template>
  <div>
    <loading :active.sync="isLoading"></loading>
    <div class="ibox shadow-md-down" v-show="!isLoading">
      <div class="ibox-content">
        <div class="table-responsive">
          <data-table
            ref="table"
            :fields="fields"
            :opts="options"
            :details="details"
          ></data-table>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// external js import
import Loading from "vue-loading-overlay";
import { mapGetters } from "vuex";

import DataTable from "@/components/DataTable";
import { helpers } from "@/utils";

export default {
  components: { Loading, DataTable },

  data() {
    const vm = this;
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
        ordering: true,
        lengthChange: true,
        serverSide: false,
        fixedHeader: true,
        saveState: false
      },
      fields: {
        "_source.subject_id": { label: "Subject", sortable: true },
        "_source.log_id": { label: "Report Id", sortable: true },
        "_source.title": { label: "Report Title", sortable: true },
        "_source.from": { label: "From", sortable: true },
        "_source.to": { label: "To", sortable: true },
        "_source.date": { label: "DateTime", sortable: true }
      },
      details: {
        url: "/api/profile/report/details",
        type: "POST",
        headers: ["File Name", "Action"]
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
    },
    jq() {
      return this.jquery || window.jQuery;
    }
  },

  mounted() {
    this.doLoadTable();
    this.$root.$on("onSearchBoxChange", () => {
      this.doLoadTable();
    });
  },
  beforeDestroy() {
    this.$root.$off();
  },

  methods: {
    // DT

    doLoadTable() {
      const vm = this;
      const jq = vm.jq;
      const table = vm.$refs.table;
      const $el = jq(table.$el);
      vm.isLoading = true;
      vm.helpers
        .post("/api/tagged/reports", vm.requestParms)
        .then(({ data }) => {
          if (data.hits.length > 0) {
            table.setTableData(data.hits);
          } else {
            table.setTableData([]);
          }
          vm.isLoading = false;

          $el.on("click", "td.details-control", e => {
            e.preventDefault();
            e.stopPropagation();
            const target = jq(e.target);
            let that = target;
            let tr = that.closest("tr");
            if (tr.attr("role") !== "row") {
              tr = tr.prev();
            }
            const row = table.dataTable.row(tr);
            const data = row.data();

            vm.details.dt = "#dtReportsDetails" + data._source.log_id;
            let options = Object.assign(vm.details, vm.options);
            options.tr = tr;
            options.row = row;
            options.data = data;
            options.post = {
              sub_id: data._source.subject_id,
              report_id: data._source.log_id
            };

            options.columns = [
              {
                data: "_source.file_name",

                render: function(data, type, full, meta) {
                  return vm.helpers.blankSpaceCheck(
                    vm.helpers.undefinedCheck(data)
                  );
                }
              },

              {
                data: "_source.file",

                render: function(data, type, full, meta) {
                  var file = "NA";

                  if (typeof full._source.file_name != "undefined")
                    file = full._source.file_name;

                  return (
                    '<a href="data:application/octet-stream;charset=utf-16le;base64,' +
                    data +
                    '"  class="pointer" download="' +
                    file +
                    '"><i class="fa fa-download"></i></a>'
                  );
                }
              }
            ];
            table.dtExpandToggle(options);
          });
        });
    }
  }
};
</script>
