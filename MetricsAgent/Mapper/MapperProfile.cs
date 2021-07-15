using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.Mediator.Models;
using System;

namespace MetricsAgent.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetrics, CpuMetricDto>();
            CreateMap<DotNetMetrics, DotNetMetricDto>();
            CreateMap<HddMetrics, HddMetricDto>();
            CreateMap<NetworkMetrics, NetworkMetricDto>();
            CreateMap<RamMetrics, RamMetricDto>();
            CreateMap<long, DateTimeOffset>().ConvertUsing(new DateTimeOffsetConverter());
        }
    }
}
