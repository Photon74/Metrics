using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsRepository _repository;
        private readonly PerformanceCounter _hddCounter;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var hddUsageInPercents = Convert.ToInt32(_hddCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            _repository.Create(new HddMetrics
            {
                Time = time,
                Value = hddUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}
