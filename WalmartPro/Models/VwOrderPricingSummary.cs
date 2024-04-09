using System;
using System.Collections.Generic;

namespace WalmartPro.Models;

public partial class VwOrderPricingSummary
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public decimal? ActualPrice { get; set; }

    public decimal? GrandTotal { get; set; }
}
