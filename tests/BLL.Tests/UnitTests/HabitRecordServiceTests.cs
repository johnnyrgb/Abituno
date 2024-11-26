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
    }
}
