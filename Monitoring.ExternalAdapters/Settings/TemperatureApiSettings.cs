using Monitoring.ExternalAdapters.Interfaces;

namespace Monitoring.ExternalAdapters.Settings
{
    /// <summary>
    /// Representa as configurações existentes para a api de acesso à api externa usada para busca de informações de temperatura
    /// </summary>
    public class TemperatureApiSettings : IApiSettings
    {
        public string ApiName { get; set; }
        public string BaseUrl { get; set; }
        public string AccessKey { get; set; }
        public string MediaType { get; set; }
        public string AdapterName { get; set; }
    }
}
