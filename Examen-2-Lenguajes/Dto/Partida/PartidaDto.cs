using Examen_2_Lenguajes.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen_2_Lenguajes.Dto.Partida
{
    public class PartidaDto
    {
        public Guid PartidaId { get; set; }
        public int NumPartida { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]

        // Referencia a la cuenta contable asociada
        public int CodigoCuenta { get; set; }
        [ForeignKey(nameof(CodigoCuenta))]
        public string NombreCuenta { get; set; }
        [ForeignKey(nameof(NombreCuenta))]
        // Relaciones
        public virtual CuentaContableEntity CuentaContable { get; set; }
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
