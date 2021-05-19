using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Services
{
    /// <summary>
    /// Dịch vụ đơn vị nhân viên
    /// </summary>
    #region EmployeeDepartmentService
    public class EmployeeDepartmentService: BaseService<EmployeeDepartment>, IEmployeeDepartmentService
    {
        #region CONSTRUCTOR
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="employeeDepartmentRepository">kho chứa đơn vị nhân viên</param>
        public EmployeeDepartmentService(IEmployeeDepartmentRepository employeeDepartmentRepository) : base(employeeDepartmentRepository)
        {

        }
        #endregion
    }
    #endregion
}
