using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.ExternalAdapters.Exceptions
{
    /// <summary>
    /// Classe utilizada somente para identificar que o erro foi gerado ao acessar uma  api externa
    /// </summary>
    public class ExternalApiException : Exception
    {
        public ExternalApiException(string message) : base(message)
        {
        }
    }
}
