using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbShipingPackage : BaseEntity
{
  

    public string? TbShipingPackagesAname { get; set; }

    public string? TbShipingPackagesEname { get; set; }



   

    public virtual ICollection<TbShipment> TbShippments { get; set; } = new List<TbShipment>();
}
