using Monitoring.ExternalAdapters.Interfaces;

namespace Monitoring.ExternalAdapters.Settings
{
    public class CityAutocompleteApiSettings : IApiSettings
    {
        public string ApiName { get; set; }
        public string BaseUrl { get; set; }
        public string AccessKey { get; set; }
        public string MediaType { get; set; }        
        public string AdapterName { get; set; }
    }
}
