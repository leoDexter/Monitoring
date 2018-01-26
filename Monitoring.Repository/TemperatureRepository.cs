using Dapper;
using Microsoft.Extensions.Configuration;
using Monitoring.Repository.Interfaces;
using Monitoring.Repository.Validation;
using MonitoringApi.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Monitoring.Repository
{
    /// <summary>
    /// Repositório de temperaturas
    /// </summary>
    public class TemperatureRepository : AbstractRepository<Temperature>
    {
        #region Attributes

        TemperatureValidation _temperatureValidation;

        #endregion

        #region Properties

        public override ICustomValidation<Temperature> CustomValidation
        {
            get { return _temperatureValidation != null ? _temperatureValidation : _temperatureValidation = new TemperatureValidation(); }
        }

        #endregion

        public TemperatureRepository(IConfiguration configuration) : base(configuration) { }

        #region Functions

        /// <summary>
        /// Obter temperatura por is
        /// </summary>
        /// <param name="id">Id da temperatura</param>
        /// <returns></returns>
        public override Temperature GetById(int id)
        {
            const string query = "SELECT T.* FROM Temperatures T WHERE T.Id = @Id";
            var parameters = AddFilterParameters(new Temperature { Id = id });

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                return dbConnection.QueryFirstOrDefault<Temperature>(query, parameters);
            }
        }

        /// <summary>
        /// Obter a última temperatura registrada para uma cidade
        /// </summary>
        /// <param name="cityId">Id da cidade</param>
        /// <returns></returns>
        public virtual Temperature GetLast(int cityId)
        {
            const string query = "SELECT TOP(1) T.* FROM Temperatures T WHERE T.CityId = @CityId ORDER BY Id DESC";
            var parameters = AddFilterParameters(new Temperature { CityId = cityId });

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                return dbConnection.QueryFirstOrDefault<Temperature>(query, parameters);
            }
        }

        /// <summary>
        /// Obter N temperaturas registradas para uma cidade a partir de uma data
        /// </summary>
        /// <param name="dto">Objeto contendo o Id da cidade</param>
        /// <param name="startDate">Data</param>
        /// <returns></returns>
        public virtual IEnumerable<Temperature> GetByDate(Temperature dto, DateTime startDate)
        {
            if (dto.CityId == 0)
                CustomValidation.ValidationErrors.AddMessage("A cidade deve ser informada");

            const string query = "SELECT T.* FROM Temperatures T WHERE T.CityId=@CityId AND T.Date>=@Date";
            var parameters = AddFilterParameters(new Temperature { CityId = dto.CityId, Date = startDate });

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                return dbConnection.Query<Temperature>(query, parameters);
            }
        }

        /// <summary>
        /// Obter todas as temperaturas já registradas para uma cidade
        /// </summary>
        /// <param name="cityId">Nome da cidade</param>
        /// <returns></returns>
        public virtual IEnumerable<Temperature> GetAll(int cityId)
        {
            const string query = "SELECT T.* FROM Temperatures T WHERE T.CityId=@CityId";
            var parameters = AddFilterParameters(new Temperature { CityId = cityId });

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                return dbConnection.Query<Temperature>(query, parameters);
            }
        }

        /// <summary>
        /// Definir parâmetros de busca
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public override DynamicParameters AddFilterParameters(Temperature dto)
        {
            var parameters = new DynamicParameters { RemoveUnused = true };

            if (dto.Id > 0)
                parameters.Add("@Id", dto.Id);
            if (dto.CityId > 0)
                parameters.Add("@CityId", dto.CityId);
            if (dto.Date != DateTime.MinValue)
                parameters.Add("@Date", dto.Date);

            return parameters;
        }

        #endregion
    }
}
