using System;
using System.Collections.Generic;

namespace WalmartPro.Models;

public partial class ShippingAddress
{
    public int ShippingAddressId { get; set; }

    public int UserId { get; set; }

    public string? AppartmentName { get; set; }

    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string? Region { get; set; }

    public string Country { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
