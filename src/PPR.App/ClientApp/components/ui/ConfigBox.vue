<template>
  <div class="theme-config">
    <div class="theme-config-box">
      <div class="spin-icon">
        <i class="fa fa-cogs fa-spin"></i>
      </div>
      <div class="skin-settings">
        <div class="title">
          Configuration
          <br />
          <small
            style="text-transform: none;font-weight: 400"
          >Config box designed to change Software configurations.</small>
        </div>
        <div class="setings-item">
          <span>Table View</span>

          <div class="switch">
            <div class="onoffswitch">
              <input
                type="checkbox"
                name="toggleview"
                class="onoffswitch-checkbox"
                v-model="checked"
                id="toggleview"
              />
              <label class="onoffswitch-label" for="toggleview">
                <span class="onoffswitch-inner"></span>
                <span class="onoffswitch-switch"></span>
              </label>
            </div>
          </div>
        </div>
        <!--<div class="setings-item default-skin">
                    <span class="skin-name ">
                        <a class="s-skin-0" id="btnPrint">
                            <?php //echo lang('print'); ?> 
                        </a>
                    </span>
        </div>-->
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      checked: false
    };
  },
  computed: {
    jq() {
      return this.jquery || window.jQuery;
    }
  },
  mounted() {
    const vm = this;
    const jq = vm.jq;
    jq("#toggleview").click(function() {
      if (jq("#toggleview").is(":checked")) {
        vm.$root.$emit("onTableViewChange", {
          reload: true,
          viewType: "tabular"
        });
      } else {
        vm.$root.$emit("onTableViewChange", { reload: true, viewType: "card" });
      }

      jq(".spin-icon").click();
    });

    jq(".spin-icon").click(function() {
      jq(".theme-config-box").toggleClass("show");
    });
  },
  methods: {
    active(data) {
      const vm = this;
      if (data == "tabular") vm.checked = true;
      else vm.checked = false;
    }
  }
};
</script>