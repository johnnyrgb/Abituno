using BLL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL.Entities;
using Presentation.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels
{
    public partial class HabitDashboardViewModel : ObservableObject, IQueryAttributable
    {
        private int _habitId;
        private IHabitService _habitService;
        private IHabitRecordService _habitRecordService;
        public int HabitId
        {
            get => _habitId;
            set => SetProperty(ref _habitId, value);
        }

        public string HabitName { get; set; }

        public int CurrentStreak { get; set; }
        public int MaxStreak {  get; set; }
        public DateOnly StartDate { get; set; }
        public int RecordCount { get; set; }
        public HabitDashboardViewModel(IHabitRecordService habitRecordService, IHabitService habitService)
        {
            _habitRecordService = habitRecordService;
            _habitService = habitService;
            LoadHabitCommand = new AsyncRelayCommand(LoadHabitAsync);
        }

        public IAsyncRelayCommand LoadHabitCommand { get; }
        private async Task LoadHabitAsync()
        {
            var habit = await _habitService.GetHabitAsync(HabitId);
            if (habit == null) return;

            HabitName = habit.Name;

           CurrentStreak = await _habitRecordService.GetCurrentStreak(HabitId);
           MaxStreak = await _habitRecordService.GetMaxStreak(HabitId);
            StartDate = habit.StartDate;
           RecordCount = await _habitRecordService.GetCount(HabitId);

            //Обновляем привязанные свойства
            OnPropertyChanged(nameof(HabitName));
           OnPropertyChanged(nameof(CurrentStreak));
           OnPropertyChanged(nameof(MaxStreak));
            OnPropertyChanged(nameof(StartDate));
           OnPropertyChanged(nameof(RecordCount));

        }

        // Метод для обработки параметров навигации
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("HabitId") && query["HabitId"] is int habitId)
            {
                HabitId = habitId;  // Устанавливаем значение HabitId
            }
            this.LoadHabitCommand.Execute(null);
        }
    }
}
