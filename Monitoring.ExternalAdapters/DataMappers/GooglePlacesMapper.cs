using Monitoring.ExternalAdapters.Interfaces;
using MonitoringApi.dto;
using System.Collections.Generic;

namespace Monitoring.ExternalAdapters.DataMappers
{
    /// <summary>
    /// Classe de mapeamento do retorno da Api
    /// </summary>
    public class GooglePlacesMapper : IResponseMapper<City>
    {
        public List<Prediction> predictions { get; set; }
        public string status { get; set; }

        /// <summary>
        /// Converte um objeto de mapeamento em um objeto da aplicação
        /// </summary>
        /// <returns></returns>
        public IEnumerable<City> ConvertResponseMapper()
        {
            var data = new List<City>();
            foreach (var item in predictions)
            {
                data.Add(new City { Name = item.structured_formatting.main_text });
            }
            return data;
        }
    }

    public class StructuredFormatting
    {
        public string main_text { get; set; }
    }

    public class Prediction
    {
        public string Description { get; set; }
        public StructuredFormatting structured_formatting { get; set; }
    }

}
