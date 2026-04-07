using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbShipingType:BaseEntity
{
  

    public string? ShipingTypeAname { get; set; }

    public string? ShipingTypeEname { get; set; }

    public double ShipingFactor { get; set; }

   

    public virtual ICollection<TbShipment> TbShipments { get; set; } = new List<TbShipment>();
}
