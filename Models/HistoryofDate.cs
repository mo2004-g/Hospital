using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hos.Models
{
    public class HistoryofDate
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3,ErrorMessage ="يجب ان تكتب اكثر من 3 احرف")]
        [MaxLength(30, ErrorMessage = "يجب ان تكتب اقل من 30 احرف")]
        public string Name { get; set; }
        [Required(ErrorMessage ="من فضلك ادخل تاريخ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly? Day { get; set; }
        [Required(ErrorMessage ="من فضلك ادخل وقت")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public TimeOnly? Time { get; set; }
        
        public int DoctorId { get; set; }
        [ValidateNever]
        public Doctor Doctor { get; set; }
    }
}
