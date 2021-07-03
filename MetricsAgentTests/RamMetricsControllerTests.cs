using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsControllerTests
    {
        private RamMetricsController controller;
        private Mock<ILogger<RamMetricsController>> mockLogger;
        private Mock<IRamMetricsRepository> mockRepository;
        private Mock<IMapper> mockMapper;

        public RamMetricsControllerTests()
        {
            mockLogger = new Mock<ILogger<RamMetricsController>>();
            mockRepository = new Mock<IRamMetricsRepository>();
            mockMapper = new Mock<IMapper>();
            controller = new RamMetricsController(mockLogger.Object, mockRepository.Object, mockMapper.Object);
        }

        [Fact]
        public void GetMetricst_ReturnOk()
        {
            //Arrange
            mockRepository.Setup(repository =>
                repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<RamMetrics>()).Verifiable();

            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Act
            var result = controller.GetMetrics(fromTime, toTime);

            //Assert
            mockRepository.Verify(repository =>
                repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }
}
