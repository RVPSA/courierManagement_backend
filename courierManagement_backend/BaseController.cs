using BusinessObjects.Common;
using courierManagement_backend.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;

namespace courierManagement_backend
{
    [EnableCors("AllowSpecificOrigin")]
    [ActionFilter]
    public class BaseController: Controller
    {
        public GeneralResponse ResponseMessage(int statusCode, string message, object result) {

            GeneralResponse general = new GeneralResponse()
            {
                StatusCode = statusCode,
                Message = message,
                Result = result,
            };
            return general;
        }

        public Session GetSession() {
            Session session = new Session();
            if (HttpContext != null && HttpContext.Request != null && HttpContext.Request.Headers != null) {

                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Headers["userId"]))
                    session.UserId = Convert.ToInt32(HttpContext.Request.Headers["userId"].ToString());

                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Headers["userRoleId"]))
                    session.UserRoleId = Convert.ToInt32(HttpContext.Request.Headers["userRoleId"].ToString());

                if(!StringValues.IsNullOrEmpty(HttpContext.Request.Headers["userName"]))
                    session.UserName = HttpContext.Request.Headers["userName"].ToString();

                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Headers["jwt"]))
                    session.Cookie = HttpContext.Request.Headers["jwt"].ToString();
            }
            return session;
        }
    }
}
