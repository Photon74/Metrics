﻿using MetricsAgent.DAL.Models;
using MetricsManager.DAL.Interfaces;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetrics>
    {
    }
}
