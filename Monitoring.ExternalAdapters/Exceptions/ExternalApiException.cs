using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.ExternalAdapters.Exceptions
{
    public class ExternalApiException : Exception
    {
        public ExternalApiException(string message) : base(message)
        {
        }
    }
}
