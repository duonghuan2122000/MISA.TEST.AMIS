<template>
  <div>
    <div class="dialog">
      <div class="dialog-background" @click.prevent="closeDialog"></div>
      <div class="dialog-content zoomIn">
        <div class="tooltip-box tooltip">
          <div class="btn-close-dialog" @click.prevent="closeDialog">
            <div class="icon icon-close"></div>
          </div>
          <div class="tooltip-text">Đóng (ESC)</div>
        </div>

        <div class="dialog-header">
          <div class="dialog-title">Thông tin nhân viên</div>
          <div class="option">
            <input type="checkbox" class="checkbox" id="is-customer" />
            <label for="is-customer"></label>
            <label style="margin-left: 8px">Là khách hàng</label>
          </div>
          <div class="option">
            <input type="checkbox" class="checkbox" id="is-provider" />
            <label for="is-provider"></label>
            <label style="margin-left: 8px">Là nhà cung cấp</label>
          </div>
        </div>

        <div class="dialog-body">
          <div class="row">
            <div class="col-6" style="padding-right: 16px">
              <div class="row">
                <div class="col-5" style="padding-right: 8px">
                  <div class="con-input">
                    <label class="label-input"
                      >Mã <span style="color: #f20">*</span></label
                    >
                    <Input
                      ref="employeeCode"
                      :value="employee && employee.employeeCode"
                      :class="{ 'has-error': errors && errors.employeeCode }"
                      @input="
                        $emit('update:employee', {
                          ...employee,
                          employeeCode: $event,
                        })
                      "
                      @blur="validate({ employeeCode: 'required' })"
                    />
                    <span
                      v-if="errors && errors.employeeCode"
                      class="text-error"
                      >{{ errors && errors.employeeCode }}</span
                    >
                  </div>
                </div>
                <div class="col-7">
                  <div class="con-input">
                    <label class="label-input"
                      >Tên <span style="color: #f20">*</span></label
                    >
                    <Input
                      :class="{ 'has-error': errors && errors.employeeName }"
                      :value="employee && employee.employeeName"
                      @input="
                        $emit('update:employee', {
                          ...employee,
                          employeeName: $event,
                        })
                      "
                      @blur="validate({ employeeName: 'required' })"
                    />
                    <span
                      v-if="errors && errors.employeeName"
                      class="text-error"
                      >{{ errors && errors.employeeName }}</span
                    >
                  </div>
                </div>
              </div>
            </div>

            <div class="col-6" style="padding-left: 16px">
              <div class="row">
                <div class="col-5" style="padding-right: 16px">
                  <div>
                    <label class="label-input">Ngày sinh</label>
                    <DatePicker
                      ref="dateOfBirth"
                      displayFormat="DD/MM/YYYY"
                      :inputAttributes="{
                        class: 'input',
                        style: 'font-size: 13px',
                        placeholder: 'DD/MM/YYYY',
                      }"
                      :weekdays="localeDatePicker.weekdays"
                      :months="localeDatePicker.months"
                      :isDateDisabled="isDateDisabled"
                      :value="
                        employee && employee.dateOfBirth
                          ? formatYYYMMDD(employee.dateOfBirth)
                          : null
                      "
                      @input="
                        $emit('update:employee', {
                          ...employee,
                          dateOfBirth: $event,
                        })
                      "
                    />
                  </div>
                </div>
                <div class="col-7">
                  <div>
                    <label class="label-input">Giới tính</label>
                    <div class="flex-row-align-center" style="height: 32px">
                      <div
                        class="flex-row-align-center"
                        style="margin-right: 16px"
                      >
                        <Radio
                          name="gender"
                          value="0"
                          :checked="employee && employee.gender == 0"
                          @change="
                            $emit('update:employee', {
                              ...employee,
                              gender: parseInt($event),
                            })
                          "
                        />
                        <label style="margin-left: 8px">Nam</label>
                      </div>
                      <div
                        class="flex-row-align-center"
                        style="margin-right: 16px"
                      >
                        <Radio
                          name="gender"
                          value="1"
                          :checked="employee && employee.gender == 1"
                          @change="
                            $emit('update:employee', {
                              ...employee,
                              gender: parseInt($event),
                            })
                          "
                        />
                        <label style="margin-left: 8px">Nữ</label>
                      </div>
                      <div
                        class="flex-row-align-center"
                        style="margin-right: 16px"
                      >
                        <Radio
                          name="gender"
                          value="2"
                          :checked="employee && employee.gender == 2"
                          @change="
                            $emit('update:employee', {
                              ...employee,
                              gender: parseInt($event),
                            })
                          "
                        />
                        <label style="margin-left: 8px">Khác</label>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div class="col-6" style="padding-right: 16px">
              <div class="con-input">
                <label class="label-input"
                  >Đơn vị <span style="color: #f20">*</span></label
                >
                <Autocomplete
                  :value="employee && employee.employeeDepartmentId"
                  :inputAttributes="{
                    class: {
                      'has-error': errors && errors.employeeDepartmentId,
                    },
                  }"
                  :suggestions="departments"
                  @update:value="
                    $emit('update:employee', {
                      ...employee,
                      employeeDepartmentId: $event,
                    })
                  "
                  @blur="validate({ employeeDepartmentId: 'required' })"
                />
                <span
                  v-if="errors && errors.employeeDepartmentId"
                  class="text-error"
                  >{{ errors && errors.employeeDepartmentId }}</span
                >
              </div>
            </div>

            <div class="col-6" style="padding-left: 16px">
              <div class="row">
                <div class="col-7" style="padding-right: 8px">
                  <div>
                    <label class="label-input">Số CMND</label>
                    <Input
                      :value="employee && employee.identityNumber"
                      @input="
                        $emit('update:employee', {
                          ...employee,
                          identityNumber: $event,
                        })
                      "
                    />
                  </div>
                </div>

                <div class="col-5">
                  <div>
                    <label class="label-input">Ngày cấp</label>
                    <DatePicker
                      displayFormat="DD/MM/YYYY"
                      :inputAttributes="{
                        class: 'input',
                        style: 'font-size: 13px',
                        placeholder: 'DD/MM/YYYY',
                      }"
                      :isDateDisabled="isDateDisabled"
                      :weekdays="localeDatePicker.weekdays"
                      :months="localeDatePicker.months"
                      :value="
                        employee && employee.identityDate
                          ? formatYYYMMDD(employee.identityDate)
                          : null
                      "
                      @input="
                        $emit('update:employee', {
                          ...employee,
                          identityDate: $event,
                        })
                      "
                    />
                  </div>
                </div>
              </div>
            </div>

            <div class="col-6" style="padding-right: 16px">
              <div>
                <div>
                  <label class="label-input">Chức danh</label>
                  <Input
                    :value="employee && employee.employeePosition"
                    @input="
                      $emit('update:employee', {
                        ...employee,
                        employeePosition: $event,
                      })
                    "
                  />
                </div>
              </div>
            </div>

            <div class="col-6" style="padding-left: 16px">
              <div>
                <label class="label-input">Nơi cấp</label>
                <Input
                  :value="employee && employee.identityPlace"
                  @input="
                    $emit('update:employee', {
                      ...employee,
                      identityPlace: $event,
                    })
                  "
                />
              </div>
            </div>

            <div class="col-12" style="margin-top: 30px">
              <div>
                <label class="label-input">Địa chỉ</label>
                <Input
                  :value="employee && employee.employeeAddress"
                  @input="
                    $emit('update:employee', {
                      ...employee,
                      employeeAddress: $event,
                    })
                  "
                />
              </div>
            </div>
            <div class="col-3" style="padding-right: 8px">
              <div>
                <label class="label-input">Điện thoại di động</label>
                <Input
                  :value="employee && employee.phoneNumber"
                  @input="
                    $emit('update:employee', {
                      ...employee,
                      phoneNumber: $event,
                    })
                  "
                />
              </div>
            </div>
            <div class="col-3" style="padding-right: 8px">
              <div>
                <label class="label-input">Điện thoại cố định</label>
                <Input
                  :value="employee && employee.teleNumber"
                  @input="
                    $emit('update:employee', {
                      ...employee,
                      teleNumber: $event,
                    })
                  "
                />
              </div>
            </div>
            <div class="col-3" style="padding-right: 8px">
              <div>
                <label class="label-input">Email</label>
                <Input
                  :value="employee && employee.email"
                  @input="
                    $emit('update:employee', {
                      ...employee,
                      email: $event,
                    })
                  "
                />
              </div>
            </div>
            <div class="col-3" style="padding-right: 8px"></div>
            <div class="col-3" style="padding-right: 8px">
              <div>
                <label class="label-input">Tài khoản ngân hàng</label>
                <Input
                  :value="employee && employee.bankAccountNumber"
                  @input="
                    $emit('update:employee', {
                      ...employee,
                      bankAccountNumber: $event,
                    })
                  "
                />
              </div>
            </div>
            <div class="col-3" style="padding-right: 8px">
              <div>
                <label class="label-input">Tên ngân hàng</label>
                <input
                  type="text"
                  class="input"
                  :value="employee && employee.bankName"
                  @input="
                    $emit('update:employee', {
                      ...employee,
                      bankName: $event,
                    })
                  "
                />
              </div>
            </div>
            <div class="col-3" style="padding-right: 8px">
              <div>
                <label class="label-input">Chi nhánh</label>
                <Input
                  :value="employee && employee.bankBranchName"
                  @input="
                    $emit('update:employee', {
                      ...employee,
                      bankBranchName: $event,
                    })
                  "
                />
              </div>
            </div>
            <div class="col-3" style="padding-right: 8px"></div>
          </div>
        </div>

        <div class="dialog-footer">
          <div class="dialog-footer-left">
            <Button color="secondary" text="Hủy" @click="closeDialog" />
          </div>
          <div class="dialog-footer-right flex-row-align-center">
            <div class="tooltip">
              <Button color="secondary" text="Cất" @click="onClickSave" />
              <div class="tooltip-text">Cất (Ctrl + S)</div>
            </div>
            <div class="tooltip">
              <Button
                color="primary"
                text="Cất và thêm"
                style="margin-left: 8px"
                @click="onClickSaveAndAdd"
              />
              <div class="tooltip-text">Cất và thêm (Ctrl + Shift + S)</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
