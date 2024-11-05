using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class HabitRecordService : IHabitRecordService
    {
        private readonly IDbRepository _repository;

        public HabitRecordService(IDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<HabitRecordDTO?> GetHabitRecordByIdAsync(int id)
        {
            var habitRecord = await _repository.HabitRecord.GetAsync(id);
            return habitRecord == null ? null : MapToDTO(habitRecord);
        }

        public async Task<IEnumerable<HabitRecordDTO>> GetAllHabitRecordsByHabitIdAsync(int habitId)
        {
            var habitRecords = await _repository.HabitRecord.GetAllAsync();
            return (habitRecords ?? Enumerable.Empty<HabitRecord>())
                .Where(hr => hr.HabitId == habitId)
                .Select(MapToDTO);
        }

        public async Task<IEnumerable<HabitRecordDTO>> GetLastHabitRecordsInDateRangeAsync(int habitId, DateOnly startDate, DateOnly endDate)
        {
            var habitRecords = await _repository.HabitRecord.GetAllAsync();
            return (habitRecords ?? Enumerable.Empty<HabitRecord>())
                .Where(hr => hr.HabitId == habitId && hr.RecordDate >= startDate && hr.RecordDate <= endDate)
                .Select(MapToDTO);
        }

        public async Task<IEnumerable<HabitRecordDTO>> GetLastHabitRecordsInRangeAsync(int habitId, int count)
        {
            var habitRecords = await _repository.HabitRecord.GetAllAsync();
            return (habitRecords ?? Enumerable.Empty<HabitRecord>())
                .Where(hr => hr.HabitId == habitId).TakeLast(count)
                .Select(MapToDTO);
        }


        public async Task CreateHabitRecordAsync(HabitRecordDTO habitRecordDto)
        {
            var habitRecord = MapToEntity(habitRecordDto);
            await _repository.HabitRecord.CreateAsync(habitRecord);
        }

        public async Task UpdateHabitRecordAsync(HabitRecordDTO habitRecordDto)
        {
            var habitRecord = MapToEntity(habitRecordDto);
            await _repository.HabitRecord.UpdateAsync(habitRecord);
        }

        public async Task DeleteHabitRecordAsync(int id)
        {
            await _repository.HabitRecord.DeleteAsync(id);
        }

        private HabitRecordDTO MapToDTO(HabitRecord habitRecord)
        {
            return new HabitRecordDTO
            {
                Id = habitRecord.Id,
                HabitId = habitRecord.HabitId,
                RecordDate = habitRecord.RecordDate
            };
        }

        private HabitRecord MapToEntity(HabitRecordDTO habitRecordDto)
        {
            return new HabitRecord
            {
                Id = habitRecordDto.Id,
                HabitId = habitRecordDto.HabitId,
                RecordDate = habitRecordDto.RecordDate
            };
        }
    }

}
