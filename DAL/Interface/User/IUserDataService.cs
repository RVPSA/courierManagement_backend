using BusinessObjects.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL.Interface.User
{
    public interface IUserDataService
    {
        public int RegisterUser(RegisterUser registerUser);
        public LoginUserResponse LoginUser(LoginUser loginUser);

        public bool AddUserDetails(UserDetails userDetails);

        /// <summary>
        /// Only for testing
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public bool AddListTest(DataTable names);
    }
}
