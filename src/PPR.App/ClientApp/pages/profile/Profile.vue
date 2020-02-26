<template>
  <div>
    <loading :active.sync="isLoading"></loading>
    <div v-if="profile.viewType == 'card'">
      <div
        class="col-sm-3"
        v-for="({ _source }, index) in response"
        v-bind:key="({ _source }, index)"
      >
        <div class="contact-box center-version">
          <a @click="showDetails(_source.subject_id)">
            <template v-if="_source.subject_image_path">
              <img
                alt="avatar"
                class="rounded-circle"
                :src="mapSrc(_source.subject_image_path)"
                oncontextmenu="return false;"
                @error="imgErrorPlaceholder($event, index)"
              />
            </template>
            <template v-else>
              <template v-if="_source.subject_sex.toLowerCase() == 'male'">
                <img
                  alt="avatar"
                  class="rounded-circle"
                  oncontextmenu="return false;"
                  src="/img/avatars/m1.jpg"
                />
              </template>
              <template v-else-if="_source.subject_sex.toLowerCase() == 'female'">
                <img
                  alt="avatar"
                  class="rounded-circle"
                  oncontextmenu="return false;"
                  src="/img/avatars/f1.jpg"
                />
              </template>
              <template v-else>
                <img alt="image" class="rounded-circle" src="/img/avatars/u1.jpg" />
              </template>
            </template>

            <h3 class="m-b-xs">
              <strong>{{ helpers.strip(_source.subject_name, 20) }}</strong>
            </h3>

            <h3 class="font-bold">{{ _source.subject_id }}</h3>

            <address class="m-t-md">
              <strong
                v-if="_source.subject_sex"
                class="font-bold"
              >{{ helpers.blankSpaceCheck(_source.subject_sex) }}</strong>
              <strong v-else class="font-bold">Gender Not Available</strong>

              <div>{{ subjectEmail(index) }}</div>

              <div
                v-if="_source.subject_organization"
                title="Organization"
              >{{ helpers.strip(_source.subject_organization, 25) }}</div>
              <div v-else>Organization Not Available</div>

              <div
                v-if="_source.subject_category"
                title="Category"
              >{{ helpers.strip(_source.subject_category, 25) }}</div>
              <div v-else>Category Not Available</div>
              <br />
            </address>
          </a>
          <div class="contact-box-footer">
            <div class="m-t-xs btn-group">
              <a
                v-if="_source.subject_data_avail_mob"
                @click="
                  redirectToMobile(
                    helpers.strip(_source.subject_data_avail_mob, 25)
                  )
                "
                class="btn btn-xs btn-white"
              >
                <i class="fa fa-phone"></i> Mobile
              </a>
              <a
                v-if="_source.subject_personalemail"
                @click="
                  redirectToEmail(
                    helpers.strip(_source.subject_personalemail, 25)
                  )
                "
                class="btn btn-xs btn-white"
              >
                <i class="fa fa-envelope"></i> Email
              </a>
              <a
                v-if="_source.subject_id"
                @click="redirectToConnection(_source.subject_id)"
                class="btn btn-xs btn-white"
              >
                <i class="fa fa-user-plus"></i> Connection
              </a>
            </div>
          </div>
        </div>
      </div>

      <!-- Infinite loader setup -->
      <div class="infinite-wrapper">
        <infinite-loading
          spinner="spiral"
          @infinite="infiniteHandler"
          ref="InfiniteLoading"
          :distance="300"
        >
          <div slot="no-results" class="img-responsive text-center" style="margin-top:100px;">
            <img src="/img/no-record.png" />
          </div>

          <div class="alert alert-success" slot="no-more">No more resources to load!</div>
          <div slot="spinner"></div>
        </infinite-loading>
      </div>
    </div>

    <div class="ibox shadow-md-down" v-show="!isLoading" v-if="profile.viewType == 'tabular'">
      <div class="ibox-content">
        <div class="table-responsive">
          <data-table
            ref="table"
            :fields="fields"
            :opts="options"
            :rowClick="rowClick"
            @before-creating="doLoadTable"
            @rowClick="iteration"
          ></data-table>
        </div>
      </div>
    </div>

    <popup ref="popup"></popup>
    <config-box ref="configBox"></config-box>
  </div>
</template>

<script>
import Loading from "vue-loading-overlay";
import InfiniteLoading from "vue-infinite-loading";
import { mapGetters } from "vuex";

import { helpers } from "@/utils";

import DataTable from "@/components/DataTable";
import ConfigBox from "@/components/ui/ConfigBox";
import Popup from "./Popup";

