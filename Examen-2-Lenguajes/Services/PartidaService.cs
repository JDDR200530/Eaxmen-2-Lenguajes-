using AutoMapper;
using Examen_2_Lenguajes.Database.Context;
using Examen_2_Lenguajes.Dto.Common;
using Examen_2_Lenguajes.Dto.Partida;
using Examen_2_Lenguajes.Entity;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace Examen_2_Lenguajes.Services
{
    public class PartidaService : IPartidaService
    {
        private readonly PartidasDbContext context;
        private readonly ILogger<PartidaService> logger;
        private readonly IMapper _mapper;
        private readonly IAuthService authService;

        public PartidaService(PartidasDbContext context, ILogger<PartidaService> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this._mapper = mapper;
        }

        // Firma del método cambiada para devolver un solo PartidaDto


        public async Task<ResponseDto<PartidaDto>> GetByIdAsync(int numPartida)
        {
            try
            {
                // Se busca la entidad por el número de partida (NumPartida)
                var partidaEntity = await context.Partidas.FirstOrDefaultAsync(o => o.NumPartida == numPartida);

                if (partidaEntity == null)
                {
                    // Si no se encuentra la partida, devolver un mensaje de error
                    return new ResponseDto<PartidaDto>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = $"La partida con el número {numPartida} no fue encontrada."
                    };
                }

                // Mapear la entidad a un DTO
                var partidaDto = _mapper.Map<PartidaDto>(partidaEntity);

                // Devolver la respuesta con los datos de la partida
                return new ResponseDto<PartidaDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Partida encontrada",
                    Data = partidaDto
                };
            }
            catch (Exception e)
            {
                // Manejo de excepciones y log
                logger.LogError(e, "Error al obtener la partida por número de partida.");
                return new ResponseDto<PartidaDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Se produjo un error al obtener la partida"
                };
            }
        }

        public async Task<ResponseDto<PartidaDto>> CreatePartidaAsync(PartidaCreatedDto dto)
        {
            // Mapear el DTO a la entidad de la partida
            var partidaEntity = _mapper.Map<PartidaEntity>(dto);

            

            context.Partidas.Add(partidaEntity);  // Agregar la nueva partida al contexto

            // Guardar los cambios en la base de datos
            await context.SaveChangesAsync();

            // Mapear la entidad creada a un DTO para la respuesta
            var partidaDto = _mapper.Map<PartidaDto>(partidaEntity);

            // Devolver la respuesta con el estado 201 (creado)
            return new ResponseDto<PartidaDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "La partida se ha creado correctamente",
                Data = partidaDto
            };
        }

        public async Task<ResponseDto<PartidaDto>> EditAsync(PartidaEditDto dto, int numPartida)
        {
            // Buscar la partida por el número de partida (NumPartida)
            var partidaEntity = await context.Partidas.FirstOrDefaultAsync(o => o.NumPartida == numPartida);

            // Si no se encuentra la partida, devolver un error
            if (partidaEntity == null)
            {
                return new ResponseDto<PartidaDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = $"La partida con el número {numPartida} no fue encontrada"
                };
            }

            // Mapear el DTO de edición a la entidad
            _mapper.Map(dto, partidaEntity);
            partidaEntity.FechaCreacion = DateTime.Now;  // Actualizar la fecha de creación (si es necesario)

            // Actualizar la partida en el contexto
            context.Partidas.Update(partidaEntity);
            await context.SaveChangesAsync();  // Guardar los cambios

            // Mapear la entidad actualizada a un DTO para la respuesta
            var partidaDto = _mapper.Map<PartidaDto>(partidaEntity);

            // Devolver la respuesta con el estado 200 (OK)
            return new ResponseDto<PartidaDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "La partida se ha modificado correctamente",
                Data = partidaDto
            };
        }

    }

}
