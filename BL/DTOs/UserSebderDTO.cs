using BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class UserSebderDTO : BaseDTOs
   
    {
  
        public Guid UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Sender name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Sender name must be between 1 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z\s\-'.]+$",
            ErrorMessage = "Sender name can only contain letters, spaces, hyphens, apostrophes, and periods")]
        public string SenderName { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Email must be between 3 and 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone number is required")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "Phone number must be between 7 and 20 characters")]
        [RegularExpression(@"^[\+]?[0-9\s\-\(\)]+$",
            ErrorMessage = "Phone number can only contain numbers, spaces, hyphens, parentheses, and plus sign")]
        public string Phone { get; set; } = null!;

 
        public Guid CityId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 500 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-\.\,#\/]+$",
            ErrorMessage = "Address can only contain letters, numbers, spaces, hyphens, periods, commas, #, and /")]
        public string Address { get; set; } = null!;

    }
}
