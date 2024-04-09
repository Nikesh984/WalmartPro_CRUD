using System;
using System.Collections.Generic;

namespace WalmartPro.Models;

public partial class VwActiveProductPromotion
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string SellerFullName { get; set; } = null!;

    public string SellerEmail { get; set; } = null!;

    public int PromotionId { get; set; }

    public int? DiscountPercentage { get; set; }

    public decimal? ActualPrice { get; set; }

    public decimal? DiscountedPrice { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
