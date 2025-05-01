using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CutItUp.Data.Data.User
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Required]
        [BindNever]
        [Display(Name = "Hasło")]
        public string PasswordHash { get; set; }
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }
        [Required]
        [Display(Name = "Stanowisko")]
        public string Position { get; set; }
        [Required]
        [Display(Name = "Salary")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Salary { get; set; }
        [Display(Name = "Zdjęcie")]
        public string? ImageUrl { get; set; } = string.Empty;
    }
}
