using Microsoft.Extensions.Logging;
using BLL.Interfaces;
using BLL.Services;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace Presentation
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.UseMauiApp<App>().Services.AddDbContext<DatabaseContext>(optionsAction =>
                optionsAction.UseSqlite(String.Concat("Data Source = ", Path.Combine(FileSystem.AppDataDirectory, "HabitDB.db"))));
            builder.Services.AddTransient<DatabaseSeed>();

            // Регистрация сервисов
            builder.Services.AddTransient<IHabitService, HabitService>();
            builder.Services.AddTransient<IHabitRecordService, HabitRecordService>();

            // Создание приложения
            var app = builder.Build();

            // Сидирование базы данных
            using (var scope = app.Services.CreateScope())
            {
                var seed = scope.ServiceProvider.GetRequiredService<DatabaseSeed>();
                seed.SeedAsync().Wait();
            }

            // Возвращаем приложение
            return app;
        }
    }
}
