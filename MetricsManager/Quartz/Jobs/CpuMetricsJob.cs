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
    [DisallowConcurrentExecution]
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
            SqlMapper.AddTypeHandler(new UriHandler());
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var agents = _agentRepository.GetAllAgents();
            foreach (var agent in agents)
            {
                var FromTime = _metricsRepository.GetLastDate(agent.AgentId);
                var ToTime = DateTimeOffset.UtcNow;

                var metrics = await _client.GetCpuMetrics(new CpuMetricsApiRequest
                {
                    FromTime = FromTime,
                    ToTime = ToTime,
                    AgentUrl = new Uri(agent.AgentUrl)
                });

                if (metrics != null)
                {
                    foreach (var metric in metrics.Metrics)
                    {
                        _metricsRepository.Create( new CpuMetrics
                        {
                            Value = metric.Value,
                            Time = metric.Time.ToUnixTimeSeconds(),
                            AgentId = agent.AgentId
                        });
                    }
                }
            }
        }
    }
}
