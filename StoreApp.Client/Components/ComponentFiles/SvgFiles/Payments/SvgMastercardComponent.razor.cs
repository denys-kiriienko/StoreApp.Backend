using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.ComponentFiles.SvgFiles.Payments;

public partial class SvgMastercardComponent
{
    [Parameter] public string? Width { get; set; } = "26";
    [Parameter] public string? Height { get; set; } = "16";
}
