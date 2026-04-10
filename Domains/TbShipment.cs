using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbShipment:BaseEntity
{


    public DateTime ShippingDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public Guid ShipingTypeId { get; set; }
    public Guid? ShipingPackagesId { get; set; }
    

    public double Width { get; set; }

    public double Height { get; set; }

    public double Weight { get; set; }

    public double Length { get; set; }

    public decimal PackageValue { get; set; }

    public decimal ShipingRate { get; set; }

    public Guid? PaymentMethodId { get; set; }

    public Guid? UserSubscriptionId { get; set; }

    public double? TrackingNumber { get; set; }

    public Guid? ReferenceId { get; set; }


    public virtual TbPaymentMethod? PaymentMethod { get; set; }

    public virtual TbUserReceiver Receiver { get; set; } = null!;

    public virtual TbUserSender Sender { get; set; } = null!;

    public virtual TbShipingType ShipingType { get; set; } = null!;

    public virtual TbShipingPackage ShipingPackages { get; set; }

    public virtual ICollection<TbShipmentStatus> TbShippmentStatuses { get; set; } = new List<TbShipmentStatus>();
}
