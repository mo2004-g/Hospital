using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hos.Models
{
    public class Profile
    {
        public string Id { get; set; }
        [Required]
        [MinLength(3,ErrorMessage ="يجب ان تكتب اكثر من 3 احرف")]
        [MaxLength(50, ErrorMessage = "يجب ان لا يزيد الاسم عن 50 حرفا")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "البريد الالكتروني غير صحيح")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [ValidateNever]
        public string Address { get; set; }
        [ValidateNever]
        public List<string>? Diseases { get; set; }
        [ValidateNever]
        public string? Image { get; set; }
    }
}
