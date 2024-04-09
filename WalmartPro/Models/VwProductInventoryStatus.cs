using System;
using System.Collections.Generic;

namespace WalmartPro.Models;

public partial class VwProductInventoryStatus
{
    public string ProductName { get; set; } = null!;

    public int? TotalStockQuantity { get; set; }

    public string InventoryStatus { get; set; } = null!;
}
