using BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class ShippingTypeDTO : BaseDTOs
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Arabic shipping type name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Arabic shipping type name must be between 1 and 100 characters")]
        [RegularExpression(@"^[\p{IsArabic}\s\-\.]+$",
             ErrorMessage = "Arabic shipping type name can only contain Arabic letters, spaces, hyphens, and periods")]
        public string ShippingTypeAname { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "English shipping type name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "English shipping type name must be between 1 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z\s\-\.]+$",
            ErrorMessage = "English shipping type name can only contain English letters, spaces, hyphens, and periods")]
        public string ShippingTypeEname { get; set; } = null!;

        [Range(0.1, 10.0, ErrorMessage = "Shipping factor must be between 0.1 and 10.0")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Shipping factor can have maximum 2 decimal places")]
        public double ShippingFactor { get; set; }

    }
}
