using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.SvgFiles;

public partial class SvgMenuComponent
{
    [Parameter] public string? Width { get; set; } = "20";
    [Parameter] public string? Height { get; set; } = "16";
}
