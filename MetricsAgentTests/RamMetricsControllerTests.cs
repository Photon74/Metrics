using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Mediator;
using MetricsAgent.Mediator.Requests;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsControllerTests
    {
        private readonly Mock<ILogger<RamRequestHandler>> _mockLogger;
        private readonly Mock<IRamMetricsRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DateTimeRangeForRam _dateTimeRange;
        private readonly RamRequestHandler _handler;

        public RamMetricsControllerTests()
        {
            _mockLogger = new Mock<ILogger<RamRequestHandler>>();
            _mockRepository = new Mock<IRamMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            _dateTimeRange = new DateTimeRangeForRam
            {
                FromTime = DateTimeOffset.FromUnixTimeSeconds(0),
                ToTime = DateTimeOffset.FromUnixTimeSeconds(100)
            };
            _handler = new RamRequestHandler(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetMetricst_ReturnOk()
        {
            //Arrange
            _mockRepository.Setup(repository =>
                repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<RamMetrics>()).Verifiable();

            //Act
            var result = _handler.Handle(_dateTimeRange, CancellationToken.None);

            //Assert
            _mockRepository.Verify(repository =>
                repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }
}
