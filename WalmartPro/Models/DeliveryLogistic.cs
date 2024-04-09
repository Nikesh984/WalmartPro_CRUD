using System;
using System.Collections.Generic;

namespace WalmartPro.Models;

public partial class DeliveryLogistic
{
    public int LogisticsId { get; set; }

    public string TrackingId { get; set; } = null!;

    public DateTime ExpectedDeliveryDate { get; set; }

    public DateTime? ActualDeliveryDate { get; set; }

    public string Status { get; set; } = null!;

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;
}
