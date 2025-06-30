using Microsoft.AspNetCore.Components;

namespace StoreApp.Admin.Client.Components
{
    public partial class Sidebar : ComponentBase
    {
        [Parameter] public EventCallback<bool> OnSidebarToggle { get; set; }
        [Parameter] public bool StartOpen { get; set; } = false;

        protected bool isOpen = false;
        protected bool dropdownOpen = false;

        protected override void OnInitialized()
        {
            isOpen = StartOpen;
        }

        protected async Task Toggle()
        {
            isOpen = !isOpen;
            await OnSidebarToggle.InvokeAsync(isOpen);
        }

        protected void ToggleDropdown()
        {
            dropdownOpen = !dropdownOpen;

            if (!isOpen)
                isOpen = true;
        }
    }
}
