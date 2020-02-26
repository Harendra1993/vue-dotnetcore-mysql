<template>
  <modal v-model="open" @hide="callback" ref="modal" size="md">
    <span slot="title">Release Notes</span>
    <div class="ibox shadow-md-down">
      <div class="ibox-content">
        <div class="animated fadeInDown">
          <div
            class="release-note position-relative container-new px-3 text-left"
          ></div>
          <section
            class="release-note position-relative container-new py-6 px-3 text-left"
            v-for="(item, index) in data"
            :key="index"
          >
            <header class="timeline-decorator d-flex flex-items-center mb-3">
              <span
                class="version-badge d-inline-block bg-gold p-1 rounded-1 mr-2 text-bold"
              >
                <b>{{ item.version }}</b>
              </span>
              <h2 class="f3 css-truncate css-truncate-target">
                {{ item.date }}
              </h2>
            </header>
            <ul class="list-style-none change-log">
              <template v-if="item.features.length > 0">
                <li
                  class="d-flex flex-items-start mb-2"
                  v-for="(feature, index) in item.features"
                  :key="index + 'F'"
                >
                  <div class="change-badge change-badge-added">Added</div>
                  <div class="change-description" v-html="feature"></div>
                </li>
              </template>
              <template v-if="item.improvements.length > 0">
                <li
                  class="d-flex flex-items-start mb-2"
                  v-for="(improvement, index) in item.improvements"
                  :key="index + 'I'"
                >
                  <div class="change-badge change-badge-improved">Improved</div>
                  <div class="change-description" v-html="improvement"></div>
                </li>
              </template>
              <template v-if="item.bugs.length > 0">
                <li
                  class="d-flex flex-items-start mb-2"
                  v-for="(bug, index) in item.bugs"
                  :key="index + 'B'"
                >
                  <div class="change-badge change-badge-fixed">Fixed</div>
                  <div class="change-description" v-html="bug"></div>
                </li>
              </template>
            </ul>
          </section>
        </div>
      </div>
    </div>
    <div slot="footer">
      <btn @click="open = false">Cancel</btn>
    </div>
  </modal>
</template>

<script>
import json from "@/releases.json";
export default {
  data() {
    return {
      open: false,
      data: json
    };
  },

  methods: {
    show: function() {
      const vm = this;

      vm.open = true;
    },
    callback(msg) {
      //this.$notify(`Modal dismissed with msg '${msg}'.`);
    }
  }
};
</script>

<style lang="scss">
@import "../sass/release.scss";
.f3 {
  font-weight: 600;
}
</style>
