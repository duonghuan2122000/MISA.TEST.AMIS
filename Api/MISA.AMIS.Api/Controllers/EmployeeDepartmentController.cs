using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.Api.Controllers
{
    #region EmployeeDepartmentController
    [Route("api/v1/employee-department")]
    [ApiController]
    public class EmployeeDepartmentController : ControllerBase
    {
        #region PROPERTIES
        private IEmployeeDepartmentService _employeeDepartmentService;
        #endregion

        #region CONSTRUCTOR
        public EmployeeDepartmentController(IEmployeeDepartmentService employeeDepartmentService)
        {
            _employeeDepartmentService = employeeDepartmentService;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Lấy danh sách đơn vị nhân viên.
        /// </summary>
        /// <returns>
        /// 200 - Có dữ liệu trả về.
        /// 500 - Lỗi server.
        /// </returns>
        [HttpGet]
        public IActionResult GetEmployeeDepartments()
        {
            var res = _employeeDepartmentService.GetAll();
            return Ok(res);
        }
        #endregion
    }
    #endregion
}
