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
            @edit="doAlertUserEdit"
            @delete="doAlertUserDelete"
          ></data-table>
        </div>
      </div>
    </div>
    <create-user @created="doUserCreated" ref="create_user" />
    <edit-user @edited="doUserEdited" ref="edit_user" />
  </div>
</template>

<script>
import Loading from "vue-loading-overlay";
import { mapGetters } from "vuex";

import DataTable from "@/components/DataTable";
import CreateUser from "./CreateUser";
import EditUser from "./EditUser";
import { helpers } from "@/utils";

export default {
  components: { Loading, DataTable, CreateUser, EditUser },

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

        dom:
          "<'col-sm-2'l><'col-sm-6'f><'html5buttons'B>" +
          "<'col-sm-12'tr>" +
          "<'col-sm-4'i><'col-sm-8'p>",
        /*eslint-enable */
        responsive: false,
        processing: true,
        searching: true,
        searchDelay: 15,
        destroy: true,
        ordering: true,
        order: [1, "desc"],
        lengthChange: true,
        serverSide: false,
        fixedHeader: true,
        saveState: false,
        buttons: [
          {
            text: "<i class='far fa-user-plus'></i> Create User",
            className: "btn btn-primary",
            action: function(e, dt, node, config) {
              vm.doAlertUserCreate();
            }
          }
        ]
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
        created: {
          label: "Created On",
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.fullDateTime(data));
          }
        },
        lastLogin: {
          label: "Last Login",
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.fullDateTime(data));
          }
        },
        userRoles: {
          label: "Roles",
          sortable: false,
          searchable: true,
          render: function(data, type, full, meta) {
            return data
              .map(function(item) {
                return item.role.roleName;
              })
              .join(", ");
          }
        },
        isActive: {
          label: "Status",
          sortable: false,
          searchable: true,
          render: function(data, type, full, meta) {
            if (data)
              return `<label class="label bg-color-greenLight"><b>Active</b?</label>`;
            else
              return `<label class="label bg-color-red"><b>Inactive</b></label>`;
          }
        },
        edit: {
          isLocal: true,
          label: "Edit",
          defaultContent:
            '<a href="javascript:void(0);" data-action="edit" class="btn btn-success btn-sm"><i class="far fa-user-edit "></i></a>'
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
    doAlertUserCreate() {
      this.$refs.create_user.show();
    },

    doUserCreated(data, user) {
      const vm = this;
      const table = vm.$refs.table;
      table.addTableRow(data.result);
    },

    doAlertUserEdit(row) {
      this.$refs.edit_user.show(row);
    },

    doUserEdited(data, user) {
      const vm = this;
      const table = vm.$refs.table;
      table.updateTableRow(data.result);
    },

    doAlertUserDelete(data, row, tr, target) {
      window.alert(`deleting item ID: ${data.id}`);

      // row.remove() doesn't work when serverside is enabled
      // so we fake it with dom remove
      tr.remove();

      const table = this.$refs.table;
      setTimeout(() => {
        // simulate extra long running ajax
        table.reload();
      }, 1500);
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
