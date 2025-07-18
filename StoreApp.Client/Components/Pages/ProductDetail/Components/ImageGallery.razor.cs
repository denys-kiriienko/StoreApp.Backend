using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.Pages.ProductDetail.Components;

public partial class ImageGallery : ComponentBase
{
    [Parameter]
    public List<string> Images { get; set; } = [];

    private int SelectedIndex { get; set; } = 0;

    private void SelectImage(int index)
    {
        SelectedIndex = index;
    }
}