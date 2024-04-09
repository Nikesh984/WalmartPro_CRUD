using System;
using System.Collections.Generic;

namespace WalmartPro.Models;

public partial class VwAggregatedProductReviewsExtended
{
    public string ProductName { get; set; } = null!;

    public int? AverageRating { get; set; }

    public int? NumberOfRatings { get; set; }

    public int? NoOf5Ratings { get; set; }

    public int? NoOf4Ratings { get; set; }

    public int? NoOf3Ratings { get; set; }

    public int? NoOf2Ratings { get; set; }

    public int? NoOf1Ratings { get; set; }

    public DateTime? LatestReviewDate { get; set; }
}
