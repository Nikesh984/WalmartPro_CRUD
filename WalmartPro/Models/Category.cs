using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WalmartPro.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    [Display(Name = "Category Name")]
    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
