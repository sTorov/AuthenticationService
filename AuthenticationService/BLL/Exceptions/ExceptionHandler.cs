using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenticationService.BLL.Exceptions
{
    public class ExceptionHandler : /*ActionFilterAttribute,*/ Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string message = "Произошла непредвиденная ошибка. Администрация сайта уже спешит на помощь!";

            if (context.Exception is CustomException)
                message = context.Exception.Message;

            context.Result = new BadRequestObjectResult(message);
        }
    }
}
