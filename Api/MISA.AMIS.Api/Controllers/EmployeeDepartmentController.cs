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
    }
    #endregion
}
