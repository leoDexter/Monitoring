using DesafioStone.Model;
using DesafioStone.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Monitoring.Repository;
using MonitoringApi.dto;
using MonitoringApi.dto.Extensions;
using MonitoringApi.Model;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DesafioStone.Controllers
{
    /// <summary>
    /// Controller de paginação da api de monitoramento de temperaturas
    /// </summary>
    [Produces("application/json")]
    [Route("api/Temperatures")]
    public class TemperaturesController : Controller
    {
        #region Attributes

        /// <summary>
        /// Configurações default da aplicação
        /// </summary>
        private IConfiguration _configuration;

        /// <summary>
        /// Repositório de cidades
        /// </summary>
        private readonly CityRepository _cityRepository;

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
        /// <param name="generalSettings">Configurações de funcionamento do sistema</param>
        public TemperaturesController(IConfiguration configuration, IOptionsSnapshot<GeneralSettings> generalSettings)
        {
            _configuration = configuration;
            _generalSettings = generalSettings.Value;
            _cityRepository = new CityRepository(_configuration);
        }

        #endregion

        #region Actions Get
        /// <summary>
        /// Retorna a lista paginada das cidades em ordem decrescente da última temperatura registradae sua última temperature registrada.
        /// </summary>
        /// <param name="pageIndex">Página solicitada</param>
        [HttpGet("api/Temperatures/{pageIndex}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(PagedResultsModel))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorModel))]
        public ActionResult GetWithPaging(int pageIndex)
        {
            long totalRecords = 0;
            var pagedCities = (List<City>)_cityRepository.GetPaged(out totalRecords, pageIndex, _generalSettings.PaginationSize);
            pagedCities.ForEach(c => c.LastTemperatureExt(_configuration));

            return Ok(new PagedResultsModel(pagedCities, pageIndex, _generalSettings.PaginationSize, totalRecords));
        }

        #endregion
    }
}