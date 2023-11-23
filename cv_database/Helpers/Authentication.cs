using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace cv_database.Helpers
{
    public class Authentication : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyUserId = "Authorization";
      
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyUserId, out var APIKey))
            {
                context.Result = new BadRequestResult();
                return;
            }
            else
            {
                var check = CheckAPIKey(APIKey);
                if (!check)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
            await next();
        }
        public static bool CheckAPIKey(string key)
        {
            if (key == "")
            {
                return false;
            }
            return true;
        }
    }
}



