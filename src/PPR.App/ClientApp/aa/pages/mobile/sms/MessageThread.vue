<template>
  <div
    :class="[
      obj.messagetype.toLowerCase() == 'inbox'
        ? 'bubble me'
        : 'bubble you',
      obj.selected ? 'selected' : ''
    ]"
  >
    <template>{{
      obj.message
    }}</template>

    <span class="message-date">&nbsp;{{ helpers.getTime(obj.datetime) }}</span>
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
    }
  }
};
</script>
