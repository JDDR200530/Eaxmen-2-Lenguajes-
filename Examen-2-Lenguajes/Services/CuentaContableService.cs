using AutoMapper;
using Examen_2_Lenguajes.Database.Context;
using Examen_2_Lenguajes.Dto.Common;
using Examen_2_Lenguajes.Dto.CuentaContable;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace Examen_2_Lenguajes.Services
{
    public class CuentaContableService : ICuentaContableService
    {
        private readonly PartidasDbContext _context;
        private readonly ILogger<PartidaSeeder> _logger;
        private readonly IMapper _mapper;

        public CuentaContableService(PartidasDbContext context, ILogger<PartidaSeeder> logger,IMapper mapper)
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
        }

        public async Task<ResponseDto<List<CuentaContableDto>>> GetCuentasListAsync()
        {
            var cuentasEntity = await _context.CuentaContables.ToListAsync();
            var cuentaDtos = _mapper.Map<List<CuentaContableDto>>(cuentasEntity);

            return new ResponseDto<List<CuentaContableDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Lista de Cuentas contables",
                Data = cuentaDtos
            };
        }

        public async Task<ResponseDto<CuentaContableDto>> EditAsync(CuentaContableEditDto dto)
        {
            // Crear un objeto de respuesta inicial
            var response = new ResponseDto<CuentaContableDto>();

            try
            {
                // Buscar la cuenta contable por el CódigoCuenta
                var cuenta = await _context.CuentaContables
                    .FirstOrDefaultAsync(c => c.CodigoCuenta == dto.CodigoCuenta);

                if (cuenta == null)
                {
                    response.Status = false;
                    response.Message = "La cuenta contable no fue encontrada.";
                    return response;
                }

                // Modificar solo el monto
                cuenta.Monto = dto.Monto;

                // Guardar los cambios en la base de datos
                _context.CuentaContables.Update(cuenta);
                await _context.SaveChangesAsync();

                // Preparar la respuesta de éxito
                response.Status = true;
                response.Message = "El monto de la cuenta contable fue actualizado exitosamente.";
                response.Data = new CuentaContableDto
                {
                    CodigoCuenta = cuenta.CodigoCuenta,
                    NombreCuenta = cuenta.NombreCuenta,
                    Monto = cuenta.Monto,
                    Movimiento = cuenta.Movimiento
                };
            }
            catch (Exception ex)
            {
                // Manejo de errores y creación de una respuesta de error
                response.Status = false;
                response.Message = $"Ocurrió un error al actualizar la cuenta: {ex.Message}";
            }

            return response;
        }

    }


}
