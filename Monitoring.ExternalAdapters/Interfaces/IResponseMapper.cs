using MonitoringApi.dto.Interfaces;
using System.Collections.Generic;

namespace Monitoring.ExternalAdapters.Interfaces
{
    /// <summary>
    /// Response mapper contract
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResponseMapper<T> where T : IEntity
    {
        IEnumerable<T> ConvertResponseMapper();
    }
}
