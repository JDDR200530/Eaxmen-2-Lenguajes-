using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen_2_Lenguajes.Entity
{
    [Table ("Particiones", Schema = "dbo")]
    public class PartidaEntity : BaseEntity
    {
        

        [Column("numero_partida")]
        public int NumPartida { get; set; }

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("id_user")]
        public string UserId { get; set; }  // Cambiar de Guid a string
        [ForeignKey(nameof(UserId))]
        public virtual UserEntity CreatedByUser { get; set; }

        [Column("codigo_cuenta")]
        public int CodigoCuenta { get; set; }
        [ForeignKey(nameof(CodigoCuenta))]
        public virtual CuentaContableEntity CuentaContable { get; set; }

        [Column("nombre_cuenta")]
        public string NombreCuenta { get; set; }

        public virtual UserEntity UpdatedByUser { get; set; }
    }

}
