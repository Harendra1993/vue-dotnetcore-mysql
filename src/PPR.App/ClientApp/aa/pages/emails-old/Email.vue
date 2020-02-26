<template>
  <!-- <modal v-model="open" @hide="callback" ref="modal" size="lg"> -->
  <div>
    <loading :active.sync="isLoading" :is-full-page="false"></loading>
    <div v-show="!isLoading" style="overflow-y:visible">
      <div ref="print">
        <div class="email-body" v-if="meta">
          <div ref="hdr" class="mail-box-header custom-header">
            <div class="pull-right tooltip-demo">
              <a
                @click="extract('print')"
                class="btn btn-white btn-sm"
                data-toggle="tooltip"
                data-placement="top"
                title="Print"
                data-original-title="Print email"
              >
                <i class="fa fa-print"></i>
              </a>
              <a
                @click="extract('html')"
                class="btn btn-white btn-sm"
                data-toggle="tooltip"
                data-placement="top"
                title="Download As HTML"
                data-original-title="Download As Html"
              >
                <i class="fa fa-download"></i>
              </a>
              <a
                class="btn btn-default"
                @click="showHeaders()"
                data-toggle="tooltip"
                data-placement="top"
                title="Show Headers"
              >
                <i class="fa fa-info-circle"></i>
              </a>
            </div>
            <h2 class="font-normal" v-html="meta.email_subject"></h2>
            <div class="mail-tools tooltip-demo m-t-md">
              <h4>
                <span
                  class="pull-right font-normal"
                  v-html="meta.datetime"
                ></span>
                <!--Start From Email -->
                <div>
                  <span class="font-normal">From:</span>
                  <a @click="redirect(meta.from)" target="_blank">{{
                    meta.from
                  }}</a>
                </div>
                <!--End From Email -->

                <!--Start To Email -->
                <div class="mt-1">
                  <span class="font-normal">To:</span>
                  <a @click="redirect(to)" v-html="to"></a>
                  <template v-if="extraTo.length > 0">
                    &nbsp;&nbsp;
                    <a @click="showExtraTo = !showExtraTo">
                      <span
                        v-if="!showExtraTo"
                        class="fa fa-plus-circle txt-color-blueDark"
                        title="Show details"
                        >&nbsp;More</span
                      >
                      <span
                        v-if="showExtraTo"
                        class="fa fa-minus-circle txt-color-blueDark"
                        title="Hide details"
                        >&nbsp;Less</span
                      >
                    </a>
                    <collapse v-model="showExtraTo">
                      <div>
                        <span v-for="(to, index) in extraTo" :key="index">
                          <span
                            @click="redirect(to)"
                            v-html="to"
                            class="label label-default"
                            style="cursor:pointer;"
                          ></span
                          >&nbsp;
                        </span>
                      </div>
                    </collapse>
                  </template>
                </div>
                <!--End To Email -->
              </h4>
            </div>
          </div>
          <div
            class="mail-body"
            v-for="(item, index) in data.body"
            :key="index"
          >
            <iframe
              id="iref"
              :height="iframeHeight"
              class="frame"
              scrolling="no"
              v-on:load="doLoadBody()"
              frameborder="0"
              ref="iref"
            ></iframe>
          </div>
        </div>
      </div>

      <div v-if="data.attachments.length > 0" class="attchment">
        <p class="custom-grey-bg">
          <span>
            <i class="fa fa-paperclip"></i>
            {{ attachmentsCount }} Attachments
          </span>
          <!-- |
          <span
            title="Download All Attachments As Zip"
            @click="downloadMultiple()"
            class="font-bold custom-pointer"
            >Download All</span>-->
        </p>

        <div class="attchment">
          <div
            class="file-box"
            v-for="(att, index) in data.attachments"
            :key="index"
          >
            <div class="file">
              <a @click="download(index)">
                <span class="corner"></span>
                <div class="icon">
                  <i
                    v-if="att.attachments_type == '.pdf'"
                    class="fa fa-file-pdf-o"
                  ></i>
                  <i
                    v-else-if="
                      att.attachments_type == '.ppt' ||
                        att.attachments_type == '.pptx'
                    "
                    class="fa fa-file-powerpoint-o"
                  ></i>
                  <i
                    v-else-if="
                      att.attachments_type == '.doc' ||
                        att.attachments_type == '.docx'
                    "
                    class="fa fa-file-word-o"
                  ></i>
                  <i
                    v-else-if="
                      att.attachments_type == '.xls' ||
                        att.attachments_type == '.xlxs'
                    "
                    class="fa fa-file-excel-o"
                  ></i>
                  <i
                    v-else-if="att.attachments_type == '.zip'"
                    class="fa fa-file-zip"
                  ></i>
                  <i
                    v-else-if="
                      att.attachments_type == '.jpeg' ||
                        att.attachments_type == '.jpg' ||
                        att.attachments_type == '.png' ||
                        att.attachments_type == '.bmp' ||
                        att.attachments_type == '.gif' ||
                        att.attachments_type == '.thumb'
                    "
                  >
                    <img
                      class="img-responsive"
                      :src="imageSrc(att.attachments_path)"
                    />
                  </i>
                  <i v-else class="fa fa-file-o"></i>
                </div>

                <div class="file-name">
                  {{ limitchars(index) }}
                  <br />
                  {{ att.attachments_size }}
                </div>
              </a>
            </div>
          </div>
          <div class="clearfix"></div>
        </div>
      </div>
    </div>
  </div>

  <!-- end row -->

  <!-- <div slot="footer">
      <btn @click="open = false">Cancel</btn>
    </div>
  </modal>-->
</template>

