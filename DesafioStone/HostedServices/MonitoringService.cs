using DesafioStone.Settings;
using Microsoft.Extensions.Configuration;
using Monitoring.ExternalAdapters;
using Monitoring.Repository;
using MonitoringApi.dto;
using MonitoringApi.dto.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MonitoringApi.HostedServices
{
    /// <summary>
    /// Serviço de atualização das temperaturas das cidades agendadas
    /// </summary>
    public class MonitoringService : BaseTask
    {
        #region Attributes

        IEnumerable<City> _cities;
        CityRepository _cityRepository;
        TemperatureRepository _temperatureRepository;
        BaseFacade<Temperature> _temperatureApiFacade;
        GeneralSettings _serviceSettings;

        #endregion

        /// <summary>
        /// Constroi instancia do serviço
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="serviceSettings"></param>
        /// <param name="temperatureFacade"></param>
        public MonitoringService(IConfiguration configuration, GeneralSettings serviceSettings, BaseFacade<Temperature> temperatureFacade)
        {
            _temperatureRepository = new TemperatureRepository(configuration);
            _cityRepository = new CityRepository(configuration);
            _temperatureApiFacade = temperatureFacade;
            _serviceSettings = serviceSettings;
        }

        #region Override start and stop methods

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        #endregion

        #region Specific task implementation 

        delegate Task TestDelegate(CancellationToken stoppingToken);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Loading monitored cities
                _cities = _cityRepository.GetAll();

                foreach (var city in _cities)
                {
                    var temp = await _temperatureApiFacade.FindFirstOrDefault(SearchParam.ByName, city.Name);
                    temp.CityId = city.Id;
                    _temperatureRepository.Add(temp);
                }

                await Task.Delay(_serviceSettings.MonitoringServiceInterval * 60 * 1000, stoppingToken);
            }
        }

        #endregion
    }
}
