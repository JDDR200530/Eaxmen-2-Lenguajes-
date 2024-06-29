using System.ComponentModel.DataAnnotations;

namespace Examen1_Josue_David_No._4.Dtos.Categorias
{
    public class Producto
    {
        [Display(Name = "IdProducto")]
        [Required (ErrorMessage = "El {0} de la categoria es requerido")]
        public int IdProducto { get; set; }
        [Display(Name="Nombre")]
        [Required(ErrorMessage = "El {0} de la categoria es requerido")]
        public string NombreProducto { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El {0} de la categoria es requerido")]
        public int Precio {  get; set; }
    }
}
