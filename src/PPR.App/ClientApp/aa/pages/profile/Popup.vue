<template>
  <modal v-model="open" @hide="callback" ref="modal" size="lg">
    <div style="height:500px;" v-show="isLoading">
      <loading :active.sync="isLoading"></loading>
    </div>

    <span slot="title">Profile Details</span>

    <div class="row" v-show="!isLoading">
      <div class="col-sm-12 col-md-12 col-lg-12">
        <div
          class="well well-light well-sm no-margin no-padding shadow-md-down"
        >
          <div class="row">
            <div class="col-sm-12">
              <div id="myCarousel" class="carousel profile-carousel">
                <div
                  class="air air-bottom-right padding-10"
                  style="display: none;"
                >
                  <a onclick class="btn txt-color-white bg-color-teal btn-sm">
                    <i class="fa fa-print"></i> Print
                  </a>
                  &nbsp;
                </div>

                <div class="air air-top-left padding-10">
                  <h4 class="txt-color-white font-md"></h4>
                </div>

                <div class="carousel-inner">
                  <div class="item active">
                    <div class="ibox">
                      <div id="map-box" class="ibox-content"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div class="col-sm-12">
              <div class="row">
                <div class="col-sm-2 profile-pic">
                  <template v-if="res.subject_image_path">
                    <img
                      alt="avatar"
                      :src="mapSrc(res.subject_image_path)"
                      oncontextmenu="return false;"
                      @error="imgErrorPlaceholder"
                      style="height: 180px; width: 100%; display: block;"
                    />
                  </template>
                  <template v-else>
                    <template
                      v-if="
                        helpers.undefinedCheck(res.subject_sex).toLowerCase() ==
                          'male'
                      "
                    >
                      <img
                        alt="avatar"
                        oncontextmenu="return false;"
                        src="/img/avatars/m1.jpg"
                        style="height: 180px; width: 100%; display: block;"
                      />
                    </template>
                    <template
                      v-else-if="
                        helpers.undefinedCheck(res.subject_sex).toLowerCase() ==
                          'female'
                      "
                    >
                      <img
                        alt="avatar"
                        oncontextmenu="return false;"
                        src="/img/avatars/f1.jpg"
                        style="height: 180px; width: 100%; display: block;"
                      />
                    </template>
                    <template v-else>
                      <img
                        alt="image"
                        src="/img/avatars/u1.jpg"
                        style="height: 180px; width: 100%; display: block;"
                      />
                    </template>
                  </template>
                </div>

                <div class="col-sm-8">
                  <div class="row">
                    <div class="col-md-4 text-muted">
                      <h4>
                        <b> <i class="fa fa-"></i> Subject Id </b>
                      </h4>
                    </div>
                    <div class="col-md-8">
                      <h4 class="txt-color-darken gs-word-wrap">
                        {{ res.subject_id }}
                      </h4>
                    </div>
                  </div>

                  <div class="row">
                    <div class="col-md-4 text-muted">
                      <h4>
                        <b> <i class="fa fa-"></i> Name </b>
                      </h4>
                    </div>
                    <div class="col-md-8">
                      <h4
                        v-if="res.subject_name"
                        class="txt-color-darken gs-word-wrap"
                      >
                        {{ helpers.blankSpaceAndNullCheck(res.subject_name) }}
                      </h4>
                    </div>
                  </div>

                  <div class="row">
                    <div class="col-md-4 text-muted">
                      <h4>
                        <b> <i class="fa fa-"></i> Arabic Name </b>
                      </h4>
                    </div>

                    <div class="col-md-8">
                      <h4
                        v-if="res.arabic_name"
                        class="txt-color-darken gs-word-wrap"
                      >
                        {{ helpers.blankSpaceAndNullCheck(res.arabic_name) }}
                      </h4>
                    </div>
                  </div>

                  <div class="row">
                    <div class="col-md-4 text-muted">
                      <h4>
                        <b> <i class="fa fa-map-"></i> Address </b>
                      </h4>
                    </div>

                    <div class="col-md-8">
                      <h4
                        v-if="res.subject_address"
                        class="txt-color-darken gs-word-wrap"
                      >
                        {{
                          helpers.blankSpaceAndNullCheck(res.subject_address)
                        }}
                      </h4>
                    </div>
                  </div>

                  <div class="row">
                    <div class="col-md-4 text-muted">
                      <h4>
                        <b> <i class="fa fa-"></i>Passport </b>
                      </h4>
                    </div>
                    <div class="col-md-8">
                      <h4
                        v-if="res.subject_passport_number"
                        class="txt-color-darken gs-word-wrap"
                      >
                        {{
                          helpers.blankSpaceAndNullCheck(
                            res.subject_passport_number
                          )
                        }}
                      </h4>
                    </div>
                  </div>

                  <div class="row">
                    <div class="col-md-4 text-muted">
                      <h4>
                        <b> <i class="fa fa-"></i> Personal Email </b>
                      </h4>
                    </div>

                    <div class="col-md-8">
                      <h4
                        v-if="res.subject_personalemail"
                        class="txt-color-darken gs-word-wrap"
                      >
                        {{
                          helpers.blankSpaceAndNullCheck(
                            res.subject_personalemail
                          )
                        }}
                      </h4>
                    </div>
                  </div>

                  <div class="row">
                    <div class="col-md-4 text-muted">
                      <h4>
                        <b> <i class="fa fa-"></i> Phone </b>
                      </h4>
                    </div>

                    <div class="col-md-8">
                      <h4
                        v-if="res.subject_personalphonenumber"
                        class="txt-color-darken gs-word-wrap"
                      >
                        {{
                          helpers.blankSpaceAndNullCheck(
                            res.subject_personalphonenumber
                          )
                        }}
                      </h4>
                    </div>
                  </div>

                  <div class="row">
                    <div class="col-md-4 text-muted">
                      <h4>
                        <b> <i class="fa fa-"></i> Bank Account </b>
                      </h4>
                    </div>

                    <div class="col-md-8">
                      <h4
                        v-if="res.subject_bankaccount_info"
                        class="txt-color-darken gs-word-wrap"
                      >
                        {{
                          helpers.blankSpaceAndNullCheck(
                            res.subject_bankaccount_info
                          )
                        }}
                      </h4>
                    </div>
                  </div>

                  <div class="row">
                    <div class="col-md-4 text-muted">
                      <h4>
                        <b> <i class="fa fa-"></i> Gender </b>
                      </h4>
                    </div>

                    <div class="col-md-8">
                      <h4
                        v-if="res.subject_sex"
                        class="txt-color-darken gs-word-wrap"
                      >
                        {{ helpers.blankSpaceAndNullCheck(res.subject_sex) }}
                      </h4>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="row">
            <div class="col-sm-12">
              <hr />

              <div class="padding-10">
                <tabs>
                  <tab title="Professional" pull-right>
                    <div class="padding-top-10">
                      <div class="row">
                        <div class="col-md-4 text-muted">
                          <h4>
                            <b>Position</b>
                          </h4>
                        </div>

                        <div class="col-md-8">
                          <h4
                            v-if="res.subject_position"
                            class="txt-color-darken gs-word-wrap"
                          >
                            {{
                              helpers.blankSpaceAndNullCheck(
                                res.subject_position
                              )
                            }}
                          </h4>
                        </div>
                      </div>

                      <div class="row">
                        <div class="col-md-4 text-muted">
                          <h4>
                            <b>Organization</b>
                          </h4>
                        </div>

                        <div class="col-md-8">
                          <h4
                            v-if="res.subject_organization"
                            class="txt-color-darken gs-word-wrap"
                          >
                            {{
                              helpers.blankSpaceAndNullCheck(
                                res.subject_organization
                              )
                            }}
                          </h4>
                        </div>
                      </div>

                      <div class="row">
                        <div class="col-md-4 text-muted">
                          <h4>
                            <b>Professional Email</b>
                          </h4>
                        </div>

                        <div class="col-md-8">
                          <h4
                            v-if="res.subject_professionalemail"
                            class="txt-color-darken gs-word-wrap"
                          >
                            {{
                              helpers.blankSpaceAndNullCheck(
                                res.subject_professionalemail
                              )
                            }}
                          </h4>
                        </div>
                      </div>

                      <div class="row">
                        <div class="col-md-4 text-muted">
                          <h4>
                            <b>Post Rank</b>
                          </h4>
                        </div>

                        <div class="col-md-8">
                          <h4
                            v-if="res.subject_pastrank"
                            class="txt-color-darken gs-word-wrap"
                          >
                            {{
                              helpers.blankSpaceAndNullCheck(
                                res.subject_pastrank
                              )
                            }}
                          </h4>
                        </div>
                      </div>
                    </div>
                  </tab>
                  <tab title="Critical" pull-right>
                    <div class="row">
                      <div class="col-md-4 text-muted">
                        <h4>
                          <b> <i class="fa fa-"></i>Anti-National </b>
                        </h4>
                      </div>

                      <div class="col-md-8">
                        <h4 class="txt-color-darken gs-word-wrap">
                          <template v-if="res.subject_antinational">
                            <template
                              v-if="
                                helpers.convertToLower(
                                  res.subject_antinational
                                ) == 'yes'
                              "
                            >
                              <span style="color:red;">
                                {{
                                  helpers.blankSpaceAndNullCheck(
                                    res.subject_antinational
                                  )
                                }}
                              </span>
                            </template>
                            <template v-else>
                              {{
                                helpers.blankSpaceAndNullCheck(
                                  res.subject_antinational
                                )
                              }}
                            </template>
                          </template>
                        </h4>
                      </div>
                    </div>

                    <div class="row">
                      <div class="col-md-4 text-muted">
                        <h4>
                          <b> <i class="fa fa-"></i>Category </b>
                        </h4>
                      </div>

                      <div class="col-md-8">
                        <h4
                          v-if="res.subject_category"
                          class="txt-color-darken gs-word-wrap"
                        >
                          {{
                            helpers.blankSpaceAndNullCheck(res.subject_category)
                          }}
                        </h4>
                      </div>
                    </div>

                    <div class="row">
                      <div class="col-md-4 text-muted">
                        <h4>
                          <b> <i class="fa fa-"></i>Assets in Control </b>
                        </h4>
                      </div>

                      <div class="col-md-8">
                        <h4
                          v-if="res.subject_assetsincontrol"
                          class="txt-color-darken gs-word-wrap"
                        >
                          {{
                            helpers.blankSpaceAndNullCheck(
                              res.subject_assetsincontrol
                            )
                          }}
                        </h4>
                      </div>
                    </div>

                    <div class="row">
                      <div class="col-md-4 text-muted">
                        <h4>
                          <b> <i class="fa fa-"></i>Google Play </b>
                        </h4>
                      </div>

                      <div class="col-md-8">
                        <h4
                          v-if="res.subject_googleplay"
                          class="txt-color-darken gs-word-wrap"
                        >
                          {{
                            helpers.blankSpaceAndNullCheck(
                              res.subject_googleplay
                            )
                          }}
                        </h4>
                      </div>
                    </div>

                    <div class="row">
                      <div class="col-md-4 text-muted">
                        <h4>
                          <b> <i class="fa fa-"></i>ICloud </b>
                        </h4>
                      </div>

                      <div class="col-md-8">
                        <h4
                          v-if="res.subject_icloud"
                          class="txt-color-darken gs-word-wrap"
                        >
                          {{
                            helpers.blankSpaceAndNullCheck(res.subject_icloud)
                          }}
                        </h4>
                      </div>
                    </div>
                  </tab>
                  <tab title="Reports" pull-right>
                    <data-table
                      ref="table"
                      :fields="fields"
                      :opts="options"
                      :details="details"
                      class="w-100"
                    ></data-table>
                  </tab>
                </tabs>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- end row -->
    </div>

    <!-- end row -->

    <div slot="footer">
      <btn @click="open = false">Cancel</btn>
    </div>
  </modal>
