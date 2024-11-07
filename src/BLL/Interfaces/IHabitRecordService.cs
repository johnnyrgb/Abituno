using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IHabitRecordService
    {
        Task<HabitRecordDTO?> GetHabitRecordByIdAsync(int id);
        Task<IEnumerable<HabitRecordDTO>> GetAllHabitRecordsByHabitIdAsync(int habitId);
        Task<IEnumerable<HabitRecordDTO>> GetLastHabitRecordsInRangeAsync(int habitId, int count);
        Task<IEnumerable<HabitRecordDTO>> GetLastHabitRecordsInDateRangeAsync(int habitId, DateOnly startDate, DateOnly endDate);
        Task<int> CreateHabitRecordAsync(HabitRecordDTO habitRecordDto);
        Task UpdateHabitRecordAsync(HabitRecordDTO habitRecordDto);
        Task DeleteHabitRecordAsync(int id);
    }
}