using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioStone.Settings
{
    /// <summary>
    /// Configurações de funcionamento do sistema
    /// </summary>
    public class GeneralSettings
    {
        /// <summary>
        /// Intervalo em minutos que o serviço de atualização de temperaturas irá aguardar entre uma execução e outra
        /// </summary>
        public int MonitoringServiceInterval { get; set; }

        /// <summary>
        /// Define a quantidade de itens por página na consulta paginada
        /// </summary>
        public int PaginationSize { get; set; }

        /// <summary>
        /// Parâmetro para o retorno das temperaturas das últimas XX horas da cidade informada
        /// </summary>
        public int Latest { get; set; }
    }
}
