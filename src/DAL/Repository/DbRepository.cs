using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repository
{
    public class DbRepository : IDbRepository, IDisposable
    {
        private readonly DatabaseContext _context;
        private IEntityRepository<Habit>? _habitRepository;
        private IEntityRepository<HabitRecord>? _habitRecordRepository;

        public DbRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEntityRepository<Habit> Habit
        {
            get
            {
                if (_habitRepository == null)
                {
                    _habitRepository = new HabitRepository(_context);
                }
                return _habitRepository;
            }
        }

        public IEntityRepository<HabitRecord> HabitRecord
        {
            get
            {
                if (_habitRecordRepository == null)
                {
                    _habitRecordRepository = new HabitRecordRepository(_context);
                }
                return _habitRecordRepository;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
