using System;
using System.Collections.Generic;

namespace WalmartPro.Models;

public partial class ShoppingCart
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    public virtual User User { get; set; } = null!;
}
