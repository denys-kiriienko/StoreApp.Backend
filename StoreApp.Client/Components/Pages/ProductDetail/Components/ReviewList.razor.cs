using Microsoft.AspNetCore.Components;
using StoreApp.Client.Enums;
using StoreApp.Client.Models;

namespace StoreApp.Client.Components.Pages.ProductDetail.Components;

public partial class ReviewList : ComponentBase
{
    [Parameter]
    public List<ReviewModel> Reviews { get; set; } = [];

    [Parameter]
    public EventCallback<ReviewModel> WriteReviewClicked { get; set; }

    [Parameter]
    public bool IsReadOnly { get; set; }

    private Dictionary<ReviewSortOptions, string> sortOptions = new()
    {
        { ReviewSortOptions.Latest, "Latest" },
        { ReviewSortOptions.HighestRating, "Highest" },
        { ReviewSortOptions.LowestRating, "Lowest" },
    };
    
    private ReviewSortOptions selectedSortOption = ReviewSortOptions.Latest;

    private bool isReviewFormVisible = false;
    private bool showAllReviews = false;

    private IEnumerable<ReviewModel> DisplayedReviews()
    {
        IEnumerable<ReviewModel> sortedReviews = selectedSortOption switch
        {
            ReviewSortOptions.Latest => Reviews.OrderByDescending(r => r.CreatedAt),
            ReviewSortOptions.HighestRating => Reviews.OrderByDescending(r => r.Rating),
            ReviewSortOptions.LowestRating => Reviews.OrderBy(r => r.Rating),
            _ => Reviews
        };

        return showAllReviews ? sortedReviews : sortedReviews.Take(6);
    }

    private string ToggleButtonText => showAllReviews ? "Hide" : "Load More Reviews";

    private void ToggleShowAll()
    {
        showAllReviews = !showAllReviews;
    }

    private async Task OnWriteReviewClicked(ReviewModel review)
    {
        if (WriteReviewClicked.HasDelegate)
        {
            await WriteReviewClicked.InvokeAsync(review);
        }
        isReviewFormVisible = false;
        showAllReviews = true; // показати новий review після сабміту
    }
}