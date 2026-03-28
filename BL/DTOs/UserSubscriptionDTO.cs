using BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class UserSubscriptionDTO : BaseDTOs
    {
        public Guid UserId { get; set; }

        public Guid PackageId { get; set; }

        [Required(ErrorMessage = "Subscription date is required")]
        [DataType(DataType.DateTime)]  
        public DateTime SubscriptionDate { get; set; }

    }
}
