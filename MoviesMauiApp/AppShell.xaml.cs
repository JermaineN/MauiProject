namespace MoviesMauiApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.DetailsPage), typeof(Views.DetailsPage));
            Routing.RegisterRoute(nameof(Views.StartupPage), typeof(Views.StartupPage));
        }
    }
}
