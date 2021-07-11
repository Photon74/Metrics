﻿using AutoMapper;
using MetricsManager.Client;
using MetricsManager.Client.Requests;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Quartz;
using System;
using System.Threading.Tasks;

namespace MetricsManager.Quartz.Jobs
{
    public class CpuMetricsJob : IJob
    {
        private readonly ICpuMetricsRepository _metricsRepository;
        private readonly IAgentRepository _agentRepository;
        private readonly IMetricsAgentClient _client;
        private readonly IMapper _mapper;

        public CpuMetricsJob(ICpuMetricsRepository metricsRepository,
                            IAgentRepository agentRepository,
                            IMetricsAgentClient client,
                            IMapper mapper)
        {
            _metricsRepository = metricsRepository;
            _agentRepository = agentRepository;
            _client = client;
            _mapper = mapper;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var agents = _agentRepository.GetAllAgents();
            foreach (var agent in agents)
            {
                var fromTime = _metricsRepository.GetLastDate(agent.AgentId);
                var toTime = DateTimeOffset.UtcNow;

                var metrics = _client.GetCpuMetrics(new CpuMetricsRequest
                {
                    FromTime = fromTime,
                    ToTime = toTime,
                    AgentUrl = agent.AgentAddress.ToString()
                });

                foreach (var metric in metrics.Metrics)
                {
                    _metricsRepository.Create(_mapper.Map<CpuMetrics>(metric));
                }
            }
            return Task.CompletedTask;
        }
    }
}
