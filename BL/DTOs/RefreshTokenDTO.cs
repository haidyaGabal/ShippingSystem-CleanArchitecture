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
    public class RefreshTokenDTO:BaseDTOs
    {
        public string Token { get; set; }

        public string UserId { get; set; }

        public DateTime Expires { get; set; }

        public int CurrentState { get; set; }
    }
}
