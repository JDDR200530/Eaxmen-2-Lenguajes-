using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Examen_2__Josue_David.DataBase.DTO.Clientes
{
    public class ClienteCreate
    {
        [Column("ClienteId")]
        public Guid ClienteId { get; set; }
        [Display(Name = "Nombre del Cliente")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [Column("cliente_name")]
        public string ClienteName { get; set; }
        [Display(Name = "Identificacion del CLiente")]
        [Required(ErrorMessage = "La {0} requerida ")]
        [Column("identity_number")]
        public int IdentityNumber { get; set; }
        [Display(Name = "Cantidad del Prestamo")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [Column("loan_amount")]
        public double loanAmount { get; set; }
        [Display(Name = "Comision")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [Column("comissiion")]
        public int Comission { get; set; }
        [Display(Name = "Interese")]
        [Required(ErrorMessage = "Los {0} son requeridos")]
        [Column("interert")]
        public decimal Amount { get; set; }

        [Display(Name = "Tiempo de Pago")]
        [Required(ErrorMessage = "El {0} es requido")]
        [Column("term")]
        public int Term { get; set; }
    }
}
