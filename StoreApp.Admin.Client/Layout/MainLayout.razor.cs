namespace StoreApp.Admin.Client.Layout
{
    public partial class MainLayout
    {
        private bool sidebarOpen = false;
        
        private void SidebarToggled(bool open)
        {
            sidebarOpen = open;
            StateHasChanged();
        }
    }
}
