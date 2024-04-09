using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WalmartPro.Models;

public partial class User
{
    public int UserId { get; set; }

    [Display(Name ="First Name")]
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;


    [Required]
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    [Display(Name = "Mobile Number")]
    [Required]
    public string? MobileNumber { get; set; }

    [Display(Name = "Registration Date")]
    public DateTime RegistrationDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public virtual ICollection<CustomerSupport> CustomerSupports { get; set; } = new List<CustomerSupport>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; } = new List<ShippingAddress>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
