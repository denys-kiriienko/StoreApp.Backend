using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.ComponentFiles.CardItem;

public partial class CardItemComponent
{
    [Inject] private NavigationManager navigationManager { get; set; } = default!;
    
    [Parameter] public int Id { get; set; }
    [Parameter] public string ImageSrc { get; set; } = string.Empty;
    [Parameter] public string ImageWidth { get; set; } = "294px";
    [Parameter] public string ImageHeight { get; set; } = "294px";
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public double Rating { get; set; } = 0.0;
    [Parameter] public string RatingText { get; set; } = string.Empty;
    [Parameter] public double CurrentPrice { get; set; }
    [Parameter] public double? OldPrice { get; set; }
    [Parameter] public double? Discount { get; set; }
    
    private void OnCardClick()
    {
        navigationManager.NavigateTo($"products/{Id}");
    }
}
