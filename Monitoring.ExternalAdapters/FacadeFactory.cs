using Monitoring.ExternalAdapters.Adapters;
using Monitoring.ExternalAdapters.Interfaces;
using MonitoringApi.dto.Interfaces;
using System;

namespace Monitoring.ExternalAdapters
{
    /// <summary>
    /// Factory that generates configurated facades to access external apis
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class FacadeFactory<T> where T : IEntity, new()
    {
        public static BaseFacade<T> Create(IApiSettings settings)
        {
            if (settings.AdapterName.Equals("HgBrasilweatherAdapter"))
                return new BaseFacade<T>((IExternalApiAdapter<T>)new HgBrasilweatherAdapter(settings));

            if (settings.AdapterName.Equals("ViacepAdapter"))
                return new BaseFacade<T>((IExternalApiAdapter<T>)new ViacepAdapter(settings));            

            if (settings.AdapterName.Equals("GooglePlacesAdapter"))
                return new BaseFacade<T>((IExternalApiAdapter<T>)new GooglePlacesAdapter(settings));

            if (settings.AdapterName.Equals("WidenetAdapter"))
                return new BaseFacade<T>((IExternalApiAdapter<T>)new WidenetAdapter(settings));

            // Otherwise :: adapter not found
            throw new Exception(string.Format("Adapter: '{0}', not found", settings.AdapterName));
        }
    }
}
