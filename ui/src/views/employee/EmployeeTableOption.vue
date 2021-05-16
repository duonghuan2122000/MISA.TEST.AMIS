<template>
  <div class="dialog">
    <div
      class="dialog-background"
      style="background-color: transparent"
      @click.prevent="closeDialog"
    ></div>
    <div class="dialog-content">
      <div
        class="table-option-dropdown"
        :style="{ top: top + 'px', left: left + 'px' }"
      >
        <div class="dropdown-item">Nhân bản</div>
        <div class="dropdown-item" @click="$emit('onClickBtnDel')">Xóa</div>
        <div class="dropdown-item">Ngưng sử dụng</div>
      </div>
    </div>
  </div>
</template>

<script>
//#region export
export default {
  //#region props
  props: {

    /**
     * Vị trí của component so với top
     * CreatedBy: dbhuan 16/05/2021
     */
    top: {
      type: Number,
      default: 0,
    },

    /**
     * Vị trí của component so với left
     * CreatedBy: dbhuan 16/05/2021
     */
    left: {
      type: Number,
      default: 0,
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