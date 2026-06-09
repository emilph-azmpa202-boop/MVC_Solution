using System.ComponentModel.DataAnnotations;

namespace solution.ViewModels.Auth
{
    public class LoginVM
    {
        [Required]
        public string UserNameOrEmail { get; set; }

        [Required]
        public string Password { get; set; }

    }
}