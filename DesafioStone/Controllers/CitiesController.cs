using DesafioStone.Model;
using DesafioStone.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Monitoring.ExternalAdapters;
using Monitoring.ExternalAdapters.Settings;
using Monitoring.Repository;
using MonitoringApi.dto;
using MonitoringApi.dto.Enums;
using MonitoringApi.dto.Extensions;
using MonitoringApi.Model;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Net;

namespace DesafioStone.Controllers
{
    /// <summary>
    /// Controller da api de monitoramento de temperatura
    /// </summary>
    [Produces("application/json")]
    [Route("api/Cities")]
    public class CitiesController : Controller
    {
        #region Attributes

        /// <summary>
        /// Representa o repositória de cidades
        /// </summary>
        private readonly CityRepository _cityRepository;

        /// <summary>
        /// Representa o repositório de temperaturas
        /// </summary>
        private readonly TemperatureRepository _temperatureRepository;

        /// <summary>
        /// Facade de acesso à api de busca de cidades por cep
        /// </summary>
        private BaseFacade<City> _bycepFacade;

        /// <summary>
        /// Facade de acesso à api de busca de cidades para autocomplete
        /// </summary>
        private BaseFacade<City> _autocompleteFacade;

        /// <summary>
        /// Configurações default da aplicação
        /// </summary>
        private IConfiguration _configuration;

        /// <summary>
        /// Configurações gerais da aplicação
        /// </summary>
        private GeneralSettings _generalSettings;

        #endregion

        #region Constructor

        /// <summary>
        /// Construtor 
        /// </summary>
        /// <param name="configuration">Configurações gerais da aplicação, passado por injeção de dependencia registrada no startup.cs</param>
        /// <param name="byCepSettings">Configurações da api utilizada para buscar cidades por CEP (Opção registrada no startup.cs)</param>
        /// <param name="autocompleteSettings">Configurações da api utilizada para buscar cidades para autocomplete (Opção registrada no startup.cs)</param>        
        /// <param name="generalSettings">Configurações de funcionamento da aplicação (Opção registrada no startup.cs)</param>   
        public CitiesController(IConfiguration configuration,
            IOptionsSnapshot<CityByCepApiSettings> byCepSettings,
            IOptionsSnapshot<CityAutocompleteApiSettings> autocompleteSettings,
            IOptionsSnapshot<GeneralSettings> generalSettings)
        {
            _configuration = configuration;
            _generalSettings = generalSettings.Value;

            _cityRepository = new CityRepository(_configuration);
            _temperatureRepository = new TemperatureRepository(_configuration);

            _bycepFacade = FacadeFactory<City>.Create(byCepSettings.Value);
            _autocompleteFacade = FacadeFactory<City>.Create(autocompleteSettings.Value);
        }

        #endregion

        #region Get actions        

        /// <summary>
        /// Retorna as temperaturas registradas nas últimas 30 horas da cidade consultada.
        /// </summary>
        /// <param name="city_name">Nome da cidade a ser consultada</param>
        [HttpGet("{city_name}/temperatures")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(CityTemperatureModel))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorModel))]
        public ActionResult Get(string city_name)
        {
            var city = _cityRepository.GetByName(city_name);
            city.LatestTemperaturesExt(_configuration, _generalSettings.Latest);
            return Ok(new CityTemperatureModel(city));
        }

        /// <summary>
        /// Retorna uma lista de nomes relacionados ao termo da busca
        /// </summary>
        /// <param name="term">Termo da busca</param>        
        [HttpGet("Autocomplete/{term}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<string>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorModel))]
        public ActionResult GetAutocomplete(string term)
        {
            var result = _autocompleteFacade.FindMany(SearchParam.ByName, term).Result;
            return Ok(new AutocompleteModel(result));
        }

        #endregion

        #region Post actions

        /// <summary>
        /// Cadastra uma cidade no monitoramento de temperaturas por nome
        /// </summary>
        /// <param name="city_name">Nome da cidade a ser cadastrada no monitoramento de temperaturas</param>        
        [HttpPost("{city_name}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorModel))]
        public ActionResult Post(string city_name)
        {
            _cityRepository.Add(new City { Name = city_name });
            return Ok();
        }


        /// <summary>
        /// Cadastra uma cidade no monitoramento de temperaturas por CEP
        /// </summary>
        /// <param name="cep">CEP da cidade a ser cadastrada no monitoramento de temperaturas</param>        
        [HttpPost("by_cep/{cep}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorModel))]
        public ActionResult PostByCep(string cep)
        {
            _cityRepository.Add(_bycepFacade.FindFirstOrDefault(SearchParam.ByCep, cep).Result);
            return Ok();
        }

        #endregion

        #region Delete actions


        /// <summary>
        /// Remove a cidade informada do monitoramento de temperaturas.
        /// </summary>
        /// <param name="city_name">Nome da cidade a ser removida</param>        
        [HttpDelete("{city_name}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorModel))]
        public ActionResult Delete(string city_name)
        {
            var city = _cityRepository.GetByName(city_name);
            _temperatureRepository.Remove(city.TemperaturesExt(_configuration));
            _cityRepository.Remove(city);
            return Ok();
        }

        /// <summary>
        /// Apaga todo o histórico de temperaturas da cidade informada
        /// </summary>
        /// <param name="city_name">Nome da cidade cujo histórico será aparado</param>        
        [HttpDelete("{city_name}/temperatures")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorModel))]
        public ActionResult DeleteHistory(string city_name)
        {
            var city = _cityRepository.GetByName(city_name);
            _temperatureRepository.Remove(city.TemperaturesExt(_configuration));
            return Ok();
        }

        #endregion
    }

}