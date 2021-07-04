using AutoMapper;
using System;

namespace MetricsAgent.Mapper
{
    class DateTimeOffsetConverter : ITypeConverter<long, DateTimeOffset>
    {
        public DateTimeOffset Convert(long source,
                                      DateTimeOffset destination,
                                      ResolutionContext context)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(source);
        }
    }
}
