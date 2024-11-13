using Presentation.ViewModels;

namespace Presentation.Views;
public partial class HabitDashboardPage : ContentPage
{
    public HabitDashboardPage(HabitDashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

    }
}