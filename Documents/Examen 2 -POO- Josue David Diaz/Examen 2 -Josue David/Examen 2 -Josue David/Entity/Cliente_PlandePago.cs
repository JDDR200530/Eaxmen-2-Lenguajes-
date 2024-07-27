using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen_2__Josue_David.Entity
{
    [Table("cliente_planepago", Schema = "dto")]
    public class Cliente_PlandePago
    {
        [Key]
        [Column("ClienteId")]
        public Guid ClienteId { get; set; }

        [Column("No")]
        public int No { get; set; }
        [ForeignKey(nameof(No))]
        [Column("Date_Time")]
        public DateTime DateTime { get; set; }
        [ForeignKey(nameof(DateTime))]

        [Column("days")]
        public int Days { get; set; }
        [ForeignKey(nameof(Days))]

        [Column("intrest")]
        public int intrest { get; set; }
        [ForeignKey(nameof(intrest))]

        [Column("pago_principal")]
        public int PagoPrincipal { get; set; }
        [ForeignKey(nameof(PagoPrincipal))]

        [Column("seguro_de_vida")]
        public double SegurodeVida { get; set; }
        [ForeignKey(nameof(SegurodeVida))]

        [Column("balance_Principal")]
        public double BalancePrincipal { get; set; }
        [ForeignKey(nameof(BalancePrincipal))]

        public PlandePago PlandePago { get; set; }



    }
}

