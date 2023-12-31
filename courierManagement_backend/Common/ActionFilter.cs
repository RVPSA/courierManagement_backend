using Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace courierManagement_backend.Common
{
    public class ActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var filterDescriptors = context.ActionDescriptor.EndpointMetadata;
            //foreach (var filterdescriptor in filterDescriptors) {
            //    if (filterdescriptor.Filter.GetType() == typeof(AllowAnonymousFilter)) {
            //        base.OnActionExecuting(context);
            //        return;
            //    }
            //}

            if (filterDescriptors.Any(a => a.ToString() == "Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute"))
            {
                base.OnActionExecuting(context);
                return;
            }

            try
            {
                var jwtCookie = context.HttpContext.Request.Cookies[AppSettings.CookieName];
                if (string.IsNullOrEmpty(jwtCookie))
                {
                    context.Result = new UnauthorizedResult();
                }
                else
                {
                    ValidateCookie(jwtCookie, context);
                }
                base.OnActionExecuting(context);
            }
            catch (Exception ex)
            {
                context.Result = new UnauthorizedResult();
                base.OnActionExecuting(context);
            }
        }

        private void ValidateCookie(string jwtCookie, ActionExecutingContext context)
        {
            IIdentity identity = ReadJwtToken(jwtCookie, context, out var securityToken);
            if (securityToken != null)
            {
                if (securityToken.ValidTo < DateTime.UtcNow)
                {
                    context.Result = new UnauthorizedResult();
                }
                else
                {
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddMinutes(Convert.ToInt32(AppSettings.CookieExpire));
                    options.HttpOnly = true;
                    options.Path = AppSettings.CookiePath;
                    options.Secure = true;
                    options.SameSite = SameSiteMode.None;
                    options.Domain = AppSettings.CookieDomain;

                    context.HttpContext.Response.Cookies.Append(AppSettings.CookieName, jwtCookie, options);
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private IIdentity ReadJwtToken(string jwttoken, ActionExecutingContext context, out SecurityToken securityToken)
        {
            string secret = AppSettings.SecretKey;
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            var claim = handler.ValidateToken(jwttoken, validations, out var tokenSecure);
            securityToken = tokenSecure;
            return claim.Identity;

        }
    }
}
