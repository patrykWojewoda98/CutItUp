using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace CutItUp.Data.Data.Client
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [BindNever]
        [Display(Name = "Hasło")]
        public string PasswordHash { get; set; }

        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Imię")]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Nazwisko")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Telefon")]
        public string? PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Adres")]
        [StringLength(100)]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Miasto")]
        [StringLength(50)]
        public string? City { get; set; }

        [Display(Name = "Kod pocztowy")]
        [StringLength(10)]
        public string? PostalCode { get; set; }

        [Required]
        [Display(Name = "Kraj")]
        [StringLength(50)]
        public string Country { get; set; } = string.Empty;

        public CutItUp.Data.Data.Cart.Cart Cart { get; set; } = null!;
    }
}
