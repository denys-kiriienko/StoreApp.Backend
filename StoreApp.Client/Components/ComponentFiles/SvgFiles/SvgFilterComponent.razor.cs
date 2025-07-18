using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.ComponentFiles.SvgFiles;

public partial class SvgFilterComponent : ComponentBase
{
    [Parameter] public string? Width { get; set; } = "22";
    [Parameter] public string? Height { get; set; } = "20";
}