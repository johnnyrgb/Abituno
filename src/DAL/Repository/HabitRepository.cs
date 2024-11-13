using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class HabitRepository : IEntityRepository<Habit>
    {
        private readonly DatabaseContext _context;

        public HabitRepository(DatabaseContext context)
        {
            _context = context;
        }

        // Получение привычки по идентификатору с включением связанных записей HabitRecords
        public async Task<Habit?> GetAsync(int id)
        {
            return await _context.Habits
                .Include(h => h.HabitRecords) // Загружаем связанные записи
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<Habit>?> GetAllAsync()
        {
            return await _context.Habits.Include(h => h.HabitRecords).ToListAsync();
        }

        // Создание новой привычки
        public async Task CreateAsync(Habit entity)
        {
            await _context.Habits.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Обновление привычки
        public async Task UpdateAsync(Habit entity)
        {
            _context.Habits.Update(entity);
            await _context.SaveChangesAsync();
        }

        // Удаление привычки по идентификатору
        public async Task DeleteAsync(int id)
        {
            var habit = await _context.Habits.FindAsync(id);
            if (habit != null)
            {
                _context.Habits.Remove(habit);
                await _context.SaveChangesAsync();
            }
        }
    }
}
