using System.ComponentModel.DataAnnotations;

namespace TestTaskMVC.Data.DTOs
{
    public class UserLogin
    {
        [Required(ErrorMessage = "UserName is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
