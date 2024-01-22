using System;

namespace BusinessObjects.User
{
    public class LoginUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserResponse {
        public string UserName { get; set; }
        public string ? JwtToken { get; set; }
        public int UserRoleId { get; set; }

        public int UserId { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUser {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
    }
    public class RegisterUserResponse
    {
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
    }

    public class UserDetails {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
        public int ? UserId { get; set; } 
    }
}
