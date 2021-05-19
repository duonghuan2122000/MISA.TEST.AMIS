using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.Api.Controllers
{
    #region EmployeeController
    [Route("api/v1/employees")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        #region PROPERTIES
        /// <summary>
        /// Dịch vụ nhân viên
        /// </summary>
        private IEmployeeService _employeeService;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="employeeService">dịch vụ nhân viên</param>
        public EmployeeController(IEmployeeService employeeService): base(employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Lấy danh sách nhân viên có lọc.
        /// </summary>
        /// <param name="employeeFilter">Bộ lọc nhân viên</param>
        /// <returns>
        /// 200 - Có dữ liệu trả về
        /// 204 - Không có dữ liệu
        /// 400 - Lỗi client
        /// 500 - Lỗi server
        /// </returns>
        /// <response code="200">Có dữ liệu trả về</response>
        /// CreatedBy: dbhuan (09/05/2021)
        [HttpGet("Filter")]
        public IActionResult GetEmployees([FromQuery] EmployeeFilter employeeFilter)
        {
            var res = _employeeService.GetEmployees(employeeFilter);
            if (res.Data.Any() && res.TotalRecord != null)
            {
                return Ok(res);
            }
            return NoContent();
        }

        /// <summary>
        /// Lấy mã nhân viên mới nhất
        /// </summary>
        /// <returns>
        /// 200 - Có dữ liệu trả về.
        /// 500 - Lỗi server.
        /// </returns>
        /// CreatedBy: dbhuan (10/05/2021)
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            return Ok(_employeeService.GetNewEmployeeCode());
        }

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <returns>
        /// 200 - thành công.
        /// 500 - lỗi server.
        /// </returns>
        [HttpGet("Export")]
        public IActionResult Export()
        {
            var stream = _employeeService.ExportExcel();
            string excelName = $"Danh-sach-nhan-vien-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        #endregion
    }
    #endregion
}
