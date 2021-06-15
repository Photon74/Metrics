using MetricsAgent.Controllers;
using MetricsAgent.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetMetricsControllerTests
    {
        private DotNetMetricsController controller;
        private Mock<ILogger<DotNetMetricsController>> mockLogger;
        private Mock<IDotNetMetricsRepository> mockRepository;

        public DotNetMetricsControllerTests()
        {
            mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            mockRepository = new Mock<IDotNetMetricsRepository>();
            controller = new DotNetMetricsController(mockLogger.Object);
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
