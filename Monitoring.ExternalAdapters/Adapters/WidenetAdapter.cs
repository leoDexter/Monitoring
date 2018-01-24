using Monitoring.ExternalAdapters.DataMappers;
using Monitoring.ExternalAdapters.Exceptions;
using Monitoring.ExternalAdapters.Interfaces;
using MonitoringApi.dto;
using MonitoringApi.dto.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.ExternalAdapters.Adapters
{
    /// <summary>
    /// Classe que funciona com um adapter para o Widnet
    /// </summary>
    public class WidenetAdapter : IExternalApiAdapter<City>
    {
        #region Attributes

        IResponseMapper<City> _responseMapper = new WidenetMapper();

        IApiSettings _settings;

        #endregion

        #region Properties

        public IResponseMapper<City> ResponseMapper { get { return _responseMapper; } set { _responseMapper = value; } }

        public IApiSettings ApiSettings { get { return _settings; } }

        #endregion

        public WidenetAdapter(IApiSettings apiSettings)
        {
            _settings = apiSettings;
        }

        /// <summary>
        /// Constroi a url com base nos parâmetros informados
        /// </summary>
        /// <param name="apiSettings"></param>
        /// <param name="searchBy"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        public string BuildUrl(IApiSettings apiSettings, SearchParam searchBy, params string[] term)
        {
            if (searchBy != SearchParam.ByCep)
                throw new ExternalApiException("Widenet API só permite buscas por CEP");
            if (term is null || term.Length == 0)
                throw new ExternalApiException("Widenet API requer que o CEP seja informado");

            var cep = term[0];

            if (cep.Length != 8)
                throw new ExternalApiException("Widenet API requer que o CEP tenha 8 dígitos");

            return string.Format("{0}/{1}.json", apiSettings.BaseUrl, cep);
        }
    }
}
