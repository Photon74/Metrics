using AutoMapper;
using MetricsManager.Client;
using MetricsManager.Client.Requests;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Quartz;
using System;
using System.Threading.Tasks;

namespace MetricsManager.Quartz.Jobs
{
    public class RamMetricsJob : IJob
    {
        private readonly IRamMetricsRepository _metricsRepository;
        private readonly IAgentRepository _agentRepository;
        private readonly IMetricsAgentClient _client;
        private readonly IMapper _mapper;

        public RamMetricsJob(IRamMetricsRepository metricsRepository,
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
                var toTime = DateTimeOffset.Now;

                var metrics = _client.GetRamMetrics(new RamMetricsRequest
                {
                    FromTime = fromTime,
                    ToTime = toTime,
                    AgentUrl = new Uri(agent.AgentUrl)
                });

                foreach (var metric in metrics.Metrics)
                {
                    _metricsRepository.Create(new RamMetrics
                    {
                        Value = metric.Value,
                        Time = metric.Time.ToUnixTimeSeconds(),
                        AgentId = agent.AgentId
                    });
                }
            }
            return Task.CompletedTask;
        }
    }
}
