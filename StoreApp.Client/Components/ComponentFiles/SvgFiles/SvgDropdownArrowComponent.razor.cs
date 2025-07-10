using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.ComponentFiles.SvgFiles;

public partial class SvgDropdownArrowComponent
{
    [Parameter] public string? Width { get; set; } = "16";
    [Parameter] public string? Height { get; set; } = "16";
}
