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
        userName: {
          label: "User Name",
          sortable: true,
          searchable: true,
          defaultOrder: "desc",
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.blankSpaceAndNullCheck(data));
          }
        },
        lastLogin: {
          label: "Last Login",
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.blankSpaceAndNullCheck(data));
          }
        },
        created: {
          label: "Created On",
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.blankSpaceAndNullCheck(data));
          }
        },
        userRoles: {
          label: "Roles",
          sortable: false,
          searchable: true,
          render: function(data, type, full, meta) {
            return data;
          }
        },
        edit: {
          isLocal: true,
          label: "Edit",
          defaultContent:
            '<a href="javascript:void(0);" data-action="edit" class="btn btn-primary btn-sm"><i class="far fa-user-edit "></i></a>'
        },
        delete: {
          isLocal: true,
          label: "Delete",
          defaultContent:
            '<a href="javascript:void(0);" data-action="delete" class="btn btn-danger btn-sm"><i class="far fa-backspace "></i></a>'
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
