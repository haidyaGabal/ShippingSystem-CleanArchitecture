using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbUserSender:BaseEntity
{
  

    public Guid UserId { get; set; }

    public string SenderName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public Guid CityId { get; set; }

    public string Address { get; set; } = null!;

    public string PostalCode { get; set; }
    public string OtherAddress { get; set; } = null!;
    public string Contact { get; set; } = null!;
    public bool IsDefault { get; set; } 
    public virtual TbCity City { get; set; } = null!;

    public virtual ICollection<TbShipment> TbShipments { get; set; } = new List<TbShipment>();
}
