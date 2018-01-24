using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioStone.Settings
{
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
    }
}
