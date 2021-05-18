<template>
  <div class="dialog">
    <div class="dialog-background" @click="closeDialog"></div>
    <div class="dialog-content zoomIn" style="width: 444px">
      <div class="dialog-body">
        <div class="flex-row-align-center" style="padding-top: 32px">
          <div class="icon icon-question"></div>
          <div style="margin-left: 16px">
            Dữ liệu đã bị thay đổi. Bạn có muốn cất không ?
          </div>
        </div>
      </div>
      <div class="dialog-footer">
        <div class="dialog-footer-left">
          <Button text="Hủy" color="secondary" @click="closeDialog" />
        </div>
        <div class="dialog-footer-right">
          <Button text="Không" color="secondary" @click="$emit('onNegative')" />
          <Button text="Có" color="primary" style="margin-left: 8px" @click="$emit('onPositive')" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
//#region import
import Button from "../../components/common/Button.vue";
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
    // msg: {
    //   type: String,
    //   default: "",
    // },
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