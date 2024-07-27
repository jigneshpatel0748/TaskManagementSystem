using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagementSystem.Dto.Response;

namespace TaskManagementSystem.API.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext exceptionContext)
        {
            try
            {
                var configuration = exceptionContext.HttpContext.RequestServices.GetService<IConfiguration>();
            }
            catch
            {

            }
            exceptionContext.Result = new ObjectResult(new ApiResponse<object>
            {
                IsSuccess = false,
                Message = exceptionContext.Exception.Message
            })
            { StatusCode = 200 };
        }
    }
}
