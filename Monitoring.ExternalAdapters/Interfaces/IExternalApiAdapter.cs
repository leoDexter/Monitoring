using MonitoringApi.dto.Enums;
using MonitoringApi.dto.Interfaces;

namespace Monitoring.ExternalAdapters.Interfaces
{
    /// <summary>
    /// Interface que define como os adapter de api's externas devem ser
    /// </summary>
    public interface IExternalApiAdapter<T> where T : IEntity
    {
        /// <summary>
        /// Devem possuir uma classe de configurações
        /// </summary>
        IApiSettings ApiSettings { get; }

        /// <summary>
        /// Devem possuir uma função que constroi a url da api de acordo com suas configurações e os termos da busca
        /// </summary>
        /// <param name="apiSettings"></param>
        /// <param name="searchBy"></param>
        /// <param name="term"></param>
        /// <returns>Url para acesso à api</returns>
        string BuildUrl(IApiSettings apiSettings, SearchParam searchBy, params string[] term);

        /// <summary>
        /// Deve possuir uma classe que represente o objeto Json recebido da api
        /// </summary>
        IResponseMapper<T> ResponseMapper { get; set; }
    }
}
