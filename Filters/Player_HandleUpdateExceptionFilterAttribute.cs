using Microsoft.AspNetCore.Mvc.Filters;

namespace Core_Web_API.Filters
{
    public class Player_HandleUpdateExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var id = context.RouteData.Values["id"] as string;
            
        }
    }
}