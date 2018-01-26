using MonitoringApi.dto.Interfaces;
using System.Collections.Generic;

namespace Monitoring.ExternalAdapters.Interfaces
{
    /// <summary>
    /// Define cokmo usa classe de mapeamento deve ser
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResponseMapper<T> where T : IEntity
    {
        /// <summary>
        /// Deve possuir uma função que converte o objeto mapeado em um objeto conhecido pelo repositório
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> ConvertResponseMapper();
    }
}
