using Examen1_Josue_David_No._4.Controlador;
using Examen1_Josue_David_No._4.Dtos.Categorias;
using Examen1_Josue_David_No._4.Servicio.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Examen1_Josue_David_No._4.Servicio
{
    public class ServicioCategoria : ICategoriesService
    {
        private readonly string _JSON_File;

        public ServicioCategoria()
        {
            _JSON_File = "SeedData/Categorias.json";
        }

        public async Task<List<Pedido>> GetCategoriesListAsync()
        {
            return await ReadCategoriesFromFileAsync();
        }

        public async Task<Pedido> GetCategoryByIdAsync(Guid id)
        {
            var categorias = await ReadCategoriesFromFileAsync();
            Pedido pedido = categorias.FirstOrDefault(x => x.Id == id);
            return pedido;
        }

        public async Task<bool> CreateAsync(Cliente dto)
        {
            try
            {
                var categorias = await ReadCategoriesFromFileAsync();

                var pedido = new Pedido
                {
                    Id = Guid.NewGuid(),
                    IdCliente = dto.IdCliente, 
                 
                };

                categorias.Add(pedido);

                var categoriasControlador = categorias.Select(x => new ControladordeCategorias
                {
                    
                    
                }).ToList();

                await WriteCategoriesToFileAsync(categoriasControlador);

                return true;
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error en CreateAsync: {ex.Message}");
                return false;
            }
        }

        private async Task<List<Pedido>> ReadCategoriesFromFileAsync()
        {
            try
            {
                using (FileStream fs = new FileStream(_JSON_File, FileMode.Open, FileAccess.Read))
                {
                    return await JsonSerializer.DeserializeAsync<List<Pedido>>(fs);
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones adecuado
                Console.WriteLine($"Error en ReadCategoriesFromFileAsync: {ex.Message}");
                return new List<Pedido>();
            }
        }

        private async Task WriteCategoriesToFileAsync(List<ControladordeCategorias> categorias)
        {
            try
            {
                using (FileStream fs = new FileStream(_JSON_File, FileMode.Create, FileAccess.Write))
                {
                    await JsonSerializer.SerializeAsync(fs, categorias);
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error en WriteCategoriesToFileAsync: {ex.Message}");
            }
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
