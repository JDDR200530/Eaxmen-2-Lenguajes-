using System.ComponentModel.DataAnnotations.Schema;

namespace Examen_2_Lenguajes.Entity
{
    public class PartidaEntity
    {
        public Guid PartidaId { get; set; }

        public int NumPartida { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]

        public int CodigoCuenta { get; set; }
        [ForeignKey(nameof(CodigoCuenta))]

        public string NombreCuenta { get; set; }
        [ForeignKey(nameof(NombreCuenta))]
       
        public decimal Debe { get; set; }

        public decimal Haber { get; set; }


        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    
    } 
}
