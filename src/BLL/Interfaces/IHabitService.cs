using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IHabitService
    {
        Task<HabitDTO?> GetHabitAsync(int id);
        Task<IEnumerable<HabitDTO>> GetAllHabitsAsync();
        Task CreateHabitAsync(HabitDTO habitDto);
        Task UpdateHabitAsync(HabitDTO habitDto);
        Task DeleteHabitAsync(int id);
    }
}
