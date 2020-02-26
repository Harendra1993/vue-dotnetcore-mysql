<template>
  <div
    :class="[
      obj.messagetype.toLowerCase() == 'incoming message'
        ? 'bubble you'
        : 'bubble me',
      color,
      obj.selected ? 'selected' : ''
    ]"
  >
    <template
      v-if="
        obj.type.toLowerCase() == 'text' ||
          obj.type.toLowerCase() == 'link' ||
          obj.type.toLowerCase() == ''
      "
      >{{ obj.message }}</template
    >
    <template v-else-if="obj.type.toLowerCase() == 'deleted message'">
      <span title="Deleted Messege" class="text-muted">
        <i class="fa fa-trash"></i>
        {{ obj.message }}
      </span>
    </template>
    <template v-else-if="obj.type.toLowerCase() == 'audio'">
      <audio-player
        :src="mapSrc(obj.attachments_path)"
        oncontextmenu="return false;"
      ></audio-player>
      {{ obj.message }}
    </template>
    <template v-else-if="obj.type.toLowerCase() == 'video'">
      <video-player
        :src="mapSrc(obj.attachments_path)"
        oncontextmenu="return false;"
      ></video-player>
      {{ obj.message }}
    </template>
    <template v-else>
      <template v-if="getExtensionFromStr(obj.attachments_path) == 'pdf'">
        <a
          :href="mapSrc(obj.attachments_path)"
          :title="getFileName(obj.attachments_path)"
          target="_blank"
        >
		<div class="icon">
            <i class="far fa-file-pdf fa-6x"></i>
          </div>
        </a>
      </template>
      <template
        v-else-if="getExtensionFromStr(obj.attachments_path) == 'html'"
        target="_blank"
      >
        <a
          :href="mapSrc(obj.attachments_path)"
          :title="getFileName(obj.attachments_path)"
          target="_blank"
        >
          <div class="icon">
            <i class="far fa-file-code fa-6x"></i>
          </div>
        </a>
      </template>
      <template
        v-else-if="
          getExtensionFromStr(obj.attachments_path) == 'xls' ||
            getExtensionFromStr(obj.attachments_path) == 'xlsx'
        "
      >
        <a
          :href="mapSrc(obj.attachments_path)"
          :title="getFileName(obj.attachments_path)"
          :download="getFileName(obj.attachments_path)"
          target="_blank"
        >
          <div class="icon">
            <i class="far fa-file-excel fa-6x"></i>
          </div>
        </a>
      </template>
      <template
        v-else-if="
          getExtensionFromStr(obj.attachments_path) == 'ppt' ||
            getExtensionFromStr(obj.attachments_path) == 'pptx'
        "
      >
        <a
          :href="mapSrc(obj.attachments_path)"
          :title="getFileName(obj.attachments_path)"
          :download="getFileName(obj.attachments_path)"
          target="_blank"
        >
          <div class="icon">
            <i class="far fa-file-powerpoint fa-6x"></i>
          </div>
        </a>
      </template>
      <template
        v-else-if="
          getExtensionFromStr(obj.attachments_path) == 'doc' ||
            getExtensionFromStr(obj.attachments_path) == 'docx'
        "
      >
        <a
          :href="mapSrc(obj.attachments_path)"
          :title="getFileName(obj.attachments_path)"
          :download="getFileName(obj.attachments_path)"
          target="_blank"
        >
          <div class="icon">
            <i class="far fa-file-word fa-6x"></i>
          </div>
        </a>
      </template>
      <template v-else-if="getExtensionFromStr(obj.attachments_path) == 'csv'">
        <a
          :href="mapSrc(obj.attachments_path)"
          :title="getFileName(obj.attachments_path)"
          :download="getFileName(obj.attachments_path)"
          target="_blank"
        >
          <div class="icon">
            <i class="far fa-file-csv fa-6x"></i>
          </div>
        </a>
      </template>
      <template
        v-else-if="
          getExtensionFromStr(obj.attachments_path) == 'jpeg' ||
            getExtensionFromStr(obj.attachments_path) == 'jpg' ||
            getExtensionFromStr(obj.attachments_path) == 'png' ||
            getExtensionFromStr(obj.attachments_path) == 'bmp' ||
            getExtensionFromStr(obj.attachments_path) == 'gif' ||
            getExtensionFromStr(obj.attachments_path) == 'thumb'
        "
      >
        <img
          :src="mapSrc(obj.attachments_path)"
          class="center"
          oncontextmenu="return false;"
          @error="imgErrorPlaceholder"
        />
      </template>
      <template v-else>
        <template
          v-if="obj.attachments_path != '' && obj.attachments_path != null"
        >
          <a
            :href="mapSrc(obj.attachments_path)"
            :title="getFileName(obj.attachments_path)"
            :download="getFileName(obj.attachments_path)"
            target="_blank"
          >
            <div class="icon">
              <i class="fa fa-file-alt fa-6x"></i>
            </div>
          </a>
        </template>
      </template>
      <small>{{ obj.message }}</small>
    </template>
    <span class="message-date">&nbsp;{{ helpers.getTime(obj.datetime) }}</span>
  </div>
</template>

<script>
import AudioPlayer from "@/components/ui/AudioPlayer";
import VideoPlayer from "@/components/ui/VideoPlayer";
import { helpers } from "@/utils";

export default {
  components: {
    AudioPlayer,
    VideoPlayer
  },
  data: function() {
    return {
      helpers: helpers
    };
  },
  props: {
    obj: {
      type: Object,
      required: true
    }
  },
  computed: {
    color() {
      const vm = this;
      const data = vm.obj.table_name.split("_");
      switch (data[1].toLowerCase()) {
        case "whatsapp":
          return "whatsapp";
          break;

        case "facebookchat":
          return "fb";
          break;

        case "viber":
          return ``;
          break;
      }
    }
  },

  methods: {
    mapSrc(attPath) {
      let hostPath = process.env.MIX_MOBILE_MESSENGERS;

      return (
        "http://192.168.50.205/" +
        attPath.replace(hostPath, "/Data/mobile/filesystem").replace("/mobile/filesystem","")
      );
    },
    imgErrorPlaceholder(e) {
      //e.target.src = "/img/not-found.png";
    },
    getExtensionFromStr(str) {
      let _value;
      if (str != "undefined") _value = str.split(".");
      return _value.length > 1 ? _value[_value.length - 1] : "";
    },
    getFileName(str) {
      let filename = str.replace(/^.*[\\\/]/, "");
      if (filename.length >= 22) {
        return filename.substring(0, 20) + "...";
      }
      return filename;
    }
  }
};
</script>
