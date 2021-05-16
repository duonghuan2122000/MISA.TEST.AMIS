<template>
  <button
    class="btn"
    :class="icon ? icon : ''"
    @click.prevent="$emit('click', $event)"
  ></button>
</template>

<script>
//#region export
export default {
  //#region props
  props: {
    /**
     * Icon class
     * CreatedBy: dbhuan 15/05/2021
     */
    icon: {
      type: String,
      default: null,
    },
  },
  //#endregion
};
//#endregion
</script>