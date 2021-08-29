using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Client.Interfaces
{
    interface IMetricsResponse<T>
    {
        IList<T> Metrics { get; set; }
    }
}
