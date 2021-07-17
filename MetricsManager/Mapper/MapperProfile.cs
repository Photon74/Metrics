using AutoMapper;
using MetricsManager.Client.Models;
using MetricsManager.Client.Responses;
using MetricsManager.Controllers.Models;
using MetricsManager.DAL.Models;
using System;

namespace MetricsManager.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AgentInfo, Agent>();
            CreateMap<Agent, AgentInfo>();
            CreateMap<DotNetMetrics, DotNetMetricDto>();
            CreateMap<HddMetrics, HddMetricDto>();
            CreateMap<NetworkMetrics, NetworkMetricDto>();
            CreateMap<RamMetrics, RamMetricDto>();
            CreateMap<CpuMetrics, CpuMetricsApiResponse>();
            CreateMap<CpuMetricsApiResponse, CpuMetrics>();
            CreateMap<long, DateTimeOffset>().ConvertUsing(new LongToDateTimeOffsetConverter());
            CreateMap<DateTimeOffset, long>().ConvertUsing(new DateTimeOffsetToLongConverter());
        }
    }
}
