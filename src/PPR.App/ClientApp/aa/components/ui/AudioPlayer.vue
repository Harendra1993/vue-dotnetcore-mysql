<template>
  <div>
    <audio id="audio" ref="audio" src="" style="width:300px;" controls></audio>
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

    vm.setupAudio();
  },
  methods: {
    setupAudio() {
      const vm = this;
      vm.$refs.audio.src = vm.src;
      //"http://iandevlin.github.io/mdn/video-player/video/tears-of-steel-battle-clip-medium.mp4";
      if (vm.options.autoplay) this.$refs.audio.play();
    }
  }
};
</script>
