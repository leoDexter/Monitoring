using MonitoringApi.dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringApi.Model
{
    /// <summary>
    /// Model de temperaturas
    /// </summary>
    public class TemperatureModel
    {   
        /// <summary>
        /// Data da última atualização
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// Temperatura registrada
        /// </summary>
        public double temperature { get; set; }

        /// <summary>
        /// Cria uma instancia do model de temperaturas
        /// </summary>
        /// <param name="temperatureDto"></param>
        public TemperatureModel(Temperature temperatureDto)
        {
            date = temperatureDto.Date.ToString("{yyyy-mm-dd HH:mm:ss}");
            temperature = temperatureDto.Degrees;
        }
    }
}
