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
            Assert.IsAssignableFrom<IEnumerable<Habit>>(result);
            Assert.Empty(result);
        }
    }
}