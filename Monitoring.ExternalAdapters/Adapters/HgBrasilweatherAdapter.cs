using Monitoring.ExternalAdapters.DataMappers;
using Monitoring.ExternalAdapters.Interfaces;
using MonitoringApi.dto;
using MonitoringApi.dto.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.ExternalAdapters.Adapters
{
    /// <summary>
    /// Classe que funciona com um adapter para o HG brasil weather
    /// </summary>
    public class HgBrasilweatherAdapter : IExternalApiAdapter<Temperature>
    {
        #region Attributes

        IResponseMapper<Temperature> _responseMapper = new TemperatureMapper();

        IApiSettings _settings;

        #endregion

        #region Properties

        public IResponseMapper<Temperature> ResponseMapper { get { return _responseMapper; } set { _responseMapper = value; } }

        public IApiSettings ApiSettings { get { return _settings; } }

        #endregion                

        public HgBrasilweatherAdapter(IApiSettings apiSettings)
        {
            _settings = apiSettings;
        }

        /// <summary>
        /// Constroi a url com base nos parâmetros informados
        /// </summary>
        /// <param name="apiSettings">Configurações da api</param>
        /// <param name="searchBy">Parâmetro usado na busca</param>
        /// <param name="term">termo da busca</param>
        /// <returns>Url montada com as informações de acesso e parâmetros de busca</returns>
        public string BuildUrl(IApiSettings apiSettings, SearchParam searchBy, params string[] term)
        {
            var url = string.Empty;
            switch (searchBy)
            {
                case SearchParam.ByName:
                    url = string.Format("key={0}&format=json-cors&sdk_version=hgbrasil&city_name={1}", apiSettings.AccessKey, term[0]);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return string.Format("{0}{1}", apiSettings.BaseUrl, url);
        }
    }
}
