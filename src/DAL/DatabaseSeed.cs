using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DatabaseSeed
    {
        private readonly DatabaseContext _context;

        public DatabaseSeed(DatabaseContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (!await _context.Habits.AnyAsync())
            {
                var habits = new[]
                {
                    new Habit { Id = 1, Name = "Гладить кота", StartDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-15), EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(15) },
                    new Habit { Id = 2, Name = "Трогать траву", StartDate =  DateOnly.FromDateTime(DateTime.Now).AddDays(-11), EndDate = new DateOnly(2024, 10, 25).AddDays(19) },
                    new Habit { Id = 3, Name = "Делать зарядку", StartDate =  DateOnly.FromDateTime(DateTime.Now).AddDays(-7), EndDate =  DateOnly.FromDateTime(DateTime.Now).AddDays(23) }
                };
                await _context.Habits.AddRangeAsync(habits);


                var habitRecords = new List<HabitRecord>();

                var startDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-15);
                for (int i = 0; i < 15; i += 2)
                {
                    habitRecords.Add(new HabitRecord { HabitId = 1, RecordDate = startDate.AddDays(i) });
                }

                startDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-11);
                for (int i = 0; i < 15; i += 3)
                {
                    habitRecords.Add(new HabitRecord { HabitId = 2, RecordDate = startDate.AddDays(i) });
                }

                startDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-7);
                for (int i = 0; i < 15; i += 2)
                {
                    habitRecords.Add(new HabitRecord { HabitId = 3, RecordDate = startDate.AddDays(i) });
                }
                await _context.HabitRecords.AddRangeAsync(habitRecords);

                await _context.SaveChangesAsync();
            }
        }
    }
}
