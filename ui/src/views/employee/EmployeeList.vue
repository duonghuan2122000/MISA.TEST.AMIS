<template>
  <div>
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
            placeholder="Tìm theo mã, tên nhân viên"
            :hasIcon="true"
            style="border-radius: 0"
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

      <div class="data">
        <div class="flex-center" v-if="isLoading">
          <div class="loader"></div>
        </div>

        <div class="flex-center" v-if="isError">
          <div class="icon icon-error"></div>
          <div>Có lỗi xảy ra.</div>
        </div>

        <table class="table" v-show="!isError">
          <thead>
            <tr>
              <th class="view-white pin" style="left: 0"></th>
              <th class="pin" style="left: 16px; background-color: #eceef1">
                <Checkbox />
              </th>
              <th style="min-width: 150px; text-align: center">MÃ NHÂN VIÊN</th>
              <th style="min-width: 250px; text-align: center">
                TÊN NHÂN VIÊN
              </th>
              <th style="min-width: 150px; text-align: center">GIỚI TÍNH</th>
              <th style="min-width: 150px; text-align: center">NGÀY SINH</th>
              <th style="min-width: 150px; text-align: center">SỐ CMND</th>
              <th style="min-width: 150px; text-align: center">CHỨC DANH</th>
              <th style="min-width: 150px; text-align: center">TÊN ĐƠN VỊ</th>
              <th style="min-width: 150px; text-align: center">SỐ TÀI KHOẢN</th>
              <th style="min-width: 150px; text-align: center">
                TÊN NGÂN HÀNG
              </th>
              <th style="min-width: 300px; text-align: center">
                CHI NHÁNH TK NGÂN HÀNG
              </th>
              <th
                style="
                  min-width: 150px;
                  text-align: center;
                  right: 16px;
                  background-color: #eceef1;
                "
                class="pin"
              >
                CHỨC NĂNG
              </th>
              <th class="view-white pin" style="right: 0"></th>
            </tr>
          </thead>
          <tbody>
            <EmployeeItem
              v-for="e in employees"
              :key="e.employeeId"
              :employee="e"
              @toggleTableOption="toggleTableOption"
              @onClickBtnEdit="onClickBtnEdit"
            />
          </tbody>
        </table>
      </div>

      <div class="divider"></div>

      <Pagination
        :page="page"
        :pageSize="pageSize"
        :totalPages="totalPages"
        :totalRecord="totalRecord"
        @onChangePage="onChangePage"
      />

      <div class="hidden-left"></div>
      <div class="hidden-right"></div>
    </div>

    <EmployeeDialog
      :isShow="employeeDialogConfig.isShow"
      :employee.sync="employeeDialogConfig.employee"
      @onClose="
        (employeeDialogConfig.isShow = false),
          (employeeDialogConfig.employee = null)
      "
    />

    <EmployeeTableOption
      v-bind="{ ...employeeTableOptionConfig }"
      @onClose="closeEmployeeTableOption"
      @onClickBtnDel="onClickBtnDel"
    />

    <ConfirmDialog
      v-bind="{ ...confirmDialogConfig }"
      @onClose="
        (employeeDel = null),
          (confirmDialogConfig.isShow = false),
          (confirmDialogConfig.msg = '')
      "
      @onPositive="delEmployee"
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
} from "../../api/employee.js";

import { StateEnum } from "../../enums/enum.js";

import Button from "../../components/common/Button.vue";
import IconButton from "../../components/common/IconButton.vue";
import Input from "../../components/common/Input.vue";
import Checkbox from "../../components/common/Checkbox.vue";
import ConfirmDialog from "../../components/common/ConfirmDialog.vue";
import Pagination from "../../components/common/Pagination.vue";

import EmployeeItem from "./EmployeeItem.vue";
import EmployeeDialog from "./EmployeeDialog.vue";
import EmployeeTableOption from "./EmployeeTableOption.vue";
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

    EmployeeItem,
    EmployeeDialog,
    EmployeeTableOption,
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
       * Thông tin nhân viên đang được chọn để xóa
       * CreatedBy: dbhuan 15/05/2021
       */
      employeeDel: null,

      /**
       * Config của dialog nhân viên
       * CreatedBy: dbhuan 16/05/2021
       */
      employeeDialogConfig: {
        isShow: false,
        employee: null,
      },

      /**
       * Config của dialog xác nhận
       * CreatedBy: dbhuan 16/05/2021
       */
      confirmDialogConfig: {
        isShow: false,
        msg: "",
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
        .catch((err) => {
          this.state = StateEnum.ERROR;
          console.log(err);
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

      this.employeeDel = employee;
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
      this.employeeDel = null;
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
      if (this.employeeDel) {
        this.confirmDialogConfig = {
          isShow: true,
          msg: `Bạn có chắc chắn muốn xóa nhân viên <${this.employeeDel.employeeCode}> không?`,
        };
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
        this.getEmployees();
      }, 1000);
    },

    /**
     * hàm click button export refresh
     * CreatedBy: dbhuan 16/05/2021
     */
    onClickBtnRefresh() {
      this.page = 1;
      this.employeeFilter = "";
      this.getEmployees();
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

    /**
     * Xóa nhân viên
     * CreatedBy: dbhuan 16/05/2021
     */
    delEmployee() {
      this.confirmDialogConfig = {
        isShow: false,
        msg: "",
      };
      if (this.employeeDel) {
        delEmployee(this.employeeDel.employeeId).then(() => {
          this.page = 1;
          this.getEmployees();
        });
      }
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
