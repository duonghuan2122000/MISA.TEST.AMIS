<template>
  <div style="display: inline-block">
    <input
      type="radio"
      :id="id"
      class="radio"
      :name="name"
      :value="value"
      :checked="checked"
      @input="$emit('change', $event.target.value)"
    />
    <label :for="id"></label>
  </div>
</template>

<script>
//#region export
export default {
    //#region props
  props: {
    /**
     * name của radio
     * CreatedBy: dbhuan 16/05/2021
     */
    name: {
      type: String,
      required: true,
    },

    /**
     * Giá trị của radio
     * CreatedBy: dbhuan 16/05/2021
     */
    value: {
      type: String,
      required: true,
    },

    /**
     * Xác định radio có được check hay không
     * CreatedBy: dbhuan 16/05/2021
     */
    checked: {
      type: Boolean,
      default: null,
    },
  },
  //#endregion

  //#region data
  data() {
    return {
      id: Math.random().toString(36).substr(0, 5),
    };
  },
  //#endregion
};
//#endregion
</script>