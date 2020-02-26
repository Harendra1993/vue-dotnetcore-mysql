<template>
  <div>
    <loading :active.sync="isLoading"></loading>
    <div v-if="untagged_files.viewType == 'card'">
      <div
        class="col-md-2"
        v-for="({ _source }, index) in response"
        v-bind:key="({ _source }, index)"
      >
        <file :obj="_source" :src="mapSrc(_source)" />
      </div>
      <!-- Infinite loader setup -->
      <div class="infinite-wrapper">
        <infinite-loading
          spinner="spiral"
          @infinite="infiniteHandler"
          ref="InfiniteLoading"
          :distance="300"
        >
          <div
            slot="no-results"
            class="img-responsive text-center"
            style="margin-top:100px;"
          >
            <img src="/img/no-record.png" />
          </div>

          <div class="alert alert-success" slot="no-more">
            No more resources to load!
          </div>
          <div slot="spinner"></div>
        </infinite-loading>
      </div>
    </div>
    <div
      class="ibox shadow-md-down"
      v-show="!isLoading"
      v-if="untagged_files.viewType == 'tabular'"
    >
      <div class="ibox-content">
        <div class="table-responsive">
          <data-table
            ref="table"
            :fields="fields"
            :opts="options"
            @before-creating="doLoadTable"
          ></data-table>
        </div>
      </div>
    </div>

    <config-box ref="configBox"></config-box>
  </div>
</template>

<script>
// external js import
import Loading from "vue-loading-overlay";
import InfiniteLoading from "vue-infinite-loading";
import { mapGetters } from "vuex";

import DataTable from "@/components/DataTable";
import { helpers } from "@/utils";
import ConfigBox from "@/components/ui/ConfigBox";
import File from "@/components/ui/File";

export default {
  components: { Loading, InfiniteLoading, DataTable, ConfigBox, File },

  data() {
    const vm = this;
    var host = process.env.MIX_FILES_FOLDER;
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
        "_source.filepath": {
          label: "View/Download",
          sortable: true,
          render: function(data, type, full, meta) {
            var extension = helpers.getExtensionFromStr(full._source.filename);
            var filename = encodeURIComponent(full._source.filename);

            if (extension == "pdf") {
              return (
                '<a class="pointer"  href=" ' +
                data.replace(host, "/Data/hawk/untagged_reports") +
                "/" +
                filename +
                '  " target="_blank"><i class="fa fa-eye"></i></a>'
              );
            } else {
              return (
                '<a class="pointer"  title=" ' +
                data.replace(host, "/Data/hawk/untagged_reports") +
                "/" +
                full._source.filename +
                '"  href=" ' +
                data.replace(host, "/Data/hawk/untagged_reports") +
                "/" +
                filename +
                '" target="_blank"><i class="fa fa-download"></i></a>'
              );
            }
          }
        },
        "_source.filename": {
          label: "Name",
          sortable: true,
          searchable: true
        },
        "_source.filesize": {
          label: "Size",
          sortable: true,
          searchable: true,
          render: function(data, type, full, meta) {
            return helpers.bytesToSize(data);
          }
        },
        "_source.datetime": {
          label: "Timestamp",
          sortable: true,
          searchable: true
        }
      }
    };
  },

  computed: {
    ...mapGetters(["globals"]),
    ...mapGetters(["untagged_files"]),
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
    const configBox = vm.$refs.configBox;

    window.$("#untagged").addClass("exact-active");

    vm.$root.$on("onSearchBoxChange", () => {
      vm.resetComponent();
      if (vm.profile.viewType == "tabular") vm.doLoadTable();
    });

    vm.$root.$on("onTableViewChange", data => {
      vm.resetComponent();
      vm.$store.commit("CHANGE_UNTAGGED_FILES_VIEW", data.viewType);
    });
    configBox.active(vm.untagged_files.viewType);
  },
  beforeDestroy() {
    this.$root.$off();
    window.$("#untagged").removeClass("exact-active");
  },

  methods: {
    resetComponent() {
      const vm = this;
      vm.page = 1;
      vm.response = [];
      if (vm.$refs.InfiniteLoading)
        vm.$refs.InfiniteLoading.stateChanger.reset();
    },

    infiniteHandler($state) {
      const vm = this;
      if (vm.page === 1) vm.isLoading = true;

      helpers
        .post("/api/untagged/files", {
          keyword: vm.globals.keyword,
          type: vm.globals.searchType,
          limit: vm.limit,
          currentPage: vm.page
        })
        .then(({ data }) => {
          if (data.hits.length == 0) {
            $state.complete();
            if (vm.page == 1)
              vm.helpers.notify({
                scope: vm,
                type: "danger",
                body: "No data Found."
              });
          } else {
            vm.response = vm.response.concat(data.hits);
            vm.page += 1;
            $state.loaded();
          }
          vm.isLoading = false;
        })
        .catch(error => {
          console.log(error);
          vm.isLoading = false;
        });
    },

    // DT
    doLoadTable() {
      const vm = this;
      const table = vm.$refs.table;
      vm.isLoading = true;

      vm.helpers
        .post("/api/untagged/filesDt", vm.requestParms)
        .then(({ data }) => {
          if (data.hits.length > 0) {
            table.setTableData(data.hits);
          } else {
            table.setTableData([]);
          }
          vm.isLoading = false;
        });
    },

    mapSrc(source) {
      let hostPath = process.env.MIX_FILES_FOLDER;
      if (source.filepath) {
        return (
          process.env.MIX_APP_URL +
          source.filepath.replace(hostPath, "/Data/hawk/untagged_reports") +
          source.filename
        );
      }
    }
  }
};
</script>
