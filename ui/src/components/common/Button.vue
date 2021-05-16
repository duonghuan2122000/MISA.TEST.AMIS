<template>
  <button
    :class="['btn', color ? 'btn-' + color : '']"
    @click.prevent="$emit('click')"
  >
    {{ text }}
  </button>
</template>

<script>
//#region export
export default {
  //#region props
  props: {
    /**
     * Background color của button.
     * primary, secondary
     * CreatedBy: dbhuan 15/05/2021
     */
    color: {
      type: String,
      default: "primary",
    },

    /**
     * Text hiển thị của button.
     * CreatedBy: dbhuan 15/05/2021
     */
    text: {
      type: String,
      default: "",
    },
  },
  //#endregion
};
//#endregion
</script>