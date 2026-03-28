using BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
   
    public class SubscriptionPackageDTO : BaseDTOs
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Package name is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Package name must be between 1 and 200 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-\.#]+$",
          ErrorMessage = "Package name can only contain letters, numbers, spaces, hyphens, periods, and #")]
        public string PackageName { get; set; } = null!;

        [Range(1, 1000, ErrorMessage = "Shipment count must be between 1 and 1000")]
        public int ShippimentCount { get; set; }

        [Range(0.1, 10000.0, ErrorMessage = "Number of kilometers must be between 0.1 and 10,000")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Kilometers can have maximum 2 decimal places")]
        public double NumberOfKiloMeters { get; set; }

        [Range(0.01, 5000.0, ErrorMessage = "Total weight must be between 0.01 and 5,000 kg")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Weight can have maximum 2 decimal places")]
        public double TotalWeight { get; set; }

    }
}
