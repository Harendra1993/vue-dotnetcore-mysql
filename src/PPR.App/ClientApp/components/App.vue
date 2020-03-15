<template>
  <div>
    <progress-bar ref="progress" />

    <transition name="page" mode="out-in">
      <component :is="layout" v-if="layout" />
    </transition>
  </div>
</template>

<script>
import ProgressBar from "./ProgressBar";

// Load layout components dynamically.
const requireContext = require.context("@/layouts", false, /.*\.vue$/);

const layouts = requireContext
  .keys()
  .map(file => [file.replace(/(^.\/)|(\.vue$)/g, ""), requireContext(file)])
  .reduce((components, [name, component]) => {
    components[name] = component.default || component;
    return components;
  }, {});

export default {
  el: "#page-wrapper",

  components: {
    ProgressBar
  },

  data: () => ({
    layout: null,
    defaultLayout: "default"
  }),

  //   metaInfo() {
  //     const { appName } = window.config;

  //     return {
  //       title: appName,
  //       titleTemplate: `%s · ${appName}`
  //     };
  //   },

  mounted() {
    this.$progress = this.$refs.progress;
  },

  methods: {
    /**
     * Set the application layout.
     *
     * @param {String} layout
     */
    setLayout(layout) {
      if (!layout || !layouts[layout]) {
        layout = this.defaultLayout;
      }

      this.layout = layouts[layout];
    }
  }
};
</script>
