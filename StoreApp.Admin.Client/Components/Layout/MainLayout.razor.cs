namespace StoreApp.Admin.Client.Components.Layout;

public partial class MainLayout
{
    private bool sidebarOpen = false;
    
    private void SidebarToggled(bool open)
    {
        sidebarOpen = open;
        StateHasChanged();
    }
}
