using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WalmartPro.Models;

public partial class Product
{
    public int ProductId { get; set; }

    [Display(Name = "Product Name")]
    public string ProductName { get; set; } = null!;

    [Display(Name = "Product Description")]
    public string? ProductDescription { get; set; }

    [Display(Name = "Stock Quantity")]
    public int? StockQuantity { get; set; }

    [Display(Name = "Actual Price")]
    public decimal? ActualPrice { get; set; }

    [Display(Name = "Category")]
    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderProductLine> OrderProductLines { get; set; } = new List<OrderProductLine>();

    public virtual ICollection<ProductPromotion> ProductPromotions { get; set; } = new List<ProductPromotion>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    public virtual ICollection<SupplyLogistic> SupplyLogistics { get; set; } = new List<SupplyLogistic>();

    public virtual ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
}
