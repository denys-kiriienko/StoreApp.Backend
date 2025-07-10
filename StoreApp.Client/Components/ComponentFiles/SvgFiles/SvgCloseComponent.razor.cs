using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.ComponentFiles.SvgFiles;

public partial class SvgCloseComponent
{
    [Parameter] public string? Width { get; set; } = "14";
    [Parameter] public string? Height { get; set; } = "14";
}
