import req from '../utils/req.js';

import { BASE_URL } from '../utils/envConst.js';

const PREFIX_URL = "api/v1/employees";

//#region getEmployees
/**
 * Lấy danh sách nhân viên có lọc
 * @param {object} param 
 * @returns 
 * CreatedBy: dbhuan 15/05/2021
 */
export const getEmployees = ({ page = 1, pageSize = 10, filter = "" }) =>
    req.get(`${PREFIX_URL}?page=${page}&pageSize=${pageSize}&filter=${filter}`)
        .then(res => res.data)
        .then(data => {
            return Promise.resolve(data);
        })
        .catch(err => Promise.reject(err));
//#endregion

//#region getNewEmployeeCode
/**
 * Lấy mã nhân viên mới
 * @returns string
 */
export const getNewEmployeeCode = () =>
    req.get(`${PREFIX_URL}/NewEmployeeCode`)
        .then(res => res.data)
        .then(data => Promise.resolve(data))
        .catch(err => Promise.reject(err));
//#endregion

//#region getNewEmployeeCode
/**
 * Lấy thông tin một nhân viên
 * @param {string} employeeId id nhân viên
 * @returns Thông tin nhân viên
 */
export const getEmployee = (employeeId) =>
    req.get(`${PREFIX_URL}/${employeeId}`)
        .then(res => res.data)
        .then(data => Promise.resolve(data))
        .catch(err => Promise.reject(err));
//#endregion

//#region exportExcel
/**
 * Export excel
 * @param {object} param
 */
export const exportExcel = () => {
    window.open(`${BASE_URL}${PREFIX_URL}/Export`, '_blank');
}
//#endregion

//#region delEmployee
/**
 * Xóa nhân viên
 * @param {string} employeeId id nhân viên
 * @returns 
 */
export const delEmployee = (employeeId) =>
    req.delete(`${PREFIX_URL}/${employeeId}`)
        .then(res => {
            if (res.status == 200) {
                return Promise.resolve();
            }
            return Promise.reject();
        })
        .catch(err => Promise.reject(err));
//#endregion

//#region saveEmployee
/**
 * Lưu thông tin nhân viên
 * @returns 
 */
export const saveEmployee = (data, isInsert = true) =>
    req({
        url: `${PREFIX_URL}`,
        data: data,
        method: isInsert ? 'POST' : 'PUT'
    })
        .then(res => {
            if (res.status == 200 || res.status == 201) return Promise.resolve();
            return Promise.reject();
        })
        .catch(err => Promise.reject(err));
//#endregion