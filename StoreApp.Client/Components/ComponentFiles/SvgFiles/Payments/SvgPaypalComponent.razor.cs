using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.ComponentFiles.SvgFiles.Payments;

public partial class SvgPaypalComponent
{
    [Parameter] public string? Width { get; set; } = "35";
    [Parameter] public string? Height { get; set; } = "10";
}
