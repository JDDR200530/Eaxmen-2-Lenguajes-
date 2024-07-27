using AutoMapper;
using Examen_2__Josue_David.DataBase.Context;
using Examen_2__Josue_David.DataBase.DTO.Clientes;
using Examen_2__Josue_David.Entity;
using Examen_2__Josue_David.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Service.Interface;
using System.Linq.Expressions;

namespace Examen_2__Josue_David.Service
{
    public class ClienteService : IClienteServices
    {
        private readonly GestionDeTiendaDbContext _context;
        private readonly ILogger<ClienteService> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public ClienteService(GestionDeTiendaDbContext context, ILogger<ClienteService> logger, IMapper mapper)
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
        }
        public async Task<ResponseDto<ClienteDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var clienteEntity = await _context.Clientes.FirstOrDefaultAsync(o => o.ClienteId == id);
                if (clienteEntity == null)
                {
                    return new ResponseDto<ClienteDto>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = $"El registro {id} no fue encontrado"
                    };
                }

                var clienteDto = _mapper.Map<ClienteDto>(clienteEntity);
                return new ResponseDto<ClienteDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Cliente encontrado",
                    Data = clienteDto,
                };


            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error al obtener el cliente");
                return new ResponseDto<ClienteDto>
                {

                    StatusCode = 500,
                    Status = false,
                    Message = $"Se produjo un error al obtener el cliente"
                };
            }
        }

        public async Task<ResponseDto<ClienteDto>> CreateAsync(ClienteCreate dto)
        {
            var clienteEntity = _mapper.Map<Cliente>(dto);
            clienteEntity.ClienteId = new Guid();

            _context.Clientes.Add(clienteEntity); 

            await _context.SaveChangesAsync();

            var clienteDto = _mapper.Map<ClienteDto>(clienteEntity);
            return new ResponseDto<ClienteDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "El pedido sea  creado correctamnete",
                Data = clienteDto
            };
        }
    }
}
