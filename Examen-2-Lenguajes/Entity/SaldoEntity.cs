using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen_2_Lenguajes.Entity
{
    [Table ("saldo", Schema = "dbo")]
    public class SaldoEntity
    {
        [Key]
        [Column("fecha")]
        public DateTime Fecha { get; set; }
        [Column("codigo_cuenta")]
        public int CodigoCuenta { get; set; }
        [ForeignKey(nameof(CodigoCuenta))]
        [Column("nombre_cuenta")]
        public string NombreCuenta { get; set; }
        [ForeignKey(nameof(NombreCuenta))]

        [Column("monto")]
        public decimal Monto { get; set; }
        [ForeignKey(nameof(Monto))]

       public virtual CuentaContableEntity CuentaContable { get; set; }

        public virtual UserEntity CreatedByUser { get; set; }

        public virtual UserEntity UpdatedByUser { get;set; }

    }
}
