using BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class PaymentMethodDTO:BaseDTOs
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Arabic method name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Arabic method name must be between 1 and 100 characters")]
        [RegularExpression(@"^[\p{IsArabic}\s\-\.]+$",
            ErrorMessage = "Arabic method name can only contain Arabic letters, spaces, hyphens, and periods")]
        public string MethdAname { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "English method name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "English method name must be between 1 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z\s\-\.]+$",
            ErrorMessage = "English method name can only contain English letters, spaces, hyphens, and periods")]
        public string MethodEname { get; set; } = null!;

        [Range(0, 100, ErrorMessage = "Commission must be between 0 and 100")]
        public double? Commission { get; set; }
    }
}
