using BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class ShippmentStatusDTO : BaseDTOs
    {

        public Guid? ShipmentId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Notes are required")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Notes must be between 1 and 500 characters")]
        public string Notes { get; set; } = null!;


        public Guid CarrierId { get; set; }
    }
}
