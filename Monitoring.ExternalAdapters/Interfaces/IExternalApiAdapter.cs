using MonitoringApi.dto.Enums;
using MonitoringApi.dto.Interfaces;

namespace Monitoring.ExternalAdapters.Interfaces
{
    /// <summary>
    /// External Api adapter contract
    /// </summary>
    public interface IExternalApiAdapter<T> where T : IEntity
    {
        IApiSettings ApiSettings { get; }
        string BuildUrl(IApiSettings apiSettings, SearchParam searchBy, params string[] term);
        IResponseMapper<T> ResponseMapper { get; set; }
    }
}
