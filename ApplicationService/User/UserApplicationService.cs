using BusinessObjects.User;
using DAL;
using DAL.Interface.User;
using DAL.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ApplicationService.User
{
    public class UserApplicationService
    {
        public LoginUserResponse LoginUser(LoginUser loginUser)
        {
            IDataService dataService = DataServiceBuilder.CreateDataService();
            LoginUserResponse response = new LoginUserResponse();

            try
            {
                IUserDataService userDataService = new UserDataService(dataService);

                if (loginUser.UserName != "" && loginUser.Password != "")
                {
                    string encryptPassword = BCrypt.Net.BCrypt.HashPassword(loginUser.Password);

                    response = userDataService.LoginUser(loginUser);

                    if (BCrypt.Net.BCrypt.Verify(loginUser.Password, response.Password))
                        return response;
                    else
                        return null;
                }
                else
                {
                    return response = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RegisterUserResponse RegisterUser(RegisterUser registerUser)
        {
            IDataService dataService = DataServiceBuilder.CreateDataService();
            try
            {
                IUserDataService userDataService = new UserDataService(dataService);

                RegisterUser _registerUser = new RegisterUser
                {
                    UserName = registerUser.UserName,
                    Password = BCrypt.Net.BCrypt.HashPassword(registerUser.Password),
                    RoleId = registerUser.RoleId,
                    Email = registerUser.Email,
                };

                int userId = userDataService.RegisterUser(_registerUser);

                return new RegisterUserResponse
                {
                    UserName = registerUser.UserName,
                    RoleId = registerUser.RoleId,
                    Email = registerUser.Email,
                    UserId = userId,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddUserDetails(UserDetails userDetails)
        {
            IDataService dataService = DataServiceBuilder.CreateDataService();
            try
            {
                IUserDataService userDataService = new UserDataService(dataService);
                bool result = userDataService.AddUserDetails(userDetails);

                return result ? 1 : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Only for testing
        /// </summary>
        /// <returns></returns>
        public bool AddBulkCustomer()
        {
            IDataService dataService = DataServiceBuilder.CreateDataService();
            IUserDataService userDataService = new UserDataService(dataService);

            List<string> names = new List<string>();

            names.Add("A");
            names.Add("B");
            names.Add("C");
            DataTable nameT = CommonApplicationService.CommonApplicationService.ToDataTable(names.Select(a => new { a }).ToList());

            bool result = userDataService.AddListTest(nameT);
            return result;
        }
    }
}