using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.SvgFiles;

public partial class SvgUserProfileComponent
{
    [Parameter] public string? Width { get; set; } = "22";
    [Parameter] public string? Height { get; set; } = "22";
}
