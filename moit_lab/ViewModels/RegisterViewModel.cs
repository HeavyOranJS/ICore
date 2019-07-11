using System.ComponentModel.DataAnnotations;
namespace moit_lab.ViewModels
{
    public class RegisterViewModel
    {
        public long Id { get; set; }//PK syntax

        [StringLength(30, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$")]
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [StringLength(40, MinimumLength = 5)]
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(60, MinimumLength = 5)]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [StringLength(60, MinimumLength = 5)]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConformarion { get; set; }

        public string Salt { get; set; }
        public string Role { get; set; }
    }
}
