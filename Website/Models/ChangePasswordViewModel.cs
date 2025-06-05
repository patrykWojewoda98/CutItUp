using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class ChangePasswordViewModel
    {
        public string Token { get; set; }
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Hasła muszą być takie same.")]
        public string ConfirmPassword { get; set; }
    }
}