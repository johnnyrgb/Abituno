using System.IO;
using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.ViewModels
{
    internal partial class SettingsViewModel : ObservableObject
    {
        private const string DatabaseFilePath = "HabitDB.db";
        private string _databaseSize;

        public string DatabaseSize
        {
            get => _databaseSize;
            set => SetProperty(ref _databaseSize, value);
        }

        public SettingsViewModel()
        {
            LoadDatabaseSize();
        }

        private void LoadDatabaseSize()
        {
            try
            {
                // Получаем полный путь к файлу базы данных
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, DatabaseFilePath);

                // Проверяем, существует ли файл
                if (File.Exists(dbPath))
                {
                    // Получаем размер файла в байтах
                    var fileInfo = new FileInfo(dbPath);
                    var sizeInKb = fileInfo.Length / 1024.0; // Размер в КБ

                    // Форматируем строку для отображения
                    DatabaseSize = $"Размер базы данных: {sizeInKb:F2} КБ";
                }
                else
                {
                    DatabaseSize = "База данных не найдена.";
                }
            }
            catch (Exception ex)
            {
                DatabaseSize = $"Ошибка: {ex.Message}";
            }
        }
    }
}
