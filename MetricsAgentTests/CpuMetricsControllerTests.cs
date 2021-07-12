using AutoMapper;
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
    public class CpuMetricsControllerTests
    {
        private readonly Mock<ILogger<CpuRequestHandler>> _mockLogger;
        private readonly Mock<ICpuMetricsRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DateTimeRangeForCpu _dateTimeRange;
        private readonly CpuRequestHandler _handler;

        public CpuMetricsControllerTests()
        {
            _mockLogger = new Mock<ILogger<CpuRequestHandler>>();
            _mockRepository = new Mock<ICpuMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            _dateTimeRange = new DateTimeRangeForCpu
            {
                FromTime = DateTimeOffset.FromUnixTimeSeconds(0),
                ToTime = DateTimeOffset.FromUnixTimeSeconds(100)
            };
            _handler = new CpuRequestHandler(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetMetricst_ReturnOk()
        {
            //Arrange
            _mockRepository.Setup(repository =>
                repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<CpuMetrics>()).Verifiable();

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
