using System.ComponentModel.DataAnnotations;

namespace Examen1_Josue_David_No._4.Dtos.Categorias
{
    public class Cliente
    {
        [Display(Name = "IdCliente")]
        [Required(ErrorMessage = "El {0} de la categoria es requerido")]
        public Guid IdCliente { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage ="El {0} de la categoria es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "El {0} de la categoria es requerido")]
        public string Email { get; set; }
    }
}
