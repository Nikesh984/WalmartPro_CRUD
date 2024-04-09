using System;
using System.Collections.Generic;

namespace WalmartPro.Models;

public partial class SupplyLogistic
{
    public int SupplyLogisticsId { get; set; }

    public int? SellerId { get; set; }

    public int ProductId { get; set; }

    public decimal SellerPrice { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Seller? Seller { get; set; }
}
