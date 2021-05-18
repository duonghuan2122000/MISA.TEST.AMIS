<template>
  <div class="content">
    <div class="title-box">
      <div class="title">Nhân viên</div>
      <div class="toolbar">
        <Button
          color="primary"
          text="Thêm mới nhân viên"
          @click="onClickBtnAddEmployee"
        />
      </div>
    </div>

    <div class="toolbar-box">
      <div class="con-input">
        <Input
          placeholder="Tìm theo mã, tên hoặc số điện thoại"
          :hasIcon="true"
          :value="employeeFilter"
          @input="onInputFilterEmployee"
        />
        <div class="icon-input icon icon-search"></div>
      </div>
      <IconButton
        icon="icon icon-refresh"
        style="margin-left: 8px; padding: 0"
        @click="onClickBtnRefresh"
      />
      <IconButton
        icon="icon icon-excel"
        style="margin-left: 8px; padding: 0"
        @click="onClickBtnExport"
      />
    </div>

    <div class="data" style="position: relative">
      <table class="table" v-show="!isError">
        <thead>
          <tr style="position: sticky; top: 112px">
            <th class="view-white pin" style="left: 0"></th>
            <th class="pin" style="left: 16px; background-color: #eceef1">
              <Checkbox />
            </th>
            <th style="min-width: 150px; text-align: center">MÃ NHÂN VIÊN</th>
            <th style="min-width: 250px; text-align: center">TÊN NHÂN VIÊN</th>
            <th style="min-width: 150px; text-align: center">GIỚI TÍNH</th>
            <th style="min-width: 150px; text-align: center">NGÀY SINH</th>
            <th style="min-width: 150px; text-align: center">SỐ CMND</th>
            <th style="min-width: 150px; text-align: center">CHỨC DANH</th>
            <th style="min-width: 150px; text-align: center">TÊN ĐƠN VỊ</th>
            <th style="min-width: 150px; text-align: center">SỐ TÀI KHOẢN</th>
            <th style="min-width: 150px; text-align: center">TÊN NGÂN HÀNG</th>
            <th style="min-width: 300px; text-align: center">
              CHI NHÁNH TK NGÂN HÀNG
            </th>
            <th
              style="
                min-width: 150px;
                text-align: center;
                right: 16px;
                background-color: #eceef1;
                border-left: 1px solid #ccc;
                border-right: none;
              "
              class="pin"
            >
              CHỨC NĂNG
            </th>
            <th class="view-white pin" style="right: 0"></th>
          </tr>
        </thead>
        <tbody v-if="employees && employees.length > 0">
          <EmployeeItem
            v-for="e in employees"
            :key="e.employeeId"
            :employee="e"
            @toggleTableOption="toggleTableOption"
            @onClickBtnEdit="onClickBtnEdit"
          />
        </tbody>
      </table>

      <div class="flex-center" v-if="isLoading">
        <div class="loader"></div>
      </div>

      <div class="flex-center" v-if="isError">
        <div class="icon icon-error"></div>
        <div>Có lỗi xảy ra.</div>
      </div>

      <div
        v-if="!isLoading && !isError && (!employees || employees.length < 1)"
        style="display: flex; flex-direction: column; justify-content: center"
      >
        <img
          src="../../assets/bg_report_nodata.svg"
          alt="Không có dữ liệu"
          style="margin: 20px auto"
        />
        <div style="text-align: center">Không có dữ liệu</div>
      </div>
    </div>

    <div class="divider"></div>

    <Pagination
      :page="page"
      :pageSize="pageSize"
      :totalPages="totalPages"
      :totalRecord="totalRecord"
      @onChangePage="onChangePage"
      @update:pageSize="onChangePageSize"
    />

    <div class="hidden-left"></div>
    <div class="hidden-right"></div>

    <EmployeeDialog
      v-if="employeeDialogConfig.isShow"
      ref="employeeDialogRef"
      :employee.sync="employeeDialogConfig.employee"
      :errors.sync="employeeDialogConfig.errors"
      @onClose="onClickBtnCloseEmployeeDialog"
      @onSave="onSaveEmployee"
      @onSaveAndAdd="onSaveAndAddEmployee"
    />

    <EmployeeTableOption
      v-if="employeeTableOptionConfig.isShow"
      v-bind="{ ...employeeTableOptionConfig }"
      @onClose="closeEmployeeTableOption"
      @onClickBtnDel="onClickBtnDel"
      @onClickDuplicate="onClickDuplicate"
    />

    <ConfirmDialog
      v-if="confirmDialogConfig.isShow"
      :msg="confirmDialogConfig.msg"
      @onClose="
        (employeeModify = null),
          (confirmDialogConfig.isShow = false),
          (confirmDialogConfig.msg = '')
      "
      @onPositive="delEmployee"
    />

    <AlertDialog
      v-if="alertDialogConfig.isShow"
      :msg="alertDialogConfig.msg"
      :type="alertDialogConfig.type"
      @onClose="
        (alertDialogConfig.isShow = false), (alertDialogConfig.msg = '')
      "
    />

    <InfoDialog
      v-if="infoDialogConfig.isShow"
      @onClose="infoDialogConfig.isShow = false"
      @onNegative="onNegativeInfoDialog"
      @onPositive="onPositiveInfoDialog"
    />
  </div>
