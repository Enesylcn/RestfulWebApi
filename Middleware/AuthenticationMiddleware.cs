using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebApi.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._next = next;
        }
        public async Task Invoke(HttpContext context)
        {

            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized Access!");
                return;
            }

            //Basic userid:password
            var header = context.Request.Headers["Authorization"].ToString();
            var encodedCreds = header.Substring(6);
            var creds = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCreds));
            string[] uidpwd = creds.Split(':');
            var email = uidpwd[0];
            var password = uidpwd[1];

            if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized Access!");
                return;
            }

            IConfigurationSection? basicAuth = this._configuration.GetSection("BasicAuth");
            string? basicAuthEmail = basicAuth?.GetSection("UserEmail")?.Value?.ToString() ?? (string?)null;
            string? basicAuthPassword = basicAuth?.GetSection("UserPassword")?.Value?.ToString() ?? (string?)null;

            bool IsAuthEmailOK = false;
            bool IsAuthPasswordOK = false;
            if (!string.IsNullOrWhiteSpace(basicAuthEmail) && email.Equals(basicAuthEmail))
            {
                IsAuthEmailOK = true;
            }
            if (!string.IsNullOrWhiteSpace(basicAuthPassword) && password.Equals(basicAuthPassword))
            {
                IsAuthPasswordOK = true;
            }

            if (!IsAuthEmailOK || !IsAuthPasswordOK)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized Access!");
                return;
            }

            await _next(context);
        }
    }
}