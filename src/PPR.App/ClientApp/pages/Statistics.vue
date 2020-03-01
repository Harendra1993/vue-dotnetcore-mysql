<template>
  <div class="stats">
    <div class="row">
      <div class="col-lg-4">
        <div class="widget style1 navy-bg">
          <div class="row vertical-align">
            <div class="col-sm-3">
              <i class="fa fa-users fa-2x"></i>
            </div>
            <div class="col-sm-3"></div>
            <div class="col-sm-6 text-right">
              <h2 class="font-bold">{{globals.searchCounts.profile}}</h2>
            </div>
          </div>
        </div>
      </div>
      <div class="col-lg-4">
        <div class="widget style1 navy-bg">
          <div class="row vertical-align">
            <div class="col-sm-3">
              <i class="fa fa-address-book fa-3x"></i>
            </div>
            <div class="col-sm-9 text-right">
              <h2 class="font-bold">{{globals.searchCounts.contacts}}</h2>
            </div>
          </div>
        </div>
      </div>
      <div class="col-lg-4">
        <div class="widget style1 navy-bg">
          <div class="row vertical-align">
            <div class="col-sm-3">
              <i class="fa fa-comments fa-3x"></i>
            </div>
            <div class="col-sm-9 text-right">
              <h2 class="font-bold">{{globals.searchCounts.messengers}}</h2>
            </div>
          </div>
        </div>
      </div>
      <div class="col-lg-4">
        <div class="widget style1 navy-bg">
          <div class="row vertical-align">
            <div class="col-sm-3">
              <i class="fa fa-sms fa-3x"></i>
            </div>
            <div class="col-sm-9 text-right">
              <h2 class="font-bold">{{globals.searchCounts.sms}}</h2>
            </div>
          </div>
        </div>
      </div>

      <div class="col-lg-4">
        <div class="widget style1 navy-bg">
          <div class="row vertical-align">
            <div class="col-sm-3">
              <i class="fa fa-envelope fa-3x"></i>
            </div>
            <div class="col-sm-9 text-right">
              <h2 class="font-bold">{{globals.searchCounts.emails}}</h2>
            </div>
          </div>
        </div>
      </div>
      <div class="col-lg-4">
        <div class="widget style1 navy-bg">
          <div class="row vertical-align">
            <div class="col-sm-3">
              <i class="fa fa-paperclip fa-3x"></i>
            </div>
            <div class="col-sm-9 text-right">
              <h2 class="font-bold">{{globals.searchCounts.mobile_attachments}}</h2>
            </div>
          </div>
        </div>
      </div>
      <div class="col-lg-4">
        <div class="widget style1 navy-bg">
          <div class="row vertical-align">
            <div class="col-sm-3">
              <i class="fa fa-tag fa-3x"></i>
            </div>
            <div class="col-sm-9 text-right">
              <h2 class="font-bold">{{globals.searchCounts.tagged}}</h2>
            </div>
          </div>
        </div>
      </div>
      <div class="col-lg-4">
        <div class="widget style1 navy-bg">
          <div class="row vertical-align">
            <div class="col-sm-3">
              <i class="fa fa-file-search fa-3x"></i>
            </div>
            <div class="col-sm-9 text-right">
              <h2 class="font-bold">{{globals.searchCounts.untagged_files}}</h2>
            </div>
          </div>
        </div>
      </div>
      <div class="col-lg-4">
        <div class="widget style1 navy-bg">
          <div class="row vertical-align">
            <div class="col-sm-3">
              <i class="fa fa-file-powerpoint fa-3x"></i>
            </div>
            <div class="col-sm-9 text-right">
              <h2 class="font-bold">{{globals.searchCounts.untagged_ppts}}</h2>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import { helpers } from "@/utils";
import { countMixin } from "@/mixins/countMixin";

export default {
  mixins: [countMixin],
  data() {
    return {
      helpers: helpers,
      profile: 0
    };
  },
  computed: {
    ...mapGetters(["globals"])
  }
};
</script>
<style scoped>
.stats {
  z-index: 9;
  text-align: center;
  width: 700px;
  left: 50%;
  top: 65%;
  position: absolute;
  -webkit-transform: translate(-50%, -50%);
  transform: translate(-50%, -50%);
  white-space: normal;
}
</style>