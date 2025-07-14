using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.ComponentFiles.Inputs.BasicInput;

public partial class BasicInputComponent
{
    [Parameter] public string? Width { get; set; } = "100%";
    [Parameter] public string? Height { get; set; } = "100%";
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public string FontSize { get; set; } = "16px";
    [Parameter] public string? BackgroundColor { get; set; } = "#FFFFFF";
    [Parameter] public RenderFragment? Icon { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string IconOpacity { get; set; } = "1.0";


    private string GetWrapperStyle()
    {
        return $"width: {Width}; height: {Height}; background-color: {BackgroundColor};";
    }

    private string GetInputStyle()
    {
        return $"font-size: {FontSize};";
    }
}
