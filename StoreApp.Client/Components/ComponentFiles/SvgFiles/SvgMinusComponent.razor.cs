using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.ComponentFiles.SvgFiles;

public partial class SvgMinusComponent : ComponentBase
{
    [Parameter]
    public string? Width { get; set; } = "20";

    [Parameter]
    public string? Height { get; set; } = "4";
}