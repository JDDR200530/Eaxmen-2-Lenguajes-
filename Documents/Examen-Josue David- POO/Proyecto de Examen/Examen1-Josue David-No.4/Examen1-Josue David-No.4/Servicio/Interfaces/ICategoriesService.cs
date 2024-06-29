using Examen1_Josue_David_No._4.Dtos.Categorias;

namespace Examen1_Josue_David_No._4.Servicio.Interfaces
{
    public interface ICategoriesService
    {
        Task<List<Pedido>> GetCategoriesListAsync();

        Task<List<Pedido>> GetCategoriesListAsync(Guid id);

        Task<bool> CreateAsync(Cliente dto, Guid id);
        Task<bool> CreateAsync(Producto dto, Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> CreateAsync(Cliente dto);
    }
}
