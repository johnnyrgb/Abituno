using BLL.DTOs;
using BLL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.ViewModels;
using System.Collections.ObjectModel;

public class HabitListViewModel : ObservableObject
{
    private readonly IHabitService _habitService;
    private readonly IHabitRecordService _habitRecordService;

    public ObservableCollection<HabitCardViewModel> Habits { get; private set; } = new ObservableCollection<HabitCardViewModel>();

    public HabitListViewModel(IHabitService habitService, IHabitRecordService habitRecordService)
    {
        _habitService = habitService;
        _habitRecordService = habitRecordService;
        LoadHabitsCommand = new AsyncRelayCommand(LoadHabitsAsync);
    }

    public IAsyncRelayCommand LoadHabitsCommand { get; }

    private async Task LoadHabitsAsync()
    {
        // Загружаем все привычки
        var habits = await _habitService.GetAllHabitsAsync();

        // Очищаем предыдущие данные, если они были
        Habits.Clear();

        foreach (var habit in habits)
        {
            // Загружаем записи за последние 7 дней
            var endDate = DateOnly.FromDateTime(DateTime.Now);
            var startDate = endDate.AddDays(-6); // 7 дней включая текущий
            var habitRecords = await _habitRecordService.GetLastHabitRecordsInDateRangeAsync(habit.Id, startDate, endDate);

            // Создаем 7 дней с отметками
            var last7DaysRecords = new ObservableCollection<HabitRecordViewModel>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Проверяем, есть ли запись на эту дату
                var record = habitRecords.FirstOrDefault(r => r.RecordDate == date);
                var habitRecordViewModel = new HabitRecordViewModel(_habitRecordService)
                {
                    Id = record?.Id ?? -1,  // Устанавливаем Id записи, если она существует
                    HabitId = habit.Id,
                    RecordDate = date,
                    IsCompleted = record != null
                };
                last7DaysRecords.Add(habitRecordViewModel);
            }

            // Создаем ViewModel для HabitCard с необходимыми данными
            var habitCardViewModel = new HabitCardViewModel(habit.Id, habit.Name, last7DaysRecords);

            Habits.Add(habitCardViewModel);
        }
    }
}

