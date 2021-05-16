<template>
  <tr>
    <td class="view-white pin" style="left: 0"></td>
    <td class="pin" style="left: 16px">
      <Checkbox />
    </td>
    <td>{{ employee.employeeCode }}</td>
    <td>{{ employee.employeeName }}</td>
    <td>{{ employee.genderName }}</td>
    <td style="text-align: center">02/12/2000</td>
    <td>{{ employee.identityNumber }}</td>
    <td>{{ employee.employeePosition }}</td>
    <td>{{ employee.employeeDepartmentName }}</td>
    <td>{{ employee.bankAccountNumber }}</td>
    <td>{{ employee.bankName }}</td>
    <td>{{ employee.bankBranchName }}</td>
    <td class="pin" style="right: 16px">
      <div class="table-option">
        <Button
          :color="null"
          text="Sửa"
          @click="onClickBtnEdit(employee.employeeId)"
        />
        <IconButton
          icon="icon icon-chevron-down-blue"
          @click="toggleDropdown($event, employee)"
        />
      </div>
    </td>
    <th class="view-white pin" style="right: 0"></th>
  </tr>
</template>

<script>
//#region import
import Button from "../../components/common/Button.vue";
import IconButton from "../../components/common/IconButton.vue";
import Checkbox from "../../components/common/Checkbox.vue";
//#endregion

//#region export
export default {
  //#region components
  components: {
    Button,
    IconButton,
    Checkbox,
  },
  //#endregion

  //#region props
  props: {
    employee: {
      type: Object,
      default: null,
    },
  },
  //#endregion

  //#region methods
  methods: {
    /**
     * Hàm đóng mở dropdown
     * CreatedBy: dbhuan 15/05/2021
     */
    toggleDropdown(event, empl) {
      let left = event.clientX;
      let top = event.clientY;
      this.$emit("toggleTableOption", { left, top, employee: empl });
    },

    /**
     * Hàm click button sửa
     * CreatedBy: dbhuan 16/05/2021
     */
    onClickBtnEdit(employeeId) {
      this.$emit("onClickBtnEdit", employeeId);
    },
  },
  //#endregion
};
//#endregion
</script>