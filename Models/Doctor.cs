using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hos.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5 , ErrorMessage ="اكتب اكثر من 5 احرف")]
        [MaxLength(100, ErrorMessage = "اكتب اقل من 100 احرف")]
        public string Name { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "اكتب اكثر من 5 احرف")]
        [MaxLength(100, ErrorMessage = "اكتب اقل من 100 احرف")]
        public string Specialization { get; set; }
        [ValidateNever]
        public string Img { get; set; }
        [ValidateNever]
        public List<HistoryofDate> History { get; set; }
    }
}
