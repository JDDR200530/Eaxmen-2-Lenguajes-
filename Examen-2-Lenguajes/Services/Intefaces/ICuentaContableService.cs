using Examen_2_Lenguajes.Dto.Common;
using Examen_2_Lenguajes.Dto.CuentaContable;

namespace Examen_2_Lenguajes.Services.Intefaces
{
    public interface ICuentaContableService
    {
        Task<ResponseDto<CuentaContableDto>> EditAsync(CuentaContableEditDto dto);
        Task<ResponseDto<List<CuentaContableDto>>> GetCuentasListAsync();
    }
}
