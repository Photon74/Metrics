using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricsRepository _repository;
        private readonly PerformanceCounter _dotnetCounter;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotnetCounter = new PerformanceCounter("ASP.NET", "Error Events Raised");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var dotNetErrors = Convert.ToInt32(_dotnetCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            _repository.Create(new DotNetMetrics
            {
                Time = time,
                Value = dotNetErrors
            });

            return Task.CompletedTask;
        }
    }
}
