using Presentation.Views;

namespace Presentation
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("dashboard", typeof(HabitDashboardPage));
            Routing.RegisterRoute("habitlist", typeof(HabitListPage));
        }
    }
}
