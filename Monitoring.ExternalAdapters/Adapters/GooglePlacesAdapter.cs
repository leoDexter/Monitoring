using Monitoring.ExternalAdapters.DataMappers;
using Monitoring.ExternalAdapters.Interfaces;
using MonitoringApi.dto;
using MonitoringApi.dto.Enums;

namespace Monitoring.ExternalAdapters.Adapters
{
    /// <summary>
    /// Classe que funciona com um adapter para o google places
    /// </summary>
    public class GooglePlacesAdapter : IExternalApiAdapter<City>
    {
        #region Attributes

        IResponseMapper<City> _responseMapper = new GooglePlacesMapper();

        IApiSettings _settings;

        #endregion

        #region Properties

        public IResponseMapper<City> ResponseMapper { get { return _responseMapper; } set { _responseMapper = value; } }

        public IApiSettings ApiSettings { get { return _settings; } }

        #endregion

        public GooglePlacesAdapter(IApiSettings apiSettings)
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
            var url = apiSettings.BaseUrl;

            switch (searchBy)
            {
                case SearchParam.ByName:
                    url += string.Format("input={0}", term[0]);
                    break;
                case SearchParam.ByIp:
                    break;
                case SearchParam.ByGeoLocalization:
                    break;
                default:
                    break;
            }

            return string.Concat(url, string.Format("&types=(cities)&language=pt_BR&key={0}", apiSettings.AccessKey));
        }
    }
}
