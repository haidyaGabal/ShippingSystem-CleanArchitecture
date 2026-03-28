using BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class CityDTO:BaseDTOs
    {
        [Required(ErrorMessage = "CityAname name is required", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "CityAname name must be between 5 and 100 characters"),]
        public string? CityAname { get; set; }
        [Required(ErrorMessage = "CityEname name is required", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "CityEname name must be between 4 and 100 characters")]
        public string? CityEname { get; set; }

        public Guid CountryId { get; set; }
    }
}
