﻿using AutoMapper;
using MetricsManager.Client.Models;
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
            CreateMap<CpuMetrics, CpuMetricDto>();
            CreateMap<DotNetMetrics, DotNetMetricDto>();
            CreateMap<HddMetrics, HddMetricDto>();
            CreateMap<NetworkMetrics, NetworkMetricDto>();
            CreateMap<RamMetrics, RamMetricDto>();
            CreateMap<long, DateTimeOffset>().ConvertUsing(new DateTimeOffsetConverter());
        }
    }
}
