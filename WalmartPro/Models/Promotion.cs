using System;
using System.Collections.Generic;

namespace WalmartPro.Models;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public int? DiscountPercentage { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<ProductPromotion> ProductPromotions { get; set; } = new List<ProductPromotion>();
}
