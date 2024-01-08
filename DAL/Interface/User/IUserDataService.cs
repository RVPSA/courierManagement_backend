using BusinessObjects.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interface.User
{
    public interface IUserDataService
    {
        public int RegisterUser(RegisterUser registerUser);
        public LoginUserResponse LoginUser(LoginUser loginUser);
    }
}
