using Microsoft.AspNetCore.Components;
using StoreApp.Client.Services;

namespace StoreApp.Client.Components.ComponentFiles.Header;

public partial class HeaderComponent(ICartService cartService) : ComponentBase
{
    private int cartItemCount;

    protected override async Task OnInitializedAsync()
    {
        cartItemCount = await cartService.GetCartItemCountAsync();
        cartService.OnCartItemCountChanged += OnCartItemCountChanged;
    }

    private void OnCartItemCountChanged(int count)
    {
        cartItemCount = count;
        StateHasChanged();
    }
}
