<template>
  <modal v-model="open" @hide="callback" ref="modal" size="lg">
    <span slot="title">Headers Details</span>

    <table class="table table-hover">
      <!-- <thead>
        <tr>
          <td>Key</td>
          <td>Value</td>
        </tr>
      </thead>-->
      <tbody>
        <tr v-for="(item, index) in res" :key="index">
          <td>
            <b>{{item.HeaderKey}}</b>
          </td>
          <td>{{item.HeaderValue}}</td>
        </tr>
      </tbody>
    </table>

    <div slot="footer">
      <btn @click="open = false">Cancel</btn>
    </div>
  </modal>
</template>

<script>
import { helpers } from "@/utils";

export default {
  data: function() {
    return {
      helpers: helpers,
      res: [],
      open: false
    };
  },

  beforeDestroy() {
    //this.$root.$off();
  },

  methods: {
    show: function(id) {
      const vm = this;
      vm.id = id;
      vm.open = true;
      helpers
        .post("api/email/headers", {
          id: vm.id
        })
        .then(({ data }) => {
          if (data.data.length > 0) vm.res = data.data;
          else vm.helpers.notify({ type: "danger", body: "No data Found." });
        })
        .catch(error => {
          console.log(
            "WHoops.! Some Error Occurred : Fetching Header[E113].",
            error
          );
        });
    },

    callback(msg) {
      const vm = this;
    }
  }
};
</script>