//#region import
import dayjs from "dayjs";

import { getEmployeeDepartments } from "../../api/employeeDepartment.js";

import validation from "../../helpers/validation.js";

import DatePicker from "vue-date-pick";
import "vue-date-pick/dist/vueDatePick.css";

import Button from "../../components/common/Button.vue";
import Input from "../../components/common/Input.vue";
import Radio from "../../components/common/Radio.vue";
import Autocomplete from "../../components/common/Autocomplete.vue";
//#endregion

//#region export
export default {
  //#region components
  components: {
    DatePicker,
    Button,
    Input,
    Radio,
    Autocomplete,
  },
  //#endregion

  //#region props
  props: {
    /**
     * Prop thông tin nhân viên.
     * CreatedBy: dbhuan (10/05/2021)
     */
    employee: {
      type: Object,
      default: null,
    },

    /**
     * Lỗi valid
     * CreatedBy: dbhuan 18/05/2021
     */
    errors: {
      type: Object,
      default: null,
    },
  },
  //#endregion

  //#region data
  data: () => ({
    /**
     * Tên hiển thị lỗi của input.
     * CreatedBy: dbhuan 18/05/2021
     */
    displayNames: {
      employeeCode: "Mã nhân viên",
      employeeName: "Tên nhân viên",
      employeeDepartmentId: "Đơn vị nhân viên",
    },

    /**
     * Xác định trang thái dialog alert.
     * CreatedBy: dbhuan 18/05/2021
     */
    isShowAlertDialog: false,
    typeAlertDialog: "warning",
    msgAlertDialog: "",
    departments: [],

    /**
     * Locale datepicker
     */
    localeDatePicker: {
      weekdays: ["T2", "T3", "T4", "T5", "T6", "T7", "CN"],
      months: [
        "Tháng 1",
        "Tháng 2",
        "Tháng 3",
        "Tháng 4",
        "Tháng 5",
        "Tháng 6",
        "Tháng 7",
        "Tháng 8",
        "Tháng 9",
        "Tháng 10",
        "Tháng 11",
        "Tháng 12",
      ],
    },
  }),
  //#endregion

  //#region methods
  methods: {
    /**
     * Phương thức click lưu thông tin nhân viên.
     */
    onClickSave() {
      this.validateBeforeSave();
      this.$emit("onSave");
    },

    /**
     * click button cất và thêm
     * CreatedBy: dbhuan 16/05/2021
     */
    onClickSaveAndAdd() {
      this.validateBeforeSave();
      this.$emit("onSaveAndAdd");
    },

    /**
     * Phương thức đóng dialog
     * CreatedBy: dbhuan (10/05/2021)
     */
    closeDialog() {
      this.$emit("onClose");
    },

    /**
     * Hàm format date về dạng YYYY-MM-DD
     * CreatedBy: dbhuan (10/05/2021)
     */
    formatYYYMMDD(dateStr) {
      return dateStr ? dayjs(dateStr).format("YYYY-MM-DD") : null;
    },

    /**
     * Valid dữ liệu
     * CreatedBy: dbhuan 16/05/2021
     */
    validate(rules) {
      this.$emit("update:errors", {
        ...this.errors,
        ...validation.validate(rules, this.employee, this.displayNames),
      });
    },

    validateBeforeSave() {
      this.$emit("update:errors", {
        ...validation.validate(
          {
            employeeCode: "required",
            employeeName: "required",
            employeeDepartmentId: "required",
          },
          this.employee,
          this.displayNames
        ),
      });
    },

    /**
     * sự kiện nhấn phím
     * CreatedBy: dbhuan 16/05/2021
     */
    onKeyDownListener(e) {
      if (e.keyCode == 27) {
        // ESC
        this.closeDialog();
        e.preventDefault();
      }

      if (e.key == "s" && (e.ctrlKey || e.metaKey)) {
        // Ctrl + s
        this.onClickSave();
        e.preventDefault();
      }

      if (e.key == "S" && (e.ctrlKey || e.metaKey)) {
        // Ctrl + Shift + s
        this.onClickSaveAndAdd();
        e.preventDefault();
      }
    },

    isDateDisabled(date) {
      return date > new Date();
    },
  },
  //#endregion

  //#region lifecycle
  mounted() {
    getEmployeeDepartments().then((data) => {
      this.departments = data.map((d) => ({
        value: d.employeeDepartmentId,
        text: d.employeeDepartmentName,
      }));
    });
    this.$refs.employeeCode.$el.focus();
    document.addEventListener("keydown", this.onKeyDownListener);
  },
  beforeDestroy() {
    document.removeEventListener("keydown", this.onKeyDownListener);
  },
  //#endregion
};
//#endregion
</script>