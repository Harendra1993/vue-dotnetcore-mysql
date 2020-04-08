<template>
  <div>
    <video id="video" ref="video" src="" style="width:300px;" controls></video>
  </div>
</template>

<script>
export default {
  props: {
    opts: {
      type: Object,
      default() {
        return {};
      }
    },
    src: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      options: {
        autoplay: false
      }
    };
  },
  computed: {
    jq() {
      return this.jquery || window.jQuery || window.$;
    }
  },
  created() {
    const vm = this;
    const jq = vm.jq;

    // allow user to override default options
    if (vm.opts) {
      vm.options = jq.extend({}, vm.options, vm.opts);
    }
  },
  mounted() {
    const vm = this;

    vm.setupVideo();
    // vm.hideControls();
  },
  methods: {
    setupVideo() {
      const vm = this;
      vm.$refs.video.src = vm.src;
      //"http://iandevlin.github.io/mdn/video-player/video/tears-of-steel-battle-clip-medium.mp4";
      if (vm.options.autoplay) this.$refs.video.play();
    },

    hideControls() {
      const vm = this;
      const jq = vm.jq;

      jq("video").hover(function(event) {
        if (event.type === "mouseenter") {
          jq(this).attr("controls", "");
        } else if (event.type === "mouseleave") {
          jq(this).removeAttr("controls");
        }
      });
    }
  }
};
</script>
