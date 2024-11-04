using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class HabitRecordRepository : IEntityRepository<HabitRecord>
    {
        private readonly DatabaseContext _context;

        public HabitRecordRepository(DatabaseContext context)
        {
            _context = context;
        }

        // Получение записи о привычке по идентификатору
        public async Task<HabitRecord?> GetAsync(int id)
        {
            return await _context.HabitRecords
                .FirstOrDefaultAsync(hr => hr.Id == id);
        }

        public async Task<IEnumerable<HabitRecord>?> GetAllAsync()
        {
            return await _context.HabitRecords.ToListAsync();
        }

        // Создание новой записи о привычке
        public async Task CreateAsync(HabitRecord entity)
        {
            await _context.HabitRecords.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Обновление записи о привычке
        public async Task UpdateAsync(HabitRecord entity)
        {
            _context.HabitRecords.Update(entity);
            await _context.SaveChangesAsync();
        }

        // Удаление записи о привычке по идентификатору
        public async Task DeleteAsync(int id)
        {
            var habitRecord = await _context.HabitRecords.FindAsync(id);
            if (habitRecord != null)
            {
                _context.HabitRecords.Remove(habitRecord);
                await _context.SaveChangesAsync();
            }
        }
    }
}
