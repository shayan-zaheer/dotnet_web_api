using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core_Web_API.Filters
{
    public class Player_ValidateJersey : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var playerJersey = context.ActionArguments["jersey"] as int?;
            if (playerJersey.HasValue)
            {
                if (playerJersey.Value < 10)
                {
                    context.ModelState.AddModelError("PlayerJersey", "Player jersey is invalid!");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };

                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }
        }
    }
}