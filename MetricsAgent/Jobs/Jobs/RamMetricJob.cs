using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _repository;
        private readonly PerformanceCounter _ramCounter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var ramUsageInMBytes = Convert.ToInt32(_ramCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            _repository.Create(new RamMetrics
            {
                Time = time,
                Value = ramUsageInMBytes
            });

            return Task.CompletedTask;
        }
    }
}
