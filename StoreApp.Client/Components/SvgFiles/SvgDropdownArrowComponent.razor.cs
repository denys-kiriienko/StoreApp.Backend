using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.SvgFiles;

public partial class SvgDropdownArrowComponent
{
    [Parameter] public string? Width { get; set; } = "16";
    [Parameter] public string? Height { get; set; } = "16";
}
