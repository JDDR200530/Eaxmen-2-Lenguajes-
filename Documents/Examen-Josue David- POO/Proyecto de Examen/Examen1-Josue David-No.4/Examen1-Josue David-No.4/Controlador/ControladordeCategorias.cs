using Examen1_Josue_David_No._4.Dtos.Categorias;
using Examen1_Josue_David_No._4.Entidades;
using Examen1_Josue_David_No._4.Servicio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Examen1_Josue_David_No._4.Controlador
{
    [ApiController]
    [Route("api/categoria")]
    public class ControladordeCategorias : ControllerBase
    {
        private List<EntidadCategoria> _categorias;
        private readonly ICategoriesService _categoriaServicio;
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _categoriaServicio.GetCategoriesListAsync());
        }

        private ActionResult Ok(object value)
        {
            throw new NotImplementedException();
        }

        [HttpGet("id")]

        public async Task<ActionResult> Get(Guid id)
        {
            var categoria = await _categoriaServicio.GetCategoriesListAsync(id);
            if (categoria == null)
            {
                return NotFound(new { Message = $"No se encontró el pedido: {id}" });
            }
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult> Cliente(Cliente dto, Guid id)
        {
            var resultado = await _categoriaServicio.CreateAsync(dto, id);
            if (!resultado)
            {
                return NotFound();
            }
            return Ok(resultado);

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(Guid id)
        {
            var category = await _categoriaServicio.GetCategoriesListAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            await _categoriaServicio.DeleteAsync(id);
            return Ok(category);
        }

    }
}
