using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Moq;

namespace BLL.Tests.UnitTests
{
    public class HabitRecordServiceTests
    {
        #region Private
        private readonly Mock<IDbRepository> _mockRepository;
        private readonly HabitRecordService _habitRecordService;
        #endregion

        public HabitRecordServiceTests()
        {
            _mockRepository = new Mock<IDbRepository>();
            _habitRecordService = new HabitRecordService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetLastHabitRecordsInDateRangeAsync_ReturnTwoHabitRecords_WhenThreeExist()
        {
            // Arrange
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var endDate = startDate.AddDays(7);

            var records = new List<HabitRecord>()
            {
                new HabitRecord
                {
                    Id = 1,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now)
                },
                new HabitRecord
                {
                    Id = 2,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(5)
                },
                new HabitRecord
                {
                    Id = 3,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(10)
                },
            };

            _mockRepository.Setup(repo => repo.HabitRecord.GetAllAsync()).ReturnsAsync(records);
            
            // Act
            var result = (await _habitRecordService.GetLastHabitRecordsInDateRangeAsync(1, startDate, endDate)).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            Assert.Equal(1, result[0].Id);
            Assert.Equal(1, result[0].HabitId);
            Assert.Equal(startDate, result[0].RecordDate);

            Assert.Equal(2, result[1].Id);
            Assert.Equal(1, result[1].HabitId);
            Assert.Equal(startDate.AddDays(5), result[1].RecordDate);
        }

        [Fact]
        public async Task GetLasHabitRecordsInRangeAsync_ReturnFiveRecords_WhenSevenExist()
        {
            // Arrange
            var habitId = 1;
            var count = 5;

            var records = new List<HabitRecord>()
            {
                new HabitRecord
                {
                    Id = 1,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now)
                },
                new HabitRecord
                {
                    Id = 2,
                    HabitId = 2,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(5)
                },
                new HabitRecord
                {
                    Id = 3,
                    HabitId = 3,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(6)
                },
                new HabitRecord
                {
                    Id = 4,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(7)
                },
                new HabitRecord
                {
                    Id = 5,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(8)
                },
                new HabitRecord
                {
                    Id = 6,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(9)
                },
                new HabitRecord
                {
                    Id = 7,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(10)
                },
            };

            _mockRepository.Setup(repo => repo.HabitRecord.GetAllAsync()).ReturnsAsync(records);

