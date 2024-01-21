using ApplicationService.User;
using BusinessObjects.Common;
using BusinessObjects.User;
using Configurations;
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
            paramCollection[0] = DataServiceBuilder.CreateDBParameter("@RoleId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input,
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
                    string token = CreateJWt(response);
                    CreateCookie(token);

                    return this.ResponseMessage(200, "Success", response);
                }
                else {
                    return this.ResponseMessage(200, "Fail", null);
                }
            }
            catch (Exception ex) {

                return this.ResponseMessage(500, "Exception", ex.Message);
            }
        }

        private string CreateJWt(LoginUserResponse user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.SecretKey);

            //claims
            Claim[] claim = new Claim[] {
                new Claim("userName",user.UserName.ToString()),
                new Claim("userRoleId",user.UserRoleId.ToString()), 
                new Claim("UserId",user.UserId.ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(AppSettings.TokenExpiration)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private void CreateCookie(string token) {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(Convert.ToInt32(AppSettings.CookieExpire));
            options.HttpOnly = true;
            options.Path = AppSettings.CookiePath;
            options.Secure = true;
            options.SameSite = SameSiteMode.None;
            options.Domain = AppSettings.CookieDomain;
            Response.Cookies.Append(AppSettings.CookieName,token,options);
        }

        [HttpPost]
        [AllowAnonymous]
        public GeneralResponse RegisterUser(RegisterUser userRegister) {
            UserApplicationService userApplicationService = new UserApplicationService();
            try {
                RegisterUserResponse registerUserResponse =  userApplicationService.RegisterUser(userRegister);
                return this.ResponseMessage(200,"Success",registerUserResponse);
            }
            catch (Exception ex) {
                return this.ResponseMessage(500,"Exception",ex.Message);
            }
        }

        [HttpPost]
        public GeneralResponse AddUserDetails(UserDetails userDetails) {
            UserApplicationService userApplicationService = new UserApplicationService();

            try {
                Session session = this.GetSession();

                UserDetails _userDetails = new UserDetails
                {
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    Address = userDetails.Address,
                    PhoneNumber = userDetails.PhoneNumber,
                    DateOfBirth = userDetails.DateOfBirth,
                    UserId = session.UserId
                };

                int result = userApplicationService.AddUserDetails(_userDetails);
                if (result == 0)
                {
                    return this.ResponseMessage(200, "Fail", null);
                }
                else {
                    return this.ResponseMessage(200, "Success", null);
                }

            }
            catch (Exception ex) {
                return this.ResponseMessage(500,"Exception",ex.Message);
            }
        }


    }
}