<script>
import { mapGetters } from "vuex";
import Downloader from "file-saver";
import Mark from "mark.js";
import Loading from "vue-loading-overlay";
import Cookies from "js-cookie";

import { helpers } from "@/utils";

export default {
  components: {
    Loading,
    Cookies
  },
  data: function() {
    return {
      helpers: helpers,
      res: [],
      open: false,
      data: { body: [], attachments: [] },
      meta: null,
      to: "",
      extraTo: [],
      showExtraTo: false,
      showCC: false,
      showBCC: false,
      fullPage: true, // loader config
      isLoading: false, // loader config
      isResponse: false,
      iframeLoaded: false,
      attachments: false,
      iframeDivHeight: "100px",
      iframeHeight: "100px"
    };
  },
  computed: {
    ...mapGetters(["globals"]),
    attachmentsCount() {
      return this.data.attachments.length;
    },

    dynamicHeight() {
      if (this.data.attachments.length > 0) {
        this.iframeDivHeight = "450px !important";

        return this.iframeDivHeight;
      }

      return this.iframeDivHeight;
    }
  },

  beforeDestroy() {
    const vm = this;
    vm.$root.$off();
    // vm.dataTable = null;
  },

  methods: {
    redirect: function(emailid) {
      Cookies.set("HawkEmail", helpers.utoa(emailid), {
        path: "",
        domain: "email.cnc.com"
      });
      window.open("http://email.cnc.com/eMail/integration.php", "_blank");
      //console.log(Cookies.get("HawkEmail"));
    },

    show: function(data) {
      const vm = this;

      vm.isLoading = true;
      vm.open = true;
      vm.meta = data;
      vm.to = vm.meta.to.split(",")[0];
      vm.extraTo = vm.meta.to.split(",").slice(1);

      //console.log(vm.meta);

      helpers
        .post("/api/email-old/body", {
          id: data.id
        })
        .then(({ data }) => {
          vm.data = data.data;
          if (vm.data.body.length > 0) vm.doLoadBody();
          if (vm.data.attachments.length > 0) vm.attachments = true;
          vm.isLoading = false;
        })
        .catch(error => {
          console.log(error);
        });
    },

    callback(msg) {
      //this.$notify(`Modal dismissed with msg '${msg}'.`);
    },

    doLoadBody() {
      const vm = this;
      vm.iframeHeight = "500px";
      if (vm.$refs.iref) {
        let fr = vm.$refs.iref[0].contentDocument;
        setTimeout(() => {
          let content = vm.data.body[0]["email_body"];

          fr.body.innerHTML = content;

          vm.iframeHeight = fr.body.scrollHeight + "px";
        }, 100);
      }
      //vm.highlight();
    },

    extract(type) {
      const vm = this;

      vm.displayRole = "none";
      setTimeout(() => {
        let el = vm.$refs.print;
        let heading = el.children[0].children[0].innerHTML;
        let body =
          el.children[0].children[1].children[0].contentDocument.body.innerHTML;
        let attachment = el.children[1];

        switch (type) {
          case "print":
            let newWindow = window.open("");
            newWindow.document.write(heading);
            newWindow.document.write(body);

            if (attachment !== undefined) {
              newWindow.document.write(attachment.children[0].innerHTML);
            }

            newWindow.print();
            newWindow.close();
            this.displayRole = "";

            break;

          case "html":
            let htmlData =
              heading +
              body +
              (attachment !== undefined
                ? attachment.children[0].innerHTML
                : "");

            var file = new File(
              [htmlData],
              new Date().toLocaleString() + ".html",
              {
                type: "text/html;charset=utf-8"
              }
            );

            Downloader.saveAs(file);
            this.displayRole = "";

            this.helpers.notify({
              scope: this,
              title: "EMAIL NOTIFICATION",
              body: "Exporting Email As HTML",
              type: "warning",
              placement: "bottom-right"
            });

            break;

          default:
            this.helpers.notify({
              scope: this,
              title: "EMAIL NOTIFICATION",
              body: "Unable To Extract Information",
              type: "danger",
              placement: "bottom-left"
            });

            break;
        }
      }, 10);
    },
    showHeaders() {
      this.$root.$emit("onEmailShowHeaders", { id: this.meta.id });
    },
    download(index) {
      let url = "api/email/download/" + this.data.attachments[index].Id;

      location.href = url;

      this.helpers.notify({
        scope: this,
        title: "EMAIL NOTIFICATION",
        body: "Downloading Single Attachment",
        type: "warning",
        placement: "bottom-right"
      });
    },

    downloadMultiple() {
      let url = "api/email/downloadMultiple/" + this.meta.id;

      location.href = url;

      this.helpers.notify({
        scope: this,
        title: "EMAIL NOTIFICATION",
        body:
          "Downloading Multiple Attachments. It may Take Some Time. PLease Be Patient!",
        type: "warning",
        placement: "bottom-right"
      });
    },
    limitchars(index) {
      let str = this.data.attachments[index];

      if (str.attachments_name.length >= 22) {
        return (
          str.attachments_name.substring(0, 20) + "...." + str.attachments_type
        );
      }

      return str.attachments_name;
    },
    imageSrc(attPath) {
      let hostPath = "/var/www/RedEmail/public_html";

      return "http://email.cnc.com/" + attPath.replace(hostPath, "");
    }
  }
};
</script>
<style>
iframe#iref {
  word-break: break-all !important;
}

iframe.frame {
  width: 100%;
  border: none;
  margin-bottom: 20px;
}

.custom-pointer {
  cursor: pointer;
}
/* body.modal-open {
  height: 100vh;
  overflow-y: auto;
  padding-right: 15px;
} */

body {
  overflow-y: auto;
  overflow: hidden;
}
</style>
