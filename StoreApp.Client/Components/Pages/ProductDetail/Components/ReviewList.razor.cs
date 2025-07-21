using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;

namespace StoreApp.Client.Components.Pages.ProductDetail.Components;

public partial class ReviewList : ComponentBase
{
    [Parameter]
    public List<ReviewModel> Reviews { get; set; } = [];

    [Parameter]
    public EventCallback<ReviewModel> WriteReviewClicked { get; set; }

    [Parameter]
    public bool IsReadOnly { get; set; } = false;
    
    private bool isReviewFormVisible = false;

    private async Task OnWriteReviewClicked(ReviewModel review)
    {
        if (WriteReviewClicked.HasDelegate)
        {
            await WriteReviewClicked.InvokeAsync(review);
        }
        isReviewFormVisible = false;
    }
}