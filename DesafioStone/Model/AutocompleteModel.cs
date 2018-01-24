using MonitoringApi.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioStone.Model
{
    /// <summary>
    /// Representa o retorno para autocomplete
    /// </summary>
    public class AutocompleteModel
    {
        /// <summary>
        /// Nomes das cidades encontradas
        /// </summary>
        public List<string> Result { get; set; }

        /// <summary>
        /// Cria uma lista com os nomes das cidades encontratas
        /// </summary>
        /// <param name="cities">Cidades encontradas</param>
        public AutocompleteModel(IEnumerable<City> cities)
        {
            Result = new List<string>();

            if (cities != null)
                foreach (var item in cities)
                    Result.Add(item.Name);
        }
    }
}
