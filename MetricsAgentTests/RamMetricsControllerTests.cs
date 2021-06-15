using MetricsAgent.Controllers;
using MetricsAgent.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsControllerTests
    {
        private RamMetricsController controller;
        private Mock<ILogger<RamMetricsController>> mockLogger;
        private Mock<IRamMetricsRepository> mockRepository;

        public RamMetricsControllerTests()
        {
            mockLogger = new Mock<ILogger<RamMetricsController>>();
            mockRepository = new Mock<IRamMetricsRepository>();
            controller = new RamMetricsController(mockLogger.Object);
        }

        [Fact]
        public void GetMetricst_ReturnOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Act
            var result = controller.GetMetrics(fromTime, toTime);

            //Assert
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
