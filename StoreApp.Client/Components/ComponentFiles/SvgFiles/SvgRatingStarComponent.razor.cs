using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.ComponentFiles.SvgFiles;

public partial class SvgRatingStarComponent
{
    [Parameter] public string? Width { get; set; } = "19";
    [Parameter] public string? Height { get; set; } = "17";
}
