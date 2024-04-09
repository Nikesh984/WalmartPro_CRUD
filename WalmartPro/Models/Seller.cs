using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WalmartPro.Models;

public partial class Seller
{
    public int SellerId { get; set; }

    [Display(Name = "Seller First Name")]
    public string SellerFirstName { get; set; } = null!;


    [Display(Name = "Seller Last Name")]
    public string SellerLastName { get; set; } = null!;

    [Display(Name = "Seller Email")]
    public string SellerEmail { get; set; } = null!;

    [Display(Name = "Seller Username")]
    public string SellerUsername { get; set; } = null!;

    [Display(Name = "Seller Password")]
    public string SellerPassword { get; set; } = null!;


    [Display(Name = "Seller Mobile Number")]
    public string? SellerMobileNumber { get; set; }


    [Display(Name = "Registartion Date")]
    public DateTime RegistrationDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public virtual ICollection<SupplyLogistic> SupplyLogistics { get; set; } = new List<SupplyLogistic>();
}
