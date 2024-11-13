using Azure;
using Examen_2_Lenguajes.Dto.CuentaContable;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace Examen_2_Lenguajes.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaContableService cuentaContableService;

        public CuentasController(ICuentaContableService cuentaContableService)
        
        {
            this.cuentaContableService = cuentaContableService;
        }

        [HttpGet]
        public async Task<ActionResult<Response<CuentaContableDto>>> GetAll()
        {
            var response = await cuentaContableService.GetCuentasListAsync();
            return StatusCode(response.StatusCode, response);
        }
    }
}
