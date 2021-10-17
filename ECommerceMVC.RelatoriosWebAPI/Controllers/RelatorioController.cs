using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerceMVC.RelatoriosWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private static readonly List<String> Relatorio =
            new List<string>() 
            {
                "Primeiro pedido",
                "Segundo pedido"
            };

        // GET: api/<RelatorioController>
        [HttpGet]
        public string Get()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in Relatorio)
            {
                sb.AppendLine(item);
            }
            return sb.ToString();
        }

        // POST api/<RelatorioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Relatorio.Add(value);
        }
    }
}
