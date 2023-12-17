using ApplicationService.User;
using BusinessObjects.Common;
using BusinessObjects.User;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace courierManagement_backend.Controllers.UserController
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : BaseController
    {
        [HttpGet]
        public int TestMethod() {
            return 10;
        }

        [HttpGet]
        public void GetUser() {
            IDataService dataService = DataServiceBuilder.CreateDataService();
            DbParameter[] paramCollection = new DbParameter[1];
            paramCollection[0] = DataServiceBuilder.createDBParameter("@RoleId",System.Data.DbType.Int32,System.Data.ParameterDirection.Input,
                1);
            DbDataReader reader = dataService.ExecuteReader("[UM].[GetUserbyId]", paramCollection);
            if (reader.HasRows)
            {
                while (reader.Read()) {
                    DataReader da = new DataReader(reader);
                    Console.WriteLine(da.GetInt32("RoleId"));
                    Console.WriteLine(da.GetString("RoleName"));
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public GeneralResponse LoginUSer(LoginUser loginUser) { 

            UserApplicationService userApplicationService = new UserApplicationService();
            try {
                var response = userApplicationService.LoginUser(loginUser);
                if (response != null)
                {
                    string token = CreateJWt(loginUser);
                    CreateCookie(token);

                    return this.ResponseMessage(200, "Success", response);
                }
                else {
                    return this.ResponseMessage(200, "Fail", null);
                }
            }
            catch (Exception ex) {
                
                return this.ResponseMessage(200, "Exception", ex.Message);
            }
        }

        private string CreateJWt(LoginUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("courier_service_management_application");

            //claims
            Claim[] claim = new Claim[] {
                new Claim("userName",user.UserName.ToString()),
                new Claim("userRoleId",Convert.ToString(1)),
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private void CreateCookie(string token) {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(5);
            options.HttpOnly = true;
            options.Path = "/";
            options.Secure = true;
            options.SameSite = SameSiteMode.None;
            options.Domain = "localhost";
            Response.Cookies.Append("jwt",token,options);
        }


    }
}
