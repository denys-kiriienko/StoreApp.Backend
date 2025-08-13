using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.ComponentFiles.SvgFiles;

public partial class SvgTrashComponent
{
    [Parameter] public string? Width { get; set; } = "18";
    [Parameter] public string? Height { get; set; } = "20";
}