</template>

<script>
//#region import
import {
  getEmployees,
  getNewEmployeeCode,
  getEmployee,
  exportExcel,
  delEmployee,
  saveEmployee,
} from "../../api/employee.js";

import { StateEnum } from "../../enums/enum.js";

import Button from "../../components/common/Button.vue";
import IconButton from "../../components/common/IconButton.vue";
import Input from "../../components/common/Input.vue";
import Checkbox from "../../components/common/Checkbox.vue";
import ConfirmDialog from "../../components/common/ConfirmDialog.vue";
import Pagination from "../../components/common/Pagination.vue";
import AlertDialog from "../../components/common/AlertDialog.vue";

import EmployeeItem from "./EmployeeItem.vue";
import EmployeeDialog from "./EmployeeDialog.vue";
import EmployeeTableOption from "./EmployeeTableOption.vue";
import InfoDialog from "./InfoDialog.vue";

//#endregion

//#region export
export default {
  //#region components
  components: {
    Button,
    IconButton,
    Input,
    Checkbox,
    ConfirmDialog,
    Pagination,
    AlertDialog,

    EmployeeItem,
    EmployeeDialog,
    EmployeeTableOption,
    InfoDialog,
  },
  //#endregion

  //#region data
  data() {
    return {
      /**
       * trạng thái trang web.
       * CreatedBy: dbhuan 16/05/2021
       */
      state: StateEnum.LOADING,

      /**
       * Trang hiện tại
       * CreatedBy: dbhuan 15/05/2021
       */
      page: 1,

      /**
       * Số bản ghi trên một trang
       * CreatedBy: dbhuan 15/05/2021
       */
      pageSize: 20,

      /**
       * Từ khóa lọc danh sách nhân viên
       * CreatedBy: dbhuan 15/05/2021
       */
      employeeFilter: "",

      /**
       * Tổng số bản ghi
       * CreatedBy: dbhuan 15/05/2021
       */
      totalRecord: 0,

      /**
       * Tổng số trang
       * CreatedBy: dbhuan 15/05/2021
       */
      totalPages: 0,

      /**
       * Danh sách nhân viên
       * CreatedBy: dbhuan 15/05/2021
       */
      employees: [],

      /**
       * Config của dropdown chức năng table
       * CreatedBy: dbhuan 16/05/2021
       */
      employeeTableOptionConfig: {
        isShow: false,
        top: 0,
        left: 0,
      },

      /**
       * Thông tin nhân viên đang được chọn để thao tác
       * CreatedBy: dbhuan 15/05/2021
       */
      employeeModify: null,

      /**
       * Config của dialog nhân viên
       * CreatedBy: dbhuan 16/05/2021
       */
      employeeDialogConfig: {
        isShow: false,
        employee: null,
        employeeOrigin: null,
        isInsert: true,
        errors: null,
      },

      /**
       * Config của dialog xác nhận
       * CreatedBy: dbhuan 16/05/2021
       */
      confirmDialogConfig: {
        isShow: false,
        msg: "",
      },

      /**
       * Config của dialog alert.
       * CreatedBy: dbhuan 18/05/2021
       */
      alertDialogConfig: {
        isShow: false,
        msg: "",
        type: "warning",
      },

      /**
       * Config của dialog info
       * CreatedBy: dbhuan 18/05/2021
       */
      infoDialogConfig: {
        isShow: false,
      },

      timeOut: null,
    };
  },
  //#endregion

  //#region computed
  computed: {
    /**
     * Đang loading
     * CreatedBy: dbhuan 16/05/2021
     */
    isLoading: function () {
      return this.state == StateEnum.LOADING;
    },

    /**
     * Có lỗi
     * CreatedBy: dbhuan 16/05/2021
     */
    isError: function () {
      return this.state == StateEnum.ERROR;
    },
  },
  //#endregion

  //#region methods
  methods: {
    /**
     * Lấy danh sách nhân viên.
     * CreatedBy: dbhuan 15/05/2021
     */
    getEmployees() {
      this.state = StateEnum.LOADING;
      getEmployees({
        page: this.page,
        pageSize: this.pageSize,
        filter: this.employeeFilter,
      })
        .then((data) => {
          this.totalRecord = data.totalRecord;
          this.totalPages = data.totalPages;
          this.employees = data.data;
          this.state = StateEnum.SUCCESS;
        })
        .catch(() => {
          this.state = StateEnum.ERROR;
        });
    },

    /**
     * Hàm click vào icon down.
     * Gán employee đang được chọn và hiển thị dropdown ở phần chức năng
     * @param {object} data Gồm các thông tin về left, top và thông tin nhân viên
     * CreatedBy: dbhuan 15/055/2021
     */
    toggleTableOption(data) {
      let newLeft = data.left - 100;
      let newTop = data.top + 16;
      let employee = data.employee;

      let windowHeight = window.innerHeight;

      if (newTop > windowHeight / 2) {
        newTop -= 132;
      }

      this.employeeModify = employee;
      this.employeeTableOptionConfig = {
        isShow: true,
        top: newTop,
        left: newLeft,
      };
    },

    /**
     * Hàm đóng dropdown chức năng nhân viên
     * CreatedBy: dbhuan 16/05/2021
     */
    closeEmployeeTableOption() {
      this.employeeModify = null;
      this.employeeTableOptionConfig = {
        isShow: false,
        top: 0,
        left: 0,
      };
    },

    /**
     * hàm click button thêm nhân viên
     * CreatedBy: dbhuan 16/05/2021
     */
    onClickBtnAddEmployee() {
      getNewEmployeeCode().then((data) => {
        this.employeeDialogConfig = {
          isShow: true,
          employee: {
            employeeCode: data,
          },
          isInsert: true,
          errors: null,
        };
        this.employeeDialogConfig.employeeOrigin = {
          ...this.employeeDialogConfig.employee,
        };
      });
    },

    /**
     * Hàm click button sửa nhân viên
     * CreatedBy: dbhuan 16/05/2021
     */
    onClickBtnEdit(employeeId) {
      getEmployee(employeeId).then((data) => {
        this.employeeDialogConfig = {
          isShow: true,
          employee: data,
          isInsert: false,
          errors: null,
        };
        this.employeeDialogConfig.employeeOrigin = {
          ...this.employeeDialogConfig.employee,
        };
      });
    },

    /**
     * hàm click button xóa
     * CreatedBy: dbhuan 16/05/2021
     */
    onClickBtnDel() {
      this.employeeTableOptionConfig = {
        isShow: false,
        top: 0,
        left: 0,
      };
      if (this.employeeModify) {
        this.confirmDialogConfig = {
          isShow: true,
          msg: `Bạn có chắc chắn muốn xóa nhân viên <${this.employeeModify.employeeCode}> không?`,
        };
      }
    },

    onClickDuplicate() {
      this.employeeTableOptionConfig = {
        isShow: false,
        top: 0,
        left: 0,
      };
      if (this.employeeModify) {
        getEmployee(this.employeeModify.employeeId)
          .then((employee) => {
            this.employeeDialogConfig.isInsert = true;
            this.employeeDialogConfig.errors = null;
            this.employeeDialogConfig.employee = employee;
            return getNewEmployeeCode();
          })
          .then((employeeCode) => {
            this.employeeDialogConfig.employee = {
              ...this.employeeDialogConfig.employee,
              employeeCode,
            };
            this.employeeDialogConfig.employeeOrigin = {
              ...this.employeeDialogConfig.employee,
            };
            this.employeeDialogConfig.isShow = true;
          });
      }
    },

    /**
     * hàm lọc danh sách nhân viên
     * CreatedBy: dbhuan 16/05/2021
     */
    onInputFilterEmployee(val) {
      this.employeeFilter = val;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        this.page = 1;
        this.getEmployees();
      }, 1000);
    },

    /**
     * hàm click button export refresh
     * CreatedBy: dbhuan 16/05/2021
     */
    onClickBtnRefresh() {
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        this.page = 1;
        this.employeeFilter = "";
        this.getEmployees();
      }, 1000);
    },

    /**
     * hàm click button export excel
     * CreatedBy: dbhuan 16/05/2021
     */
    onClickBtnExport() {
      exportExcel({
        page: this.page,
        pageSize: this.pageSize,
        filter: this.employeeFilter,
      });
    },

    /**
     * hàm gọi khi page thay đổi
     * CreatedBy: dbhuan 16/05/2021
     */
    onChangePage(page) {
      this.page = page;
      this.getEmployees();
    },

    onChangePageSize(pageSize) {
      this.page = 1;
      this.pageSize = pageSize;
      this.getEmployees();
    },

    /**
     * Xóa nhân viên
     * CreatedBy: dbhuan 16/05/2021
     */
    delEmployee() {
      this.confirmDialogConfig = {
        isShow: false,
        msg: "",
      };
      if (this.employeeModify) {
        delEmployee(this.employeeModify.employeeId).then(() => {
          this.page = 1;
          this.getEmployees();
        });
      }
    },

    saveEmployee() {
      if (this.employeeDialogConfig.errors) {
        for (let err in this.employeeDialogConfig.errors) {
          if (this.employeeDialogConfig.errors[err]) {
            this.alertDialogConfig = {
              isShow: true,
              type: "error",
              msg: this.employeeDialogConfig.errors[err],
            };
            console.log(this.employeeDialogConfig.errors[err]);
            return Promise.reject();
          }
        }
      }
      return saveEmployee(
        this.employeeDialogConfig.employee,
        this.employeeDialogConfig.isInsert
      )
        .then(() => {
          this.getEmployees();
          return Promise.resolve();
        })
        .catch((err) => {
          if (err.response.data.Data && err.response.data.Data.EmployeeCode) {
            var error = err.response.data.devMsg;
            this.employeeDialogConfig.errors = {
              employeeCode: error,
            };
            this.alertDialogConfig = {
              isShow: true,
              type: "warning",
              msg: error,
            };
            return Promise.reject();
          }
        });
    },

    /**
     * click button cất
     * CreatedBy: dbhuan 18/05/2021
     */
    onSaveEmployee() {
      this.saveEmployee().then(() => {
        this.closeEmployeeDialog();
      });
    },

    /**
     * click button cất và thêm
     * CreatedBy: dbhuan 18/05/2021
     */
    onSaveAndAddEmployee() {
      this.saveEmployee().then(() => {
        console.log("click");
        this.onClickBtnAddEmployee();
      });
    },

    /**
     * Hàm đóng dialog nhân viên
     * CreatedBy: dbhuan 18/05/2021
     */
    closeEmployeeDialog() {
      this.employeeDialogConfig = {
        isShow: false,
        employee: null,
        employeeOrigin: null,
        errors: null,
        isInsert: true,
      };
    },

    /**
     * click button đóng dialog nhân viên
     * CreatedBy: dbhuan 18/05/2021
     */
    onClickBtnCloseEmployeeDialog() {
      for (let key in this.employeeDialogConfig.employee) {
        if (
          this.employeeDialogConfig.employeeOrigin[key] !=
          this.employeeDialogConfig.employee[key]
        ) {
          this.infoDialogConfig.isShow = true;
          return;
        }
      }
      this.closeEmployeeDialog();
    },

    /**
     * click button Không trong info dialog.
     * CreatedBy: dbhuan 18/05/2021
     */
    onNegativeInfoDialog() {
      this.infoDialogConfig.isShow = false;
      this.closeEmployeeDialog();
    },

    /**
     * click button Có trong info dialog
     * CreatedBy: dbhuan 18/05/2021
     */
    onPositiveInfoDialog() {
      this.infoDialogConfig.isShow = false;
      this.$refs.employeeDialogRef.validateBeforeSave();
      this.saveEmployee();
    },
  },
  //#endregion

  //#region mounted
  mounted() {
    this.getEmployees();
  },
  //#endregion
};
//#endregion
</script>
