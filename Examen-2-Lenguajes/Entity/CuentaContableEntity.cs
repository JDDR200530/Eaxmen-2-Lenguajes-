using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen_2_Lenguajes.Entity
{
    public enum TipoMovimiento
    {
        Debe,
        Haber
    }
    [Table("cuenta", Schema = "dbo")]

    public class CuentaContableEntity : BaseEntity
    {
        [Key]
        [Column("codigos_cuentas")]
        public int CodigoCuenta { get; set; }
        [Column("nombre_cuenta")]
        public string NombreCuenta { get; set; }
        [Column("cantidad")]
        public decimal Monto { get; set; }
        [Column("tipo_movimiento")]
        public TipoMovimiento Movimiento { get; set; }

        // Lista de cuentas hijas o subdivisiones
        public virtual List<CuentaContableEntity> Subdivisiones { get; set; }
        public virtual IEnumerable<PartidaEntity> Partida { get; set; }

        public CuentaContableEntity()
        {
            Subdivisiones = new List<CuentaContableEntity>();
        }
    }


}
