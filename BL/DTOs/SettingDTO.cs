using BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
  
    public class SettingDTO : BaseDTOs
    {

        [Range(0, double.MaxValue, ErrorMessage = "Kilometer rate must be a positive number")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Kilometer rate can have maximum 2 decimal places")]
        public double? KiloMeterRate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Kilogram rate must be a positive number")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Kilogram rate can have maximum 2 decimal places")]
        public double? KilooGramRate { get; set; }
    }
}
