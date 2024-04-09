using System;
using System.Collections.Generic;

namespace WalmartPro.Models;

public partial class VwEnhancedSupportTicketOverview
{
    public int SupportId { get; set; }

    public int UserId { get; set; }

    public string? FullName { get; set; }

    public int? OrderId { get; set; }

    public string IssueDescription { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int? DaysOpen { get; set; }

    public string Priority { get; set; } = null!;

    public DateTime CreatedOn { get; set; }
}
