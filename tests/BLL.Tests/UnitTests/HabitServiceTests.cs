using BLL.DTOs;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Moq;

namespace BLL.Tests.UnitTests
{
    public class HabitServiceTests
    {
        #region Private
        private readonly Mock<IDbRepository> _mockRepository;
        private readonly HabitService _habitService;
        #endregion


        public HabitServiceTests()
        {
            _mockRepository = new Mock<IDbRepository>();
            _habitService = new HabitService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetHabitAsync_ReturnHabit_WhenHabitExists()
        {
            // Arrange
            var habitId = 1;
            var habit = new Habit
            {
                Id = habitId,
                Name = "Test Habit",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
            };

            _mockRepository.Setup(repos => repos.Habit.GetAsync(habitId)).ReturnsAsync(habit);

            // Act
            var result = await _habitService.GetHabitAsync(habitId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(habitId, result.Id);
            Assert.Equal(habit.Name, result.Name);
            Assert.Equal(habit.StartDate, result.StartDate);
            Assert.Equal(habit.EndDate, result.EndDate);
        }

        [Fact]
        public async Task GetHabitAsync_ReturnHabit_WhenHabitDoesNotExists()
        {
            // Arrange
            var habitId = 1;
            _mockRepository.Setup(repos => repos.Habit.GetAsync(habitId)).ReturnsAsync((Habit?)null);

            // Act
            var result = await _habitService.GetHabitAsync(habitId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllHabitsAsync_ReturnAllHabits_WhenHabitsExist()
        {
            // Arrange
            var habits = new List<Habit>
            {
                new Habit 
                { 
                    Id = 1, 
                    Name = "Привычка 1",  
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
                },
                new Habit
                {
                    Id = 2,
                    Name = "Привычка 2",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
                },
            };

            _mockRepository.Setup(repos => repos.Habit.GetAllAsync()).ReturnsAsync(habits);

            // Act
            var result = await _habitService.GetAllHabitsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Привычка 1", result.ElementAt(0).Name);
        }

        [Fact]
        public async Task GetAllHabitsAsync_ReturnAllHabits_WhenHabitsDoNotExist()
        {
            // Arrange

            _mockRepository.Setup(repos => repos.Habit.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Habit>);

            // Act
            var result = await _habitService.GetAllHabitsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<HabitDTO>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task CreateAllHabitsAsync_CreatesHabit()
        {
            // Arrange
            var habitDTO = new HabitDTO()
            {
                Id = 1,
                Name = "Новая привычка",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(30),
            };

            _mockRepository.Setup(repos => repos.Habit.CreateAsync(It.IsAny<Habit>())).Returns(Task.CompletedTask);

            // Act
            await _habitService.CreateHabitAsync(habitDTO);

            // Assert
            _mockRepository.Verify(r => r.Habit.CreateAsync(It.Is<Habit>(h =>
                h.Name == habitDTO.Name &&
                h.StartDate == habitDTO.StartDate &&
                h.EndDate == habitDTO.EndDate &&
                h.Id == habitDTO.Id)));
        }

        [Fact]
        public async Task UpdateHabitAsync_UpdatesHabit()
        {
            // Arrange
            var habitDTO = new HabitDTO()
            {
                Id = 1,
                Name = "Обновляемая привычка",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(30),
            };

            _mockRepository.Setup(r => r.Habit.UpdateAsync(It.IsAny<Habit>()))
                .Returns(Task.CompletedTask);

            // Act
            await _habitService.UpdateHabitAsync(habitDTO);

            // Assert
            _mockRepository.Verify(r => r.Habit.UpdateAsync(It.Is<Habit>(h =>
                h.Id == habitDTO.Id &&
                h.Name == habitDTO.Name &&
                h.StartDate == habitDTO.StartDate &&
                h.EndDate == habitDTO.EndDate)));
        }

        [Fact]
        public async Task DeleteHabitAsync_DeletesHabit()
        {
            // Arrange
            var habitId = 1;
            _mockRepository.Setup(r => r.Habit.DeleteAsync(habitId))
                .Returns(Task.CompletedTask);

            // Act
            await _habitService.DeleteHabitAsync(habitId);

            // Assert
            _mockRepository.Verify(r => r.Habit.DeleteAsync(habitId));
        }
    }
}