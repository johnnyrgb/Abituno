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
        public int HabitId
        {
            get => _habitId;
            set => SetProperty(ref _habitId, value);
        }

        // Метод для обработки параметров навигации
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("HabitId") && query["HabitId"] is int habitId)
            {
                HabitId = habitId;  // Устанавливаем значение HabitId
            }
        }

        // Другие свойства и команды вашей ViewModel
    }
}
