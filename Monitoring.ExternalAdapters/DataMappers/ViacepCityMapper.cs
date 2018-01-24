using Monitoring.ExternalAdapters.Interfaces;
using MonitoringApi.dto;
using System.Collections.Generic;

namespace Monitoring.ExternalAdapters.DataMappers
{
    /// <summary>
    /// Classe de mapeamento do retorno da Api
    /// </summary>
    public class ViacepCityMapper : IResponseMapper<City>
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string unidade { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }

        /// <summary>
        /// Converte um objeto de mapeamento em um objeto da aplicação
        /// </summary>
        /// <returns></returns>
        public IEnumerable<City> ConvertResponseMapper()
        {
            return new List<City> { new City { Name = localidade } };
        }
    }
}
