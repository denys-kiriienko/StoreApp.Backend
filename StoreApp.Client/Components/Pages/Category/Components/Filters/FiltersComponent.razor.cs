using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.Pages.Category.Components.Filters;

public partial class FiltersComponent
{
    [Parameter] public bool IsMobileOverlay { get; set; } = false;
    [Parameter] public EventCallback OnClose { get; set; }
}
