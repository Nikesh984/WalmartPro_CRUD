using System;
using System.Collections.Generic;

namespace WalmartPro.Models;

public partial class VwEnhancedCustomerOrderSummary
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public int? NumberOfOrders { get; set; }

    public decimal? TotalSpent { get; set; }

    public decimal? AverageOrderValue { get; set; }

    public DateTime? LastOrderDate { get; set; }

    public string? LastOrderStatus { get; set; }

    public string CustomerSegment { get; set; } = null!;
}
