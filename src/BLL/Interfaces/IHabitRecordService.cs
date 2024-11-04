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
        Task CreateHabitRecordAsync(HabitRecordDTO habitRecordDto);
        Task UpdateHabitRecordAsync(HabitRecordDTO habitRecordDto);
        Task DeleteHabitRecordAsync(int id);
    }
}
