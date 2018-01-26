using Monitoring.ExternalAdapters.Interfaces;

namespace Monitoring.ExternalAdapters.Settings
{
    /// <summary>
    /// Representa as configurações existentes para a api de acesso à api externa usada para busca de cidades por CEP
    /// </summary>
    public class CityByCepApiSettings : IApiSettings
    {
        public string ApiName { get; set; }
        public string BaseUrl { get; set; }
        public string AccessKey { get; set; }
        public string MediaType { get; set; }
        public string AdapterName { get; set; }
    }
}
