<template>
  <div class="dialog">
    <div class="dialog-background" @click="closeDialog"></div>
    <div class="dialog-content zoomIn" style="width: 444px">
      <div class="dialog-body">
        <div class="flex-row-align-center" style="padding-top: 32px">
          <div class="icon icon-warning-48"></div>
          <div style="margin-left: 16px">
            {{ msg }}
          </div>
        </div>
      </div>
      <div class="dialog-footer">
        <div class="dialog-footer-left">
          <Button text="Không" color="secondary" @click="closeDialog" />
        </div>
        <div class="dialog-footer-right">
          <Button text="Có" color="primary" @click="$emit('onPositive')" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
//#region import
import Button from "./Button.vue";
//#endregion

//#region export
export default {
  //#region components
  components: {
    Button,
  },
  //#endregion

  //#region props
  props: {
    /**
     * Lời nhắn
     * CreatedBy: dbhuan 16/05/2021
     */
    msg: {
      type: String,
      default: "",
    },
  },
  //#endregion

  //#region methods
  methods: {
    /**
     * Phương thức đóng dialog.
     * CreatedBy: dbhuan (09/05/2021)
     */
    closeDialog() {
      this.$emit("onClose");
    },
  },
  //#endregion
};
//#endregion
</script>