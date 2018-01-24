using Dapper;
using Microsoft.Extensions.Configuration;
using Monitoring.Repository.Interfaces;
using Monitoring.Repository.Validation;
using MonitoringApi.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Monitoring.Repository
{
    /// <summary>
    /// Repositório de cidade
    /// </summary>
    public class CityRepository : AbstractRepository<City>
    {
        #region Attributes

        public CityRepository(IConfiguration configuration) : base(configuration) { }

        #endregion

        CityValidation _cityValidation;

        #region properties

        public override ICustomValidation<City> CustomValidation
        {
            get { return _cityValidation != null ? _cityValidation : _cityValidation = new CityValidation(); }
        }

        #endregion

        /// <summary>
        /// obter uma cidade por Id
        /// </summary>
        /// <param name="id">Id da cidade</param>
        /// <returns></returns>
        public override City GetById(int id)
        {
            const string query = "SELECT C.* FROM City C WHERE C.Id = @Id";
            var parameters = AddFilterParameters(new City { Id = id });

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                var record = dbConnection.QueryFirstOrDefault<City>(query, parameters);
                if (record is null)
                    CustomValidation.ValidationErrors.AddMessage("Cidade não encontrada");

                return record;
            }
        }

        /// <summary>
        /// Obter uma cidade por nome
        /// </summary>
        /// <param name="name">nome da cidade</param>
        /// <returns></returns>
        public City GetByName(string name)
        {
            const string query = "SELECT C.* FROM City C where C.Name = @Name";
            var parameters = AddFilterParameters(new City { Name = name });

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                var record = dbConnection.QueryFirstOrDefault<City>(query, parameters);
                if (record is null)
                    CustomValidation.ValidationErrors.AddMessage("Cidade não encontrada");
                return record;
            }
        }

        /// <summary>
        /// Verificar se uma cidade já está cadastrada por nome
        /// </summary>
        /// <param name="cityName">nome da cidade</param>
        /// <returns></returns>
        public bool AlreadyExists(string cityName)
        {
            const string query = "SELECT C.* FROM City C where C.Name = @Name";
            var parameters = AddFilterParameters(new City { Name = cityName });

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                var record = dbConnection.QueryFirstOrDefault<City>(query, parameters);
                return (record != null);
            }
        }

        /// <summary>
        /// Obter cidades paginado
        /// </summary>
        /// <param name="totalRecords">Parâmetro de saída com o total de cidades existentes </param>
        /// <param name="pageIndex">Págian solicitada</param>
        /// <param name="pageSize">Total de itens por página</param>
        /// <returns></returns>
        public IEnumerable<City> GetPaged(out long totalRecords, int pageIndex = 1, int pageSize = 10)
        {
            long totalRec = 0;

            var parameters = AddFilterParameters(new City());
            parameters.Add("@TotalRecords", totalRec, DbType.Int64, ParameterDirection.Output);
            parameters.Add("@PageIndex", pageIndex);
            parameters.Add("@PageSize", pageSize);

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                var result = dbConnection.Query<City>("CityPaging", parameters, commandType: CommandType.StoredProcedure);
                totalRecords = parameters.Get<long>("@TotalRecords");

                return result;
            }
        }

        /// <summary>
        /// metodo para definição de parâmetos
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public override DynamicParameters AddFilterParameters(City dto)
        {
            var parameters = new DynamicParameters { RemoveUnused = true };

            if (dto.Id > 0)
                parameters.Add("@Id", dto.Id);
            if (!string.IsNullOrWhiteSpace(dto.Name))
                parameters.Add("@Name", dto.Name);

            return parameters;
        }
    }
}

