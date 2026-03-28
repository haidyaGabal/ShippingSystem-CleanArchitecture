using BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class CountryDTO:BaseDTOs
    {
        [Required(ErrorMessage = "CountryAname name is required", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "CountryAname name must be between 5 and 100 characters"),]
        public string? CountryAname { get; set; }
        [Required(ErrorMessage = "CountryEname name is required", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "CountryEname name must be between 5 and 100 characters"),]
        public string? CountryEname { get; set; }
    }
}
