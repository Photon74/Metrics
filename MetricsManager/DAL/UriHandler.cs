using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL
{
    public class UriHandler : SqlMapper.TypeHandler<Uri>
    {
        public override Uri Parse(object value)
        {
            return new Uri((string)value);
        }

        public override void SetValue(IDbDataParameter parameter, Uri value)
        {
            parameter.Value = value;
        }
    }
}