            // Act
            var result = (await _habitRecordService.GetLastHabitRecordsInRangeAsync(habitId, count)).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task GetCurrentStreak_ReturnThreeStreak_WhenSevenExist_DateIsLowerThanToday()
        {
            // Arrange
            var habitId = 1;
            var records = new List<HabitRecord>()
            {
                new HabitRecord
                {
                    Id = 1,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now)
                },
                new HabitRecord
                {
                    Id = 2,
                    HabitId = 2,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-5)
                },
                new HabitRecord
                {
                    Id = 3,
                    HabitId = 3,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-6)
                },
                new HabitRecord
                {
                    Id = 4,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-1)
                },
                new HabitRecord
                {
                    Id = 5,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-8)
                },
                new HabitRecord
                {
                    Id = 6,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-2)
                },
                new HabitRecord
                {
                    Id = 7,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-10)
                },
            };

            _mockRepository.Setup(repo => repo.HabitRecord.GetAllAsync()).ReturnsAsync(records);

            // Aсе
            var result = await _habitRecordService.GetCurrentStreak(habitId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task GetCurrentStreak_ReturnZeroStreak_WhenSevenExist_DateIsLowerThanToday()
        {
            // Arrange
            var habitId = 4; // Не существует
            var records = new List<HabitRecord>()
            {
                new HabitRecord
                {
                    Id = 1,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now)
                },
                new HabitRecord
                {
                    Id = 2,
                    HabitId = 2,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-5)
                },
                new HabitRecord
                {
                    Id = 3,
                    HabitId = 3,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-6)
                },
                new HabitRecord
                {
                    Id = 4,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-1)
                },
                new HabitRecord
                {
                    Id = 5,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-8)
                },
                new HabitRecord
                {
                    Id = 6,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-2)
                },
                new HabitRecord
                {
                    Id = 7,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-10)
                },
            };

            _mockRepository.Setup(repo => repo.HabitRecord.GetAllAsync()).ReturnsAsync(records);

            // Aсе
            var result = await _habitRecordService.GetCurrentStreak(habitId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetCurrentStreak_ReturnZeroStreak_WhenZeroExists()
        {
            // Arrange
            var habitId = 1;
            var records = new List<HabitRecord>()
            {
               
            };

            _mockRepository.Setup(repo => repo.HabitRecord.GetAllAsync()).ReturnsAsync(records);

            // Aсе
            var result = await _habitRecordService.GetCurrentStreak(habitId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetCurrentStreak_ReturnThreeStreak_WhenSevenExist_DateIsHigherThanToday()
        {
            // Arrange
            var habitId = 1;
            var records = new List<HabitRecord>()
            {
                new HabitRecord
                {
                    Id = 1,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now)
                },
                new HabitRecord
                {
                    Id = 2,
                    HabitId = 2,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(5)
                },
                new HabitRecord
                {
                    Id = 3,
                    HabitId = 3,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(6)
                },
                new HabitRecord
                {
                    Id = 4,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(1)
                },
                new HabitRecord
                {
                    Id = 5,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(8)
                },
                new HabitRecord
                {
                    Id = 6,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(2)
                },
                new HabitRecord
                {
                    Id = 7,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(10)
                },
            };

            _mockRepository.Setup(repo => repo.HabitRecord.GetAllAsync()).ReturnsAsync(records);

            // Aсе
            var result = await _habitRecordService.GetCurrentStreak(habitId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result);
        }

        [Fact]
        public async Task GetMaxStreak_ReturnThreeStreak_WhenEightExist_DateIsLowerThanToday()
        {
            // Arrange
            var habitId = 1;
            var records = new List<HabitRecord>()
            {
                new HabitRecord
                {
                    Id = 1,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now)
                },
                new HabitRecord
                {
                    Id = 2,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-1)
                },
                new HabitRecord
                {
                    Id = 3,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-5)
                },
                new HabitRecord
                {
                    Id = 4,
                    HabitId = 2,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-12)
                },
                new HabitRecord
                {
                    Id = 5,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-8) // +
                },
                new HabitRecord
                {
                    Id = 6,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-9) // +
                },

                new HabitRecord
                {
                    Id = 6,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-110)
                },
                new HabitRecord
                {
                    Id = 7,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-10) // +
                },
            };

            _mockRepository.Setup(repo => repo.HabitRecord.GetAllAsync()).ReturnsAsync(records);

            // Aсе
            var result = await _habitRecordService.GetMaxStreak(habitId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task GetMaxStreak_ReturnThreeStreak_WhenEightExist_DateIsHigherThanToday()
        {
            // Arrange
            var habitId = 1;
            var records = new List<HabitRecord>()
            {
                new HabitRecord
                {
                    Id = 1,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now)
                },
                new HabitRecord
                {
                    Id = 2,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(1)
                },
                new HabitRecord
                {
                    Id = 3,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(5)
                },
                new HabitRecord
                {
                    Id = 4,
                    HabitId = 2,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(12)
                },
                new HabitRecord
                {
                    Id = 5,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(8) // +
                },
                new HabitRecord
                {
                    Id = 6,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(9) // +
                },

                new HabitRecord
                {
                    Id = 6,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(110)
                },
                new HabitRecord
                {
                    Id = 7,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(10) // +
                },
            };

            _mockRepository.Setup(repo => repo.HabitRecord.GetAllAsync()).ReturnsAsync(records);

            // Aсе
            var result = await _habitRecordService.GetMaxStreak(habitId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task GetMaxStreak_ReturnFourStreak_WhenEightExist_DateIsHigherAndLowerThanToday_AndIncludesIt()
        {
            // Arrange
            var habitId = 1;
            var records = new List<HabitRecord>()
            {
                new HabitRecord
                {
                    Id = 1,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now) // +
                },
                new HabitRecord
                {
                    Id = 2,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-1) // +
                },
                new HabitRecord
                {
                    Id = 3,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(1) // +
                },
                new HabitRecord
                {
                    Id = 4,
                    HabitId = 2,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-12)
                },
                new HabitRecord
                {
                    Id = 5,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(8) 
                },
                new HabitRecord
                {
                    Id = 6,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-9)
                },

                new HabitRecord
                {
                    Id = 6,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(2) // +
                },
                new HabitRecord
                {
                    Id = 7,
                    HabitId = 1,
                    RecordDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-10) 
                },
            };

            _mockRepository.Setup(repo => repo.HabitRecord.GetAllAsync()).ReturnsAsync(records);

            // Aсе
            var result = await _habitRecordService.GetMaxStreak(habitId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(4, result);
        }
    }
}
