using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.ComponentFiles.CardItem;

public partial class CardItemComponent
{
    [Parameter] public string ImageSrc { get; set; } = string.Empty;
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public double Rating { get; set; } = 0.0;
    [Parameter] public string RatingText { get; set; } = string.Empty;
    [Parameter] public string CurrentPrice { get; set; } = string.Empty;
    [Parameter] public string? OldPrice { get; set; }
    [Parameter] public string? Discount { get; set; }
}
