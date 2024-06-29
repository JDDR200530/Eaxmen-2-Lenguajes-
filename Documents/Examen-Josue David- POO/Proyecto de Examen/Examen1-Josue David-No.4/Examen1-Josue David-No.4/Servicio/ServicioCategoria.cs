using Examen1_Josue_David_No._4.Controlador;
using Examen1_Josue_David_No._4.Dtos.Categorias;
using Examen1_Josue_David_No._4.Servicio.Interfaces;

namespace Examen1_Josue_David_No._4.Servicio
{
    public class ServicioCategoria : ICategoriesService
    {
        public readonly string _JSON_File;

        public ServicioCategoria()
        {
            _JSON_File = "SeedData/Categorias.json";
        }

        public async Task<List<Pedido>> GetCategoriesListAsync()
        {
            return await ReadCategoriesFromFileAsync();
        }

        private async Task<List<Pedido>> ReadCategoriesFromFileAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Pedido> GetCategoryByIdAsync(Guid id)
        {
            var categoria = await ReadCategoriesFromFileAsync();
            Pedido pedido = categoria.FirstOrDefault(x => x.Id == id);
            return pedido;
        }

        public async Task<bool>CreatAsync(Cliente dto)
        {
            var categoriaDtos = await ReadCategoriesFromFileAsync();

            var pedido = new Pedido
            {
                Id = Guid.NewGuid(),
                IdCliente = Guid.NewGuid(),
                IdProducto = Guid.NewGuid(),

            };

            categoriaDtos.Add(pedido);
            var categorias = categoriaDtos.Select(x => new ControladordeCategorias
            {
                Id = x.Id,
                Name= x.Name,
                Description = x.Description,
            })


        }

        public Task<List<Pedido>> GetCategoriesListAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAsync(Cliente dto, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAsync(Producto dto, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
