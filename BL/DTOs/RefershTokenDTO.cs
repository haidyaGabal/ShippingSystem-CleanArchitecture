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
    public class RefershTokenDTO:BaseDTOs
    {
        [Required]
        public string Token { get; set; }

        public DateTime Expires { get; set; }

        [Required]
        public string UserId { get; set; }

        public int CurrentState { get; set; }
    }
}
