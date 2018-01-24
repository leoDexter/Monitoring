using Monitoring.ExternalAdapters.Interfaces;
using MonitoringApi.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.ExternalAdapters.DataMappers
{
    /// <summary>
    /// Classe de mapeamento do retorno da Api
    /// </summary>
    public class WidenetMapper : IResponseMapper<City>
    {
        public int status { get; set; }
        public string code { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string address { get; set; }

        /// <summary>
        /// Converte um objeto de mapeamento em um objeto da aplicação
        /// </summary>
        /// <returns></returns>
        public IEnumerable<City> ConvertResponseMapper()
        {
            return new List<City> { new City { Name = city } };
        }
    }
}
