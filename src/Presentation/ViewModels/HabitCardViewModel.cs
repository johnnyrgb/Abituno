using BLL.DTOs;
using BLL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Presentation.ViewModels
{
    public class HabitCardViewModel : ObservableObject
    {
        public int HabitId { get; set; }
        public string HabitName { get; set; }
        public ObservableCollection<HabitRecordViewModel> Records { get; set; } = new ObservableCollection<HabitRecordViewModel>();
        public HabitCardViewModel(int habitId, string habitName, ObservableCollection<HabitRecordViewModel> records)
        {
            HabitId = habitId;
            HabitName = habitName;
            Records = records;
            NavigateToHabitDashboardCommand = new AsyncRelayCommand(NavigateToHabitDashboardAsync);
        }
        public IAsyncRelayCommand NavigateToHabitDashboardCommand { get; }
        private async Task NavigateToHabitDashboardAsync()
        {
            // Переход на HabitDashboardPage с передачей HabitId
            var navigationParameter = new Dictionary<string, object>
            {
                 { "HabitId", HabitId }
            };

            await Shell.Current.GoToAsync($"dashboard", navigationParameter);

        }
    }

    public class HabitRecordViewModel : ObservableObject
    {
        private readonly IHabitRecordService _habitRecordService;

        public HabitRecordViewModel(IHabitRecordService habitRecordService)
        {
            _habitRecordService = habitRecordService;
            SetCompletionCommand = new AsyncRelayCommand(ToggleCompletionAsync);
        }

        public int Id { get; set; }
        public int HabitId { get; set; }  // ID привычки
        public DateOnly RecordDate { get; set; }
        public bool IsCompleted { get; set; }

        public IAsyncRelayCommand SetCompletionCommand { get; }

        private async Task ToggleCompletionAsync()
        {
            if (IsCompleted)
            {
                await _habitRecordService.DeleteHabitRecordAsync(Id);
                IsCompleted = false;
                Id = -1;
            }
            else
            {
                var newRecordId = await _habitRecordService.CreateHabitRecordAsync(new HabitRecordDTO { HabitId = HabitId, RecordDate = RecordDate });
                IsCompleted = true;
                Id = newRecordId;
            }

            OnPropertyChanged(nameof(IsCompleted));
        }
    }
}
