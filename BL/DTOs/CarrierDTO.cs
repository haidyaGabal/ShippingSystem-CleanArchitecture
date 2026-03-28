using BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class CarrierDTO : BaseDTOs
    {
        [Required(ErrorMessage = "Carrier name is required", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Carrier name must be between 1 and 100 characters")]
        public string CarrierName { get; set; } = null!;
    }
}
