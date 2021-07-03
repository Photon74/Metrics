using AutoMapper;
using MetricsAgent.Controllers.Models;
using MetricsAgent.DAL.Models;
using System;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<long, DateTimeOffset>().ConvertUsing(new DateTimeOffsetConverter());
            CreateMap<CpuMetrics, CpuMetricDto>();
            CreateMap<DotNetMetrics, DotNetMetricDto>();
            CreateMap<HddMetrics, HddMetricDto>();
            CreateMap<NetworkMetrics, NetworkMetricDto>();
            CreateMap<RamMetrics, RamMetricDto>();
        }
    }

    class DateTimeOffsetConverter : ITypeConverter<long, DateTimeOffset>
    {
        public DateTimeOffset Convert(long source, DateTimeOffset destination, ResolutionContext context)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(source);
        }
    }
}
