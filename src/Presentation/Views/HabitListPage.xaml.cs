namespace Presentation.Views;

public partial class HabitListPage : ContentPage
{
    public HabitListPage(HabitListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        viewModel.LoadHabitsCommand.Execute(null);
    }
}