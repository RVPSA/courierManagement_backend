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
                dbParameters[0] = DataServiceBuilder.createDBParameter("@UserName",System.Data.DbType.String,System.Data.ParameterDirection.Input,registerUser.UserName);
                dbParameters[1] = DataServiceBuilder.createDBParameter("@RoleId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, registerUser.RoleId);
                dbParameters[2] = DataServiceBuilder.createDBParameter("@password",System.Data.DbType.String,System.Data.ParameterDirection.Input,registerUser.Password);
                dbParameters[3] = DataServiceBuilder.createDBParameter("@Email", System.Data.DbType.String, System.Data.ParameterDirection.Input, registerUser.Email);
                dbParameters[4] = DataServiceBuilder.createDBParameter("@UserId", System.Data.DbType.String, System.Data.ParameterDirection.Output,0);

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

                dbParameter[0] = DataServiceBuilder.createDBParameter("@UserName",System.Data.DbType.String,System.Data.ParameterDirection.Input,loginUser.UserName);

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
    }
}
