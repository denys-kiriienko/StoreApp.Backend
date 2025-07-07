using Microsoft.AspNetCore.Components;

namespace StoreApp.Admin.Client.Components.ComponentsFiles.LoadingSpinner;

public partial class LoadingSpinnerComponent
{
    [Parameter] public string? Style { get; set; }
    [Parameter] public string? Width { get; set; }
    [Parameter] public string? Height { get; set; }
    [Parameter] public string? Color { get; set; }
    [Parameter] public string? LoadingText { get; set; }

    protected override void OnInitialized()
    {
        BuildStyle();
    }

    private string BuildStyle()
    {
        var style = new List<string>();

        if (!string.IsNullOrWhiteSpace(Width))
            style.Add($"--spinner-width: {Width}");

        if (!string.IsNullOrWhiteSpace(Height))
            style.Add($"--spinner-height: {Height}");

        if (!string.IsNullOrWhiteSpace(Color))
            style.Add($"--spinner-color: {Color}");

        if (!string.IsNullOrWhiteSpace(Style))
            style.Add(Style);

        return string.Join("; ", style);
    }
}
