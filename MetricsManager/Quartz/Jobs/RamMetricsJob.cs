using AutoMapper;
using Dapper;
using MetricsManager.Client;
using MetricsManager.Client.Requests;
using MetricsManager.DAL;
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
            SqlMapper.AddTypeHandler(new UriHandler());
        }

        public Task Execute(IJobExecutionContext context)
        {
            var agents = _agentRepository.GetAllAgents();
            foreach (var agent in agents)
            {
                var FromTime = _metricsRepository.GetLastDate(agent.AgentId);
                var ToTime = DateTimeOffset.Now;

                var metrics = _client.GetRamMetrics(new RamMetricsRequest
                {
                    FromTime = FromTime,
                    ToTime = ToTime,
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
