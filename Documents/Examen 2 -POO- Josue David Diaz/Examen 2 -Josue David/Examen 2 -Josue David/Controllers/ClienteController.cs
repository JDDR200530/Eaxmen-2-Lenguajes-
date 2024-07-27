using Azure;
using Examen_2__Josue_David.DataBase.DTO.Clientes;
using Examen_2__Josue_David.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Service.Interface;

namespace Examen_2__Josue_David.Controllers
{
    [ApiController]
    [Route("/api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServices _clienteServices;

        public ClienteController(IClienteServices clienteServices) 
        {
            this._clienteServices = clienteServices;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<ClienteDto>>> GetOneById(Guid Id)
        {
            var response = await _clienteServices.GetByIdAsync(Id);
            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<ClienteDto>>> Create(ClienteCreate dto)
        {
            var response = await _clienteServices.CreateAsync(dto);
            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }
    }
}
