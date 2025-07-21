using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;

namespace StoreApp.Client.Components.Pages.ProductDetail.Components;

public partial class ReviewForm : ComponentBase
{
    [Parameter]
    public EventCallback<ReviewModel> OnSubmit { get; set; }

    private ReviewModel review = new();

    private async Task SubmitReview()
    {
        if (review.Rating is < 1 or > 5)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(review.Comment))
        {
            return;
        }

        await OnSubmit.InvokeAsync(review);
        review = new ReviewModel();
    }
}