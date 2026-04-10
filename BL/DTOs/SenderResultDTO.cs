using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class SenderResultDTO
    {
        public bool IsSuccess { get; set; }
        public TbUserSender? Data { get; set; }
        public bool IsExisting { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
