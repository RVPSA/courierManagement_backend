namespace BusinessObjects.User
{
    public class LoginUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserResponse {
        public string UserName { get; set; }
        public string JwtToken { get; set; }

        public int UserId { get; set; }
    }
}
