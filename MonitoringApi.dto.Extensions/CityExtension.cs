using Microsoft.Extensions.Configuration;
using Monitoring.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringApi.dto.Extensions
{
    public static class CityExtension
    {
        /// <summary>
        /// Carrega no objeto atual e retorna todas as temperaturas registradas para uma cidade.
        /// </summary>
        /// <param name="city">Cidade</param>
        /// <param name="config">Configurações da aplicação</param>
        /// <returns></returns>
        public static IEnumerable<Temperature> TemperaturesExt(this City city, IConfiguration config)
        {
            var temperatureRepository = new TemperatureRepository(config);
            var allTemperatures = temperatureRepository.GetAll(city.Id);
            city.Temperatures = allTemperatures;

            return city.Temperatures;
        }

        /// <summary>
        /// Carrega no objeto atual e retorna todas as temperaturas registradas nas íltimas XX horas
        /// </summary>
        /// <param name="city">Cidade</param>
        /// <param name="config">Configurações da aplicação</param>
        /// <param name="latestHours">Numero de horas</param>
        /// <returns></returns>
        public static IEnumerable<Temperature> LatestTemperaturesExt(this City city, IConfiguration config, int latestHours)
        {
            var temperatureRepository = new TemperatureRepository(config);
            var lastestTemperature = temperatureRepository.GetByDate(new Temperature { CityId = city.Id }, DateTime.Now.AddHours(0 - latestHours));
            city.Temperatures = lastestTemperature;

            return city.Temperatures;
        }

        public static Temperature LastTemperatureExt(this City city, IConfiguration config)
        {
            var temperatureRepository = new TemperatureRepository(config);
            var lastTemperature = temperatureRepository.GetLast(city.Id);
            city.Temperatures = new List<Temperature> { lastTemperature };

            return lastTemperature;
        }
    }
}
