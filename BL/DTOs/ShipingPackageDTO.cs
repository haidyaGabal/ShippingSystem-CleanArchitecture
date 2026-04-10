using BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class ShipingPackageDTO : BaseDTOs
    {

        [Required(ErrorMessage = "Arabic name is required")]
        [StringLength(200, ErrorMessage = "Arabic name cannot exceed 200 characters")]
        public string? TbShipingPackagesAname { get; set; }

        [Required(ErrorMessage = "English name is required")]
        [StringLength(200, ErrorMessage = "English name cannot exceed 200 characters")]
        public string? TbShipingPackagesEname { get; set; }

    }
}
