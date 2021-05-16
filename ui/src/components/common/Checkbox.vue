<template>
  <div style="display: inline-block">
    <input type="checkbox" class="checkbox" :id="id" />
    <label :for="id"></label>
  </div>
</template>

<script>
//#region export
export default {
    //#region data
    data(){
        return {
            id: Math.random().toString(36).substr(0, 5)
        }
    }
    //#endregion
}
//#endregion
</script>