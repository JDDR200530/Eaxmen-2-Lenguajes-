using Examen_2__Josue_David.DataBase.DTO.Clientes;
using Proyecto_Poo.Dtos.Common;

namespace Examen_2__Josue_David.Service.Interface
{
    public interface IClienteServices
    {
        Task<ResponseDto<ClienteDto>> CreateAsync(ClienteCreate dto);
        Task<ResponseDto<ClienteDto>> GetByIdAsync(Guid id);
    }
}
