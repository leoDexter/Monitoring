using MonitoringApi.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringApi.Model
{
    /// <summary>
    /// Classe de retorno de uma cidade com suas últimas temperaturas registradas
    /// </summary>
    public class CityTemperatureModel
    {
        /// <summary>
        /// Nome da cidade
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// Temperaturas
        /// </summary>
        public List<TemperatureModel> temperatures { get; set; }

        /// <summary>
        /// Representa as informações de monitoramento de uma cidade específica
        /// </summary>
        /// <param name="cityDto">Dados da cidade monitorada</param>        
        public CityTemperatureModel(City cityDto)
        {
            city = cityDto.Name;

            temperatures = new List<TemperatureModel>();
            foreach (var t in cityDto.Temperatures)
                temperatures.Add(new TemperatureModel(t));
        }
    }
}
