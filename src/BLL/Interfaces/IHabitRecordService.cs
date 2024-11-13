using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IHabitRecordService
    {
        Task<HabitRecordDTO?> GetHabitRecordByIdAsync(int id);
        Task<IEnumerable<HabitRecordDTO>> GetAllHabitRecordsByHabitIdAsync(int habitId);
        Task<IEnumerable<HabitRecordDTO>> GetLastHabitRecordsInRangeAsync(int habitId, int count);
        Task<IEnumerable<HabitRecordDTO>> GetLastHabitRecordsInDateRangeAsync(int habitId, DateOnly startDate, DateOnly endDate);
        Task<int> GetCurrentStreak(int habitId);
        Task<int> GetMaxStreak(int habitId);
        Task<int> GetCount(int habitId);
        Task<int> CreateHabitRecordAsync(HabitRecordDTO habitRecordDto);
        Task UpdateHabitRecordAsync(HabitRecordDTO habitRecordDto);
        Task DeleteHabitRecordAsync(int id);
    }
}