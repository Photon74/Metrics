using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly PerformanceCounter _networkCounter;
        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", "Realtek PCIe GbE Family Controller");
        }
        public Task Execute(IJobExecutionContext context)
        {
            var networkUsageInBytes = Convert.ToInt32(_networkCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            _repository.Create(new NetworkMetrics { Time = time, Value = networkUsageInBytes });

            return Task.CompletedTask;
        }
    }
}
