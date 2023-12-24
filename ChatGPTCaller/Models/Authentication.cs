namespace ChatGPTCaller.Models
{
    public class RegisterRequest
    {
        public User User { get; set; }
    }

    public class RegisterResponse
    {
        public bool RegisterResult { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class LogInRequest
    {
        public User User { get; set; }
    }

    public class LogInResponse
    {
        public bool LogInResult { get; set; }
        public string UserID { get; set; }
        public string UserRole { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class User
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
