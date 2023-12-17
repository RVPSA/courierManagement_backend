using BusinessObjects.User;
using System;

namespace ApplicationService.User
{
    public class UserApplicationService
    {
        public LoginUserResponse LoginUser(LoginUser loginUser) {
            LoginUserResponse response = new LoginUserResponse();
            try {
                if (loginUser.UserName == "Admin" && loginUser.Password == "admin@12345")
                {
                   
                    response.UserId = 1;
                    response.UserName = "Admin";
                    response.JwtToken = "";
                    return response;
                }
                else {

                    return response=null;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        
        
    }
}
