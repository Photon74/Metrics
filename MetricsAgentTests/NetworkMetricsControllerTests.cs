using MetricsAgent.Controllers;
using MetricsAgent.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkMetricsControllerTests
    {
        private NetworkMetricsController controller;
        private Mock<ILogger<NetworkMetricsController>> mockLogger;
        private Mock<INetworkMetricsRepository> mockRepository;

        public NetworkMetricsControllerTests()
        {
            mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            mockRepository = new Mock<INetworkMetricsRepository>();
            controller = new NetworkMetricsController(mockLogger.Object);
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
