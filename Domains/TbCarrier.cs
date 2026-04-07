using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbCarrier:BaseEntity
{


    public string CarrierName { get; set; } = null!;

   

    public virtual ICollection<TbShipmentStatus> TbShippmentStatuses { get; set; } = new List<TbShipmentStatus>();
}
