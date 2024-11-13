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

        public async Task<int> GetCurrentStreak(int habitId)
        {
            // Получаем все записи выполнения привычки для нужного habitId
            var habitRecords = (await _repository.HabitRecord.GetAllAsync())
                               .Where(record => record.HabitId == habitId)
                               .OrderByDescending(record => record.RecordDate)
                               .ToList();

            int currentStreak = 0;
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            // Проверка на выполнение привычки сегодня
                if (habitRecords.Any(record => record.RecordDate == today))
                {
                    currentStreak++; // Увеличиваем счетчик за сегодняшнее выполнение
                }

            // Начинаем с вчерашнего дня для проверки последовательности
            DateOnly? previousDate = today.AddDays(-1);

            // Основной цикл для проверки последовательности выполнения до вчерашнего дня включительно
            foreach (var record in habitRecords)
            {
                if (record.RecordDate == previousDate)
                {
                    // Если запись выполнена на предыдущий день последовательности
                    currentStreak++;
                    previousDate = previousDate.Value.AddDays(-1); // Переходим к следующему дню (назад)
                }
                else if (record.RecordDate < previousDate)
                {
                    // Если запись выполнена на более ранний день, последовательность прерывается
                    break;
                }
            }

            return currentStreak;
        }


        public async Task<int> GetMaxStreak(int habitId)
        {
            // Получаем все записи выполнения привычки для заданного habitId
            var habitRecords = (await _repository.HabitRecord.GetAllAsync())
                                                .Where(record => record.HabitId == habitId)
                                                .OrderBy(record => record.RecordDate);

            int maxStreak = 0;
            int currentStreak = 0;
            DateOnly? previousDate = null;

            // Проходим по отсортированным записям
            foreach (var record in habitRecords)
            {
                if (previousDate.HasValue && record.RecordDate == previousDate.Value.AddDays(1))
                {
                    currentStreak++;
                }
                else
                {
                    maxStreak = Math.Max(maxStreak, currentStreak);
                    currentStreak = 1; // Начинаем новую последовательность
                }

                previousDate = record.RecordDate;
            }

            maxStreak = Math.Max(maxStreak, currentStreak);

            return maxStreak;
        }
        public async Task<int> GetCount(int habitId)
        {
            var habitRecords = await _repository.HabitRecord.GetAllAsync();
            return (habitRecords ?? Enumerable.Empty<HabitRecord>())
                .Where(hr => hr.HabitId == habitId).Count();
        }

        public async Task<int> CreateHabitRecordAsync(HabitRecordDTO habitRecordDto)
        {
            var habitRecord = MapToEntity(habitRecordDto);
            await _repository.HabitRecord.CreateAsync(habitRecord);
            return habitRecord.Id;
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
