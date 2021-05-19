using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.Api.Controllers
{
    /// <summary>
    /// BaseController
    /// </summary>
    /// CreatedBy: dbhuan 19/05/2021
    #region BaseController
    [ApiController]
    public class BaseController<T> : ControllerBase where T: class
    {
        #region PROPERTIES
        /// <summary>
        /// base service
        /// </summary>
        private IBaseService<T> _baseService;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Phương thức khởi tạo
        /// </summary>
        /// <param name="baseService"></param>
        public BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Lấy tất cả thực thể T
        /// </summary>
        /// <returns>
        /// 200 - Có dữ liệu
        /// 204 - Không có dữ liệu
        /// 400 - Lỗi client
        /// 500 - Lỗi server
        /// </returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _baseService.GetAll();
            if (res.Any())
            {
                return Ok(res);
            }
            return NoContent();
        }

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
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var res = _baseService.Get(id);

            if(res == null)
            {
                return NoContent();
            }
            return Ok(res);
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
        [HttpPost]
        public IActionResult Insert(T t)
        {
            var rowsAffect = _baseService.Insert(t);
            if(rowsAffect > 0)
            {
                return StatusCode(201, rowsAffect);
            }
            return NoContent();
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
        [HttpPut]
        public IActionResult Update(T t)
        {
            var rowsAffect = _baseService.Update(t);
            if(rowsAffect > 0)
            {
                return Ok(rowsAffect);
            }
            return NoContent();
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
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var rowsAffect = _baseService.Delete(id);
            if(rowsAffect > 0)
            {
                return Ok(rowsAffect);
            }
            return NoContent();
        }
        #endregion
    }
    #endregion
}
