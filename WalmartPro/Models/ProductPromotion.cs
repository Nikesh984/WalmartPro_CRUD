using System;
using System.Collections.Generic;

namespace WalmartPro.Models;

public partial class ProductPromotion
{
    public int PromotionProductId { get; set; }

    public int PromotionId { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Promotion Promotion { get; set; } = null!;
}
