using System;
using System.Collections.Generic;

namespace WalmartPro.Models;

public partial class CustomerSupport
{
    public int SupportId { get; set; }

    public int UserId { get; set; }

    public int? OrderId { get; set; }

    public string IssueDescription { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public virtual Order? Order { get; set; }

    public virtual User User { get; set; } = null!;
}
