﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsView
{
    internal class LoggerFactory : ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider)
        {
            throw new NotImplementedException();
        }

        public ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
