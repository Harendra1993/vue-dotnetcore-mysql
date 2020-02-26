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
      },
      details: {
        template: `<div style="margin-left:50px;" class="jumbotron">
              <div class="row">
                <div class="col-md-1 text-bold">ID</div>
                <div class="col-md-11">{{ data._source.subject_id }}</div>
              </div>
              <div class="row">
                <div class="col-md-1 text-bold">SUBJECT NAME</div>
                <div class="col-md-11">{{ data._source.subject_name }}</div>
              </div>
              <div class="row">
                <div class="col-md-1 text-bold">GENDER</div> 
                <div class="col-md-11">{{ data._source.subject_gender }}</div>
              </div>
			  <div class="row">
                <div class="col-md-1 text-bold">SUBJECT Phone</div> 
                <div class="col-md-11">{{ data._source.subject_phone }}</div>
              </div>
              <div class="row">
                <div class="col-md-1 text-bold">SUBJECT EMAIL</div> 
                <div class="col-md-11">{{ data._source.subject_email }}</div>
              </div>
              <div class="row">
                <div class="col-md-1 text-bold">NATIONALITY</div>
                <div class="col-md-11">{{ data._source.subject_nationality }}</div>
              </div>
              <div class="row">
                <div class="col-md-1 text-bold">DESIGNATION</div> 
                <div class="col-md-11">{{ data._source.subject_designation }}</div>
              </div>
            </div>`
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

    vm.$root.$on("onSearchBoxChange", () => {
      vm.doLoadTable();
    });
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

      vm.helpers.post("/api/contacts", vm.requestParms).then(({ data }) => {
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
