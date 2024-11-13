using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class HabitService : IHabitService
    {
        private readonly IDbRepository _repository;

        public HabitService(IDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<HabitDTO?> GetHabitAsync(int id)
        {
            var habit = await _repository.Habit.GetAsync(id);
            return habit != null ? MapToDTO(habit) : null;
        }

        public async Task<IEnumerable<HabitDTO>> GetAllHabitsAsync()
        {
            var habits = await _repository.Habit.GetAllAsync();
            return (habits ?? Enumerable.Empty<Habit>()).Select(MapToDTO);
        }

        public async Task CreateHabitAsync(HabitDTO habitDto)
        {
            var habit = MapToEntity(habitDto);
            await _repository.Habit.CreateAsync(habit);
        }

        public async Task UpdateHabitAsync(HabitDTO habitDto)
        {
            var habit = MapToEntity(habitDto);
            await _repository.Habit.UpdateAsync(habit);
        }

        public async Task DeleteHabitAsync(int id)
        {
            await _repository.Habit.DeleteAsync(id);
        }

        // Маппинг из сущности в DTO
        private HabitDTO MapToDTO(Habit habit)
        {
            return new HabitDTO
            {
                Id = habit.Id,
                Name = habit.Name ?? string.Empty,
                StartDate = habit.StartDate,
                EndDate = habit.EndDate,
            };
        }

        // Маппинг из DTO в сущность
        private Habit MapToEntity(HabitDTO habitDto)
        {
            return new Habit
            {
                Id = habitDto.Id,
                Name = habitDto.Name,
                StartDate = habitDto.StartDate,
                EndDate = habitDto.EndDate
            };
        }
    }
}
