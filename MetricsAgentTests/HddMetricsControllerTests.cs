using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Mediator;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace MetricsAgentTests
{
    public class HddMetricsControllerTests
    {
        private readonly Mock<ILogger<HddRequestHandler>> _mockLogger;
        private readonly Mock<IHddMetricsRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DateTimeRangeForHdd _dateTimeRange;
        private readonly HddRequestHandler _handler;

        public HddMetricsControllerTests()
        {
            _mockLogger = new Mock<ILogger<HddRequestHandler>>();
            _mockRepository = new Mock<IHddMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            _dateTimeRange = new DateTimeRangeForHdd
            {
                FromTime = DateTimeOffset.FromUnixTimeSeconds(0),
                ToTime = DateTimeOffset.FromUnixTimeSeconds(100)
            };
            _handler = new HddRequestHandler(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetMetricst_ReturnOk()
        {
            //Arrange
            _mockRepository.Setup(repository =>
                repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<HddMetrics>()).Verifiable();

            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Act
            var result = _handler.Handle(_dateTimeRange, CancellationToken.None);

            //Assert
            _mockRepository.Verify(repository =>
                repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }
}