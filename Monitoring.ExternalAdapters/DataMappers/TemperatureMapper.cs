using Monitoring.ExternalAdapters.Interfaces;
using MonitoringApi.dto;
using System;
using System.Collections.Generic;

namespace Monitoring.ExternalAdapters.DataMappers
{
    /// <summary>
    /// Classe de mapeamento do retorno da Api
    /// </summary>
    public class TemperatureMapper : IResponseMapper<Temperature>
    {
        public Results results { get; set; }

        /// <summary>
        /// Converte um objeto de mapeamento em um objeto da aplicação
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Temperature> ConvertResponseMapper() 
        {
            return new List<Temperature> {
                new Temperature {
                    Degrees = Convert.ToDouble(results.temp),
                    Date = Convert.ToDateTime(string.Format("{0} {1}", results.date, results.time))
                }
            };
        }
    }

    public class Results
    {
        public int temp { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string condition_code { get; set; }
        public string description { get; set; }
        public string currently { get; set; }
        public string cid { get; set; }
        public string city { get; set; }
        public string img_id { get; set; }
        public string humidity { get; set; }
        public string wind_speedy { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string condition_slug { get; set; }
        public string city_name { get; set; }        
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
