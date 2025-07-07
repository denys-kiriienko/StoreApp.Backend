using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace StoreApp.Admin.Client.Components.ComponentsFiles.InputFile;

public partial class InputFileComponent
{
    [Parameter] public string ButtonText { get; set; } = "Upload File";
    [Parameter] public string? Width { get; set; }
    [Parameter] public EventCallback<InputFileChangeEventArgs> OnFileSelectedCallback { get; set; }

    private string? FileName;

    private async Task OnFileSelected(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file is not null)
        {
            FileName = file.Name;
        }

        await OnFileSelectedCallback.InvokeAsync(e);
    }
}