</template>

<script>
import Loading from "vue-loading-overlay";
import { mapGetters } from "vuex";

import { helpers } from "@/utils";
import DataTable from "@/components/DataTable";
import Mark from "mark.js";

export default {
  components: { DataTable, Loading },

  data: function() {
    return {
      helpers: helpers,
      isLoading: true,
      res: [],
      open: false,
      id: null,
      options: {
        /*eslint-disable */
        dom: "tip",
        /*eslint-enable */
        responsive: true,
        processing: true,
        searching: false,
        destroy: true,
        ordering: true,
        lengthChange: false,
        pageLength: 5,
        serverSide: false
      },
      fields: {
        "_source.log_id": { label: "Report Id", sortable: true },
        "_source.title": {
          label: "Report Title",
          sortable: true,
          searchable: true,
          defaultOrder: "desc"
        },
        "_source.from": { label: "From", sortable: false, searchable: true },
        "_source.to": { label: "To" },
        "_source.date": {
          label: "DateTime",
          sortable: true,
          searchable: true
        }
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
    jq() {
      return this.jquery || window.jQuery;
    }
  },

  beforeDestroy() {
    //this.$root.$off();
  },

  methods: {
    show: function(id) {
      const vm = this;
      vm.id = id;
      vm.open = true;
      vm.isLoading = true;

      Promise.all([
        vm.helpers.post("/api/profile/details", {
          sub_id: vm.id
        }),
        vm.helpers.post("/api/profile/reports", {
          sub_id: vm.id
        })
      ])
        .then(([profile, reports]) => {
          // do something with both responses
          vm.isLoading = false;
          if (profile.data.length > 0) {
            vm.res = profile.data[0]._source;
          }

          vm.doLoadTable(reports);
        })
        .catch(error => {
          console.log(error);
        });
    },
    doLoadTable(reports) {
      const vm = this;
      const jq = vm.jq;
      const table = this.$refs.table;
      const $el = jq(table.$el);

      if (reports.data.length > 0) {
        table.setTableData(reports.data);
      } else {
        table.setTableData([]);
      }

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
      vm.highlight();
    },

    callback(msg) {
      const vm = this;
      const jq = vm.jq;
      const table = this.$refs.table;
      const $el = jq(table.$el);

      vm.details.dt = null;

      $el.off("click", "td.details-control");
    },
    highlight() {
      const vm = this;
      if (vm.globals.keyword && vm.globals.keyword !== "") {
        let keyword = vm.helpers.replaceQuotes(vm.globals.keyword);

        setTimeout(() => {
          let mailInfo = vm.$refs.modal.$el.querySelector("div.well");
          let highlight = new Mark(mailInfo);

          let options = {
            acrossElements: true,
            ignorePunctuation: ['"']
          };

          highlight.mark(keyword, options);
        });
      }
    },

    mapSrc(filepath) {
      let hostPath = "..";

      if (filepath) {
        return "http://hawk.cnc.com" + filepath.split(hostPath).join("");
      }
    },

    imgErrorPlaceholder(e) {
      const vm = this;
      const _obj = vm.response[0]._source;

      if (vm.helpers.undefinedCheck(_obj.subject_sex).toLowerCase() == "male")
        e.target.src = "/img/avatars/m1.jpg";
      else if (
        vm.helpers.undefinedCheck(_obj.subject_sex).toLowerCase() == "female"
      )
        e.target.src = "/img/avatars/f1.jpg";
      else e.target.src = "/img/avatars/u1.jpg";
    },

    bind(key, i = 0) {
      let value = this.response[i]["_source"][key];
      return this.helpers.blankSpaceAndNullCheck(value);
    }
  }
};
</script>
