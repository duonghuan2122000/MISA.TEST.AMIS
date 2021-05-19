using Microsoft.AspNetCore.Http;
using MISA.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.AMIS.Api.Middleware
{
    /// <summary>
    /// ErrorHandleMiddleware
    /// </summary>
    /// CreatedBy: dbhuan (09/05/2021)
    #region ErrorHandleMiddleware
    public class ErrorHandleMiddleware
    {
        #region PROPERTIES
        /// <summary>
        /// Luồng request.
        /// </summary>
        private RequestDelegate _next;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Hàm call của middleware
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ErrorHandle(context, ex);
            }
        }

        /// <summary>
        /// Xử lý lỗi.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private Task ErrorHandle(HttpContext context, Exception ex)
        {
            var data = ex.Data;
            int statusCode = 500;
            if(ex is ClientException)
            {
                var clientEx = ex as ClientException;
                statusCode = 400;
                data = clientEx.DataErr;
            }

            var res = new
            {
                devMsg = ex.Message,
                userMsg = "Có lỗi xảy ra",
                Data = data
            };

            var result = JsonSerializer.Serialize(res);
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
        #endregion
    }
    #endregion
}
