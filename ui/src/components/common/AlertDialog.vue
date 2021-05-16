<template>
  <div class="dialog">
    <div class="dialog-background"></div>
    <div class="dialog-content" style="width: 444px">
      <div class="dialog-body">
        <div class="flex-row-align-center" style="padding-top: 32px">
          <div class="icon" :class="classType"></div>
          <div style="margin-left: 16px">
            {{ msg }}
          </div>
        </div>
      </div>
      <div class="dialog-footer" v-if="type == 'warning'">
        <div class="dialog-footer-left"></div>
        <div class="dialog-footer-right">
          <Button :text="textBtn" color="primary" @click="closeDialog" />
        </div>
      </div>
      <div
        v-if="type == 'error'"
        class="dialog-footer"
        style="justify-content: center"
      >
        <Button :text="textBtn" color="primary" @click="closeDialog" />
      </div>
    </div>
  </div>
</template>

<script>
import Button from "./Button.vue";
export default {
  components: {
    Button,
  },
  props: {
    /**
     * Lời nhắn
     * CreatedBy: dbhuan 16/05/2021
     */
    msg: {
      type: String,
      default: "",
    },

    /**
     * Text button
     * CreatedBy: dbhuan 16/05/2021
     */
    textBtn: {
      type: String,
      default: "Đóng",
    },

    /**
     * Loại dialog alert
     * warning, error
     */
    type: {
      type: String,
      required: true,
    },
  },

  computed: {
    classType: function () {
      if (this.type == "warning") return "icon-warning-48";
      if (this.type == "error") return "icon-error";
      return "";
    },
  },

  methods: {
    /**
     * Phương thức đóng dialog.
     * CreatedBy: dbhuan (09/05/2021)
     */
    closeDialog() {
      this.$emit("onClose");
    },
  },
};
</script>