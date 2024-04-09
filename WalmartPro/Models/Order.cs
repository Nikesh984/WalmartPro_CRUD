using System;
using System.Collections.Generic;

namespace WalmartPro.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? OrderStatus { get; set; }

    public string ShippingMethod { get; set; } = null!;

    public DateTime? OrderDate { get; set; }

    public virtual ICollection<CustomerSupport> CustomerSupports { get; set; } = new List<CustomerSupport>();

    public virtual ICollection<DeliveryLogistic> DeliveryLogistics { get; set; } = new List<DeliveryLogistic>();

    public virtual ICollection<OrderProductLine> OrderProductLines { get; set; } = new List<OrderProductLine>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User? User { get; set; }
}
