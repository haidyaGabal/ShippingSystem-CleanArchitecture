using BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class ShippmentDTO : BaseDTOs
    {
        [Required(ErrorMessage = "Shipping date is required")]
        [DataType(DataType.DateTime)]
        public DateTime ShippingDate { get; set; }

        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

    
        public Guid ShippingTypeId { get; set; }

        [Range(0.1, 1000.0, ErrorMessage = "Width must be between 0.1 and 1000.0 cm")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Width can have maximum 2 decimal places")]
        public double Width { get; set; }

        [Range(0.1, 1000.0, ErrorMessage = "Height must be between 0.1 and 1000.0 cm")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Height can have maximum 2 decimal places")]
        public double Height { get; set; }

        [Range(0.01, 500.0, ErrorMessage = "Weight must be between 0.01 and 500.0 kg")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Weight can have maximum 2 decimal places")]
        public double Weight { get; set; }

        [Range(0.1, 1000.0, ErrorMessage = "Length must be between 0.1 and 1000.0 cm")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Length can have maximum 2 decimal places")]
        public double Length { get; set; }

        [Range(0, 1000000, ErrorMessage = "Package value must be between 0 and 1,000,000")]
        [DataType(DataType.Currency)]
        public decimal PackageValue { get; set; }

        [Range(0, 10000, ErrorMessage = "Shipping rate must be between 0 and 10,000")]
        [DataType(DataType.Currency)]
        public decimal ShippingRate { get; set; }

        public Guid? PaymentMethodId { get; set; }

        public Guid? UserSubscriptionId { get; set; }

        [Range(1000000000, 9999999999, ErrorMessage = "Tracking number must be 10 digits")]
        public double? TrackingNumber { get; set; }

        public Guid? ReferenceId { get; set; }
    }
}
