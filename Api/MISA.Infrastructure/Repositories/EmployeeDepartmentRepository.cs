using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;

namespace MISA.Infrastructure.Repositories
{
    #region EmployeeDepartmentRepository
    public class EmployeeDepartmentRepository : BaseRepository<EmployeeDepartment>, IEmployeeDepartmentRepository
    {
        #region CONSTRUCTOR
        /// <summary>
        /// Hàm khởi tạo.
        /// </summary>
        /// <param name="configuration">Config project</param>
        public EmployeeDepartmentRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

    }
    #endregion
}
