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
    public class HddMetricsControllerTests
    {
        private HddMetricsController controller;
        private Mock<ILogger<HddMetricsController>> mockLogger;
        private Mock<IHddMetricsRepository> mockRepository;

        public HddMetricsControllerTests()
        {
            mockLogger = new Mock<ILogger<HddMetricsController>>();
            mockRepository = new Mock<IHddMetricsRepository>();
            controller = new HddMetricsController(mockLogger.Object, mockRepository.Object);
        }

        [Fact]
        public void GetMetricst_ReturnOk()
        {
            //Arrange
            mockRepository.Setup(repository =>
                repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<HddMetrics>()).Verifiable();

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