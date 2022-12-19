using System.ComponentModel.DataAnnotations;

namespace TestTaskMVC.Data.DTOs
{
    public class UserRegister
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
