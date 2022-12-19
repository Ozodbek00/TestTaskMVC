using TestTaskMVC.Data.Enums;

namespace TestTaskMVC.Models
{
    public sealed class User
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; } = UserRole.User;
    }
}
