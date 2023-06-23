namespace AdviceCompliance.WebApplication.Shared
{
    public partial class MainLayout
    {

        bool _drawerOpen = true;
        void ToggleDrawer()
        {
            _drawerOpen = !_drawerOpen;
        }
    }
}
