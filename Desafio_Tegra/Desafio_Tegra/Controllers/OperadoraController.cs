using System;
using System.Collections.Generic;
using Desafio_Tegra.Lib.Models;
using Desafio_Tegra.Lib.Business;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Tegra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadoraController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Aeroporto> GetAllAirPort() => OperadoraBusiness.GetAllAirports();
      
        [HttpPost]
        public Voo GetFlyWithScale([FromBody] dynamic obj){
            return OperadoraBusiness.GetFlyWithScale(Convert.ToString(obj.origem), Convert.ToString(obj.destino),Convert.ToDateTime(obj.data));
        }
            
    }
}