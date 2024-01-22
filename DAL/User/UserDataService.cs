using BusinessObjects.User;
using DAL.Interface.User;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DAL.User
{
    public class UserDataService: IUserDataService
    {
        IDataService _dataService;
        public UserDataService(IDataService dataService) {
            _dataService = dataService;
        }

        public int RegisterUser(RegisterUser registerUser) {
            try {
                DbParameter[] dbParameters = new DbParameter[5];
                dbParameters[0] = DataServiceBuilder.CreateDBParameter("@UserName",System.Data.DbType.String,System.Data.ParameterDirection.Input,registerUser.UserName);
                dbParameters[1] = DataServiceBuilder.CreateDBParameter("@RoleId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, registerUser.RoleId);
                dbParameters[2] = DataServiceBuilder.CreateDBParameter("@password",System.Data.DbType.String,System.Data.ParameterDirection.Input,registerUser.Password);
                dbParameters[3] = DataServiceBuilder.CreateDBParameter("@Email", System.Data.DbType.String, System.Data.ParameterDirection.Input, registerUser.Email);
                dbParameters[4] = DataServiceBuilder.CreateDBParameter("@UserId", System.Data.DbType.String, System.Data.ParameterDirection.Output,0);

                _dataService.ExecuteReader("UM.registeruser", dbParameters);

                return Convert.ToInt32(dbParameters[4].Value);
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        public LoginUserResponse LoginUser(LoginUser loginUser) {
            try {
                LoginUserResponse loginUserResponse = new LoginUserResponse();

                DbParameter[] dbParameter = new DbParameter[1];

                dbParameter[0] = DataServiceBuilder.CreateDBParameter("@UserName",System.Data.DbType.String,System.Data.ParameterDirection.Input,loginUser.UserName);

                DbDataReader reader = _dataService.ExecuteReader("UM.GetUserLoginDetails", dbParameter);
                if (reader.HasRows) {
                    while (reader.Read()) {
                        DataReader da = new DataReader(reader);
                        loginUserResponse.UserName = da.GetString("UserName");
                        loginUserResponse.UserRoleId = da.GetInt32("UserRoleId");
                        loginUserResponse.UserId = da.GetInt32("UserId");
                        loginUserResponse.Password = da.GetString("Password");
                    }
                }
                return loginUserResponse;

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public bool AddUserDetails(UserDetails userDetails) {

            try {

                DbParameter[] dbParameters = new DbParameter[6];

                dbParameters[0] = DataServiceBuilder.CreateDBParameter("@FirstName", System.Data.DbType.String, System.Data.ParameterDirection.Input, userDetails.FirstName);
                dbParameters[1] = DataServiceBuilder.CreateDBParameter("@LastName", System.Data.DbType.String, System.Data.ParameterDirection.Input, userDetails.LastName);
                dbParameters[2] = DataServiceBuilder.CreateDBParameter("@Address", System.Data.DbType.String, System.Data.ParameterDirection.Input, userDetails.Address);
                dbParameters[3] = DataServiceBuilder.CreateDBParameter("@PhoneNumber", System.Data.DbType.String, System.Data.ParameterDirection.Input, userDetails.PhoneNumber);
                dbParameters[4] = DataServiceBuilder.CreateDBParameter("@DateOfBirth", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, userDetails.DateOfBirth);
                dbParameters[5] = DataServiceBuilder.CreateDBParameter("@UserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, userDetails.UserId);

                _dataService.ExecuteNonQuery("UM.AddUserDetails", dbParameters);

                return true;

            }
            catch (Exception ex) {
                throw ex;
            }

            
        }
    }
}
