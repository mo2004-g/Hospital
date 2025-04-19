using System.ComponentModel.DataAnnotations;

namespace Hos.Models.Viewmodel
{
    public class RegisterVM
    {
        public int id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters long.")]
        [MaxLength(20, ErrorMessage = "Username must be at most 20 characters long.")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com)$", ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة ")]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmPassword { get; set; }
    }
}
