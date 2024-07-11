using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestfulWebApi.Interfaces;

namespace RestfulWebApi.Filters
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class FakeAuthorizeAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();

            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            
            var token = authorizationHeader.ToString();
            var parts = token.Split(' ');
            if (parts.Length != 2)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var user = authService.Authenticate(parts[0], parts[1]);
            if (user == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Add user information to the context if needed
            context.HttpContext.Items["User"] = user;

            await next();
        }  
    }
}