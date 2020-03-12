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
            @before-creating="doLoadTable"
            @iteration="iteration"
          ></data-table>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
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
      isLoading: false,
      options: {
        exports: {
          title: "Users List"
        },
        /*eslint-disable */
        dom: "<'html5buttons'B>lTfgirtp",
        /*eslint-enable */
        responsive: false,
        processing: true,
        searching: true,
        searchDelay: 15,
        destroy: true,
        // ordering: true,
        // order: [0, "desc"],
        lengthChange: true,
        serverSide: false,
        fixedHeader: true,
        saveState: false
      },
      fields: {
        "_source.name": {
          label: "Contact Name",
          sortable: true,
          searchable: true,
          defaultOrder: "desc",
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.blankSpaceAndNullCheck(data));
          }
        },
        "_source.number": {
          label: "Contact Phone",
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.blankSpaceAndNullCheck(data));
          }
        },
        "_source.email": {
          label: "Contact Email",
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.blankSpaceAndNullCheck(data));
          }
        },
        "_source.company": {
          label: "Contact Organization",
          sortable: false,
          searchable: true,
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.blankSpaceAndNullCheck(data));
          }
        }
      }
    };
  },

  mounted() {
    const vm = this;
    vm.doLoadTable();
  },
  beforeDestroy() {
    this.$root.$off();
  },

  methods: {
    showPopup(data) {
      this.$refs.popup.show(data);
    },

    // DT

    doLoadTable() {
      const vm = this;
      const table = vm.$refs.table;
      vm.isLoading = true;

      vm.helpers.get("/api/account/allusers").then(({ data }) => {
        if (data.result.length > 0) {
          table.setTableData(data.result);
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
