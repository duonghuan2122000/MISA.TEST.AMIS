using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
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
    public class EmployeeDepartmentController : BaseController<EmployeeDepartment>
    {
        #region CONSTRUCTOR
        public EmployeeDepartmentController(IEmployeeDepartmentService employeeDepartmentService): base(employeeDepartmentService)
        {
            
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Lấy thông tin thực thể t
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <returns>
        /// 200 - Có dữ liệu
        /// 204 - Không có dữ liệu
        /// 400 - Lỗi client
        /// 500 - Lỗi server
        /// </returns>
        [NonAction]
        public override IActionResult Get(Guid id)
        {
            return base.Get(id);
        }

        /// <summary>
        /// Insert một thực thể t
        /// </summary>
        /// <param name="t">Thông tin thực thể t</param>
        /// <returns>
        /// 200 - Có dữ liệu
        /// 204 - Không có dữ liệu
        /// 400 - Lỗi client
        /// 500 - Lỗi server
        /// </returns>
        [NonAction]
        public override IActionResult Insert(EmployeeDepartment t)
        {
            return base.Insert(t);
        }

        /// <summary>
        /// Update một thực thể t
        /// </summary>
        /// <param name="t">Thông tin thực thể t</param>
        /// <returns>
        /// 200 - Có dữ liệu
        /// 204 - Không có dữ liệu
        /// 400 - Lỗi client
        /// 500 - Lỗi server
        /// </returns>
        [NonAction]
        public override IActionResult Update(EmployeeDepartment t)
        {
            return base.Update(t);
        }

        /// <summary>
        /// Delete một thực thể t
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <returns>
        /// 200 - Có dữ liệu
        /// 204 - Không có dữ liệu
        /// 400 - Lỗi client
        /// 500 - Lỗi server
        /// </returns>
        [NonAction]
        public override IActionResult Delete(Guid id)
        {
            return base.Delete(id);
        }
        #endregion
    }
#endregion
}
