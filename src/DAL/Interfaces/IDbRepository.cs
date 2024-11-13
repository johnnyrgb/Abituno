using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IDbRepository
    {
        IEntityRepository<Habit> Habit { get; }
        IEntityRepository<HabitRecord> HabitRecord { get; }
    }
}
