using AutoMapper;
using MetricsManager.Client.Models;
using MetricsManager.Client.Responses;
using MetricsManager.Controllers.Models;
using MetricsManager.DAL.Models;
using MetricsManager.Mediator.Requests;
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
            CreateMap<CpuMetrics, CpuMetricDto>();
            CreateMap<CpuMetricsApiResponse, CpuMetrics>();

            CreateMap<AgentIdTimePeriodCpuRequest, AgentIdTimePeriod>();
            CreateMap<TimePeriodCpuRequest, TimePeriod>();
            CreateMap<AgentIdTimePeriodDotNetRequest, AgentIdTimePeriod>();
            CreateMap<TimePeriodDotNetRequest, TimePeriod>();
            CreateMap<AgentIdTimePeriodHddRequest, AgentIdTimePeriod>();
            CreateMap<TimePeriodHddRequest, TimePeriod>();
            CreateMap<AgentIdTimePeriodNetworkRequest, AgentIdTimePeriod>();
            CreateMap<TimePeriodNetworkRequest, TimePeriod>();
            CreateMap<AgentIdTimePeriodRamRequest, AgentIdTimePeriod>();
            CreateMap<TimePeriodRamRequest, TimePeriod>();

            CreateMap<long, DateTimeOffset>().ConvertUsing(new LongToDateTimeOffsetConverter());
            CreateMap<DateTimeOffset, long>().ConvertUsing(new DateTimeOffsetToLongConverter());
        }
    }
}
