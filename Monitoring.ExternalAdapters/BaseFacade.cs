using Monitoring.ExternalAdapters.Interfaces;
using MonitoringApi.dto.Enums;
using MonitoringApi.dto.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitoring.ExternalAdapters
{
    /// <summary>
    /// Handles the requests to external api's
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseFacade<T> where T : IEntity
    {
        #region Attributes

        IExternalApiAdapter<T> _adapter;

        #endregion

        public BaseFacade(IExternalApiAdapter<T> adapter)
        {
            _adapter = adapter;
        }

        #region Private functions

        /// <summary>
        /// Executa a chamada à api externa
        /// </summary>
        /// <param name="param">Tipo de parâmetro de busca</param>
        /// <param name="terms">termos da busca</param>
        /// <returns></returns>
        private async Task<IEnumerable<T>> RequestExternalApi(SearchParam param, params string[] terms)
        {
            var client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(_adapter.ApiSettings.MediaType));
            var json = await client.GetStringAsync(_adapter.BuildUrl(_adapter.ApiSettings, param, terms));

            _adapter.ResponseMapper = (IResponseMapper<T>)JsonConvert.DeserializeObject(json, _adapter.ResponseMapper.GetType());
            return _adapter.ResponseMapper.ConvertResponseMapper();
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Retorna mais de um valor, quando retornado pela api externa
        /// </summary>
        /// <param name="param"></param>
        /// <param name="terms"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> FindMany(SearchParam param, params string[] terms)
        {
            var data = await RequestExternalApi(param, terms);
            return data;
        }

        /// <summary>
        /// Retorna o primeira valor, quando retornado pela api externa
        /// </summary>
        /// <param name="param"></param>
        /// <param name="terms"></param>
        /// <returns></returns>
        public virtual async Task<T> FindFirstOrDefault(SearchParam param, params string[] terms)
        {
            var data = await RequestExternalApi(param, terms);
            return data.FirstOrDefault();
        }

        #endregion
    }
}
