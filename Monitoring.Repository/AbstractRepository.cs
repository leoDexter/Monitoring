using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Monitoring.Repository.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Monitoring.Repository
{
    /// <summary>
    /// Repositório genérico
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractRepository<T> where T : class
    {
        #region Attributes

        private string _connectionString;
        protected string ConnectionString => _connectionString;
        private IConfiguration _configuration;
        protected IConfiguration Configuration => _configuration;

        #endregion

        #region Properties

        public abstract ICustomValidation<T> CustomValidation { get; }

        #endregion

        #region Constructor

        public AbstractRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Insert genérico
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(T item)
        {
            CustomValidation.ValidateInsert(item, Configuration);
            CustomValidation.ValidationErrors.ThrowIfHasErrors();

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Insert(item);
            }
        }

        /// <summary>
        /// Update genérico
        /// </summary>
        /// <param name="item"></param>
        public virtual void Update(T item)
        {
            CustomValidation.ValidateUpdate(item, Configuration);
            CustomValidation.ValidationErrors.ThrowIfHasErrors();

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Update(item);
            }
        }

        /// <summary>
        /// Delete genérico
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(T item)
        {
            CustomValidation.ValidateDelete(item, Configuration);
            CustomValidation.ValidationErrors.ThrowIfHasErrors();

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Delete(item);
            }
        }

        /// <summary>
        /// Delete N genérico
        /// </summary>
        /// <param name="items"></param>
        public virtual void Remove(IEnumerable<T> items)
        {
            items.AsList().ForEach(i => CustomValidation.ValidateDelete(i, Configuration));
            CustomValidation.ValidationErrors.ThrowIfHasErrors();

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                items.AsList().ForEach(i => dbConnection.Delete(i));                
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Metodo abstratro para obter por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract T GetById(int id);

        /// <summary>
        /// Obter todos genérico
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.GetAll<T>();
            }
        }

        /// <summary>
        /// Metodo abstrato para definição de parâmetros
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public abstract DynamicParameters AddFilterParameters(T dto);

        #endregion
    }
}
