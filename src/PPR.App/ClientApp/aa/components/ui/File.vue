<template>
  <div class="file">
    <template v-if="obj.extension == '.pdf'">
      <a :href="src" target="_blank">
        <div class="size">{{ helpers.bytesToSize(obj.filesize) }}</div>
        <div class="icon">
          <i class="far fa-file-pdf fa-10x"></i>
        </div>
        <div class="file-name">
          {{ limitchars(obj.filename) }}
          <br />
          <small>Created: {{ helpers.fullDateTime(obj.datetime) }}</small>
        </div>
      </a>
    </template>

    <template v-else-if="obj.extension == '.html'">
      <a :href="src" target="_blank">
        <div class="icon">
          <i class="far fa-file-code fa-10x"></i>
        </div>
        <div class="file-name">
          {{ limitchars(obj.filename) }}
          <br />
          <small>Created: {{ helpers.fullDateTime(obj.datetime) }}</small>
        </div>
      </a>
    </template>
    <template v-else-if="obj.extension == '.xls' || obj.extension == '.xlsx'">
      <a :href="src" :download="src">
        <div class="icon">
          <i class="far fa-file-excel fa-10x"></i>
        </div>
        <div class="file-name">
          {{ limitchars(obj.filename) }}
          <br />
          <small>Created: {{ helpers.fullDateTime(obj.datetime) }}</small>
        </div>
      </a>
    </template>
    <template v-else-if="obj.extension == '.ppt' || obj.extension == '.pptx'">
      <a :href="src" :download="src">
        <div class="icon">
          <i class="far fa-file-powerpoint fa-10x"></i>
        </div>
        <div class="file-name">
          {{ limitchars(obj.filename) }}
          <br />
          <small>Created: {{ helpers.fullDateTime(obj.datetime) }}</small>
        </div>
      </a>
    </template>
    <template v-else-if="obj.extension == '.doc' || obj.extension == '.docx'">
      <a :href="src" :download="src">
        <div class="icon">
          <i class="far fa-file-word fa-10x"></i>
        </div>
        <div class="file-name">
          {{ limitchars(obj.filename) }}
          <br />
          <small>Created: {{ helpers.fullDateTime(obj.datetime) }}</small>
        </div>
      </a>
    </template>
    <template v-else-if="obj.extension == '.csv'">
      <a :href="src" :download="src">
        <div class="icon">
          <i class="far fa-file-csv fa-10x"></i>
        </div>
        <div class="file-name">
          {{ limitchars(obj.filename) }}
          <br />
          <small>Created: {{ helpers.fullDateTime(obj.datetime) }}</small>
        </div>
      </a>
    </template>
    <template v-else-if="obj.extension == '.txt'">
      <a :href="src" target="_blank">
        <div class="icon">
          <i class="far fa-file-alt fa-10x"></i>
        </div>
        <div class="file-name">
          {{ limitchars(obj.filename) }}
          <br />
          <small>Created: {{ helpers.fullDateTime(obj.datetime) }}</small>
        </div>
      </a>
    </template>
    <template
      v-else-if="
        obj.extension == '.jpeg' ||
          obj.extension == '.jpg' ||
          obj.extension == '.png' ||
          obj.extension == '.bmp' ||
          obj.extension == '.gif' ||
          obj.extension == '.thumb'
      "
    >
      <a>
        <div class="size">{{ helpers.bytesToSize(obj.filesize) }}</div>
        <div class="icon">
          <img
            :src="mapSrc(src)"
            class="center"
            oncontextmenu="return false;"
          />
        </div>
        <div class="file-name">
          {{ limitchars(obj.filename) }}
          <br />
          <small>Created: {{ helpers.fullDateTime(obj.datetime) }}</small>
        </div>
      </a>
    </template>
    <template v-else>
      <a :href="src" :download="src">
        <div class="icon">
          <i class="far fa-file-alt fa-10x"></i>
        </div>
        <div class="file-name">
          {{ limitchars(obj.filename) }}
          <br />
          <small>{{ helpers.getTime(obj.datetime) }}</small>
        </div>
      </a>
    </template>
  </div>
</template>
<script>
import { helpers } from "@/utils";

export default {
  data: function() {
    return {
      helpers: helpers
    };
  },
  props: {
    obj: {
      type: Object,
      required: true
    },
    src: {
      type: String,
      required: true
    }
  },
  methods:{
    limitchars(name) {
      const vm=this;
      if (name.length >= 22) {
        return (
          name.substring(0, 20) + "...." + vm.obj.extension
        );
      }

      return name;
    }
  }
};
</script>

<style scoped>
.size {
  position: absolute;
  top: 0px;
  right: 0px;
  z-index: 1;
  /* background: #e74c3c; */
  width: 45px;
  height: 25px;
  color: #888888;
  font-weight: 700;
  text-align: center;
  -webkti-box-sizing: border-box;
  -moz-box-sizing: border-box;
  box-sizing: border-box;
}
</style>
