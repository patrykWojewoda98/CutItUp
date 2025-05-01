using System.ComponentModel.DataAnnotations;

namespace Intranet.Models
{
    public class UserModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; } = string.Empty;

        [Display(Name = "Nowe hasło (opcjonalnie)")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Display(Name = "Stanowisko")]
        public string Position { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Wynagrodzenie (PLN)")]
        [Range(0, 9999999, ErrorMessage = "Wynagrodzenie musi być dodatnie.")]
        public decimal Salary { get; set; }

        [Display(Name = "Zdjęcie użytkownika")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageFile { get; set; }

        public string? ExistingImageUrl { get; set; }
    }
}

