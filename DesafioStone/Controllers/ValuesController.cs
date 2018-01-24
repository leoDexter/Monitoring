using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DesafioStone.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        /// <summary>
        /// Coloca o swagger como página inicial
        /// </summary>
        /// <returns>Redirect para ~/swagger</returns>
        [HttpGet]
        public ActionResult get()
        {
            return Redirect("~/swagger");
        }
    }
}
