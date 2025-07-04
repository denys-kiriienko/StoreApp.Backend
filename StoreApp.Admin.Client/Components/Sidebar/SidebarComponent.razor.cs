using Microsoft.AspNetCore.Components;

namespace StoreApp.Admin.Client.Components.Sidebar;

public partial class SidebarComponent : ComponentBase
{
    [Parameter] public EventCallback<bool> OnSidebarToggle { get; set; }
    [Parameter] public bool StartOpen { get; set; }

    protected bool isOpen;
    protected bool dropdownOpen;

    protected override void OnInitialized()
    {
        isOpen = StartOpen;
    }

    protected async Task Toggle()
    {
        isOpen = !isOpen;
        await OnSidebarToggle.InvokeAsync(isOpen);
    }

    protected Task ToggleDropdown()
    {
        if (!isOpen)
        {
            isOpen = true;
            dropdownOpen = true;
            return OnSidebarToggle.InvokeAsync(isOpen);
        }
        else
        {
            dropdownOpen = !dropdownOpen;
            return Task.CompletedTask;
        }
    }
}