export default {
  components: {
    Loading,
    InfiniteLoading,
    Popup,
    ConfigBox,
    DataTable
  },

  data() {
    return {
      helpers: helpers,
      response: [],
      fullPage: true,
      isLoading: true,
      limit: 30,
      page: 1,
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
        order: [1, "desc"],
        lengthChange: true,
        serverSide: false,
        fixedHeader: true,
        saveState: false
      },
      fields: {
        "_source.subject_id": {
          label: "ID",
          sortable: true,
          searchable: true,
          render: function(data, type, full, meta) {
            return data;
          }
        },
        "_source.subject_name": {
          label: "Subject Name",
          sortable: true,
          searchable: true,
          defaultOrder: "desc",
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.blankSpaceAndNullCheck(data));
          }
        },
        "_source.arabic_name": {
          label: "Arabic Name",
          sortable: true,
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.blankSpaceAndNullCheck(data));
          }
        },
        "_source.subject_sex": {
          label: "Sex",
          sortable: true,
          searchable: true,
          defaultOrder: "desc",
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.blankSpaceAndNullCheck(data));
          }
        },
        "_source.subject_organization": {
          label: "Organization",
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.strip(data, 60));
          }
        },
        "_source.subject_category": {
          label: "Category",
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.blankSpaceAndNullCheck(data));
          }
        },
        "_source.subject_personalemail": {
          label: "Personal Email",
          sortable: false,
          searchable: true,
          render: function(data, type, full, meta) {
            return helpers.undefinedCheck(helpers.strip(data, 25));
          }
        }
      },
      rowClick: { condition: "tr" }
    };
  },

  computed: {
    ...mapGetters(["globals"]),
    ...mapGetters(["profile"])
  },

  mounted() {
    const vm = this;
    const configBox = vm.$refs.configBox;

    vm.$root.$on("onSearchBoxChange", () => {
      vm.resetComponent();
      if (vm.profile.viewType == "tabular") vm.doLoadTable();
    });

    vm.$root.$on("onTableViewChange", data => {
      vm.resetComponent();
      vm.$store.commit("CHANGE_PROFILE_VIEW", data.viewType);
    });
    configBox.active(vm.profile.viewType);
  },

  beforeDestroy() {
    this.$root.$off();
  },

  methods: {
    infiniteHandler($state) {
      const vm = this;
      if (vm.page === 1) vm.isLoading = true;

      helpers
        .post("/api/profile/info", {
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
    doLoadTable() {
      const vm = this;
      const table = vm.$refs.table;
      vm.isLoading = true;

      // console.log(table);

      vm.helpers
        .post("/api/profile/infoDt", {
          keyword: vm.globals.keyword,
          type: vm.globals.searchType
        })
        .then(({ data }) => {
          if (data.hits.length > 0) {
            table.setTableData(data.hits);
          } else {
            table.setTableData([]);
          }
          vm.isLoading = false;
        });
    },
    showDetails(id) {
      this.$refs.popup.show(id);
    },
    iteration(row) {
      this.showDetails(row._source.subject_id);
    },

    subjectEmail(i) {
      let personal = this.helpers.blankSpaceAndNullCheck(
        this.response[i]["_source"]["subject_personalemail"]
      );

      let professional = this.helpers.blankSpaceAndNullCheck(
        this.response[i]["_source"]["subject_professionalemail"]
      );

      if (personal !== "N/A" && professional !== "N/A") {
        return this.helpers.strip(`${personal}${professional}`, 20);
      } else if (personal !== "N/A") {
        return this.helpers.strip(personal, 20);
      } else if (professional !== "N/A") {
        return this.helpers.strip(professional, 20);
      } else return "Email Not Available";
    },

    resetComponent() {
      const vm = this;
      vm.page = 1;
      vm.response = [];
      if (vm.$refs.InfiniteLoading)
        vm.$refs.InfiniteLoading.stateChanger.reset();
    },

    mapSrc(filepath) {
      let hostPath = "..";

      if (filepath) {
        return "http://hawk.cnc.com" + filepath.split(hostPath).join("");
      }
    },

    imgErrorPlaceholder(e, i) {
      const vm = this;
      const _obj = vm.response[i]._source;

      if (_obj.subject_sex.toLowerCase() == "male")
        e.target.src = "/img/avatars/m1.jpg";
      else if (_obj.subject_sex.toLowerCase() == "female")
        e.target.src = "/img/avatars/f1.jpg";
      else e.target.src = "/img/avatars/u1.jpg";
    },

    redirectToMobile(userId) {
      window.open(
        process.env.MIX_MOBILE_REDIRECTION_URL + "user" + userId,
        "_blank"
      );
    },
    redirectToEmail(email) {
      var cookieName = "HawkEmail";
      var cookieValue = btoa(email);
      var myDate = new Date();
      myDate.setMonth(myDate.getMonth() + 12);
      document.cookie =
        cookieName +
        "=" +
        cookieValue +
        ";expires=" +
        myDate +
        ";domain=.cnc.com;path=/";
      window.open(process.env.MIX_EMAIL_REDIRECTION_URL, "_blank");
    },

    redirectToConnection(subjectId) {
      window.open(process.env.MIX_CONNECTION_REDIRECTION_URL + subjectId);
    }
  }
};
</script>
