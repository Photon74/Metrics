using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.Mediator.Handlers;
using MetricsManager.Mediator.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuRepositoryTests
    {
        private readonly Mock<ILogger<CpuRequestHandler>> _mockLogger;
        private readonly Mock<ICpuMetricsRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TimePeriod _timePeriod;
        private readonly AgentIdTimePeriod _agentIdTimePeriod;
        private readonly CpuRequestHandler _handler;

        public CpuRepositoryTests()
        {
            _mockLogger = new Mock<ILogger<CpuRequestHandler>>();
            _mockRepository = new Mock<ICpuMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            _timePeriod = new TimePeriod
            {
                FromTime = DateTimeOffset.FromUnixTimeSeconds(0),
                ToTime = DateTimeOffset.FromUnixTimeSeconds(100)
            };
            _agentIdTimePeriod = new AgentIdTimePeriod
            {
                AgentId = 1,
                FromTime = DateTimeOffset.FromUnixTimeSeconds(0),
                ToTime = DateTimeOffset.FromUnixTimeSeconds(100)
            };
            _handler = new CpuRequestHandler(_mockRepository.Object, _mockLogger.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetMetricsFromCluster_ReturnsOk()
        {
            //Arrange
            _mockRepository.Setup(repository => repository
                .GetByTimePeriod(_timePeriod))
                .Returns(new List<CpuMetrics>())
                .Verifiable();

            //Assert
            _mockRepository.Verify(repository => repository
                .GetByTimePeriod(_timePeriod), Times.AtMostOnce());
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            //Arrange
            _mockRepository.Setup(repository => repository
                .GetByTimePeriodFromAgent(_agentIdTimePeriod))
                .Returns(new List<CpuMetrics>())
                .Verifiable();

            //Assert
            _mockRepository.Verify(repository => repository
                .GetByTimePeriodFromAgent(_agentIdTimePeriod), Times.AtMostOnce());
        }
    }
}
