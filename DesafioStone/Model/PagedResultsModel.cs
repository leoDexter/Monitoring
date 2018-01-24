using MonitoringApi.dto;
using MonitoringApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioStone.Model
{
    /// <summary>
    /// Representa o retorno de cidades paginado
    /// </summary>
    public class PagedResultsModel
    {
        /// <summary>
        /// Informa qual é a págian atual
        /// </summary>
        public int Currentpage { get; set; }

        /// <summary>
        /// Informa a quantidade total de registros existentes
        /// </summary>
        public long TotalRecords { get; set; }

        /// <summary>
        /// Total de itens exibidos por página
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Resultados da paginação
        /// </summary>
        public List<CityTemperatureModel> Results { get; set; }

        /// <summary>
        /// Total de páginas
        /// </summary>
        public decimal Totalpages { get { return Convert.ToInt32(Math.Ceiling((decimal)(TotalRecords / PageSize))); } }

        /// <summary>
        /// Retorna um modelo paginado
        /// </summary>
        /// <param name="results">Lista pagianda de cidades</param>
        /// <param name="currentPage">Página atual</param>
        /// <param name="pageSize">Quantidade de itens por página</param>
        /// <param name="totalRecords">Quantidade total de registros encontrados</param>
        public PagedResultsModel(IEnumerable<City> results, int currentPage, int pageSize, long totalRecords)
        {
            Results = new List<CityTemperatureModel>();

            if (results != null)
                (results as List<City>).ForEach(c => Results.Add(new CityTemperatureModel(c)));

            PageSize = pageSize;
            Currentpage = currentPage;
            TotalRecords = totalRecords;
        }
    }
}
