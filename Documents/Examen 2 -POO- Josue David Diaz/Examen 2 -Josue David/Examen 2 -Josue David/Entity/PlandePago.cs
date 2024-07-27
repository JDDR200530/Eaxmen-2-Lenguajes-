using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen_2__Josue_David.Entity
{
    [Table("plan_de_pago", Schema =("dto"))]
    public class PlandePago
    {
        [Key]

        public int No { get; set; }
       public DateTime DateTime {get; set; }      

       public int Days { get; set; }

       public int intrest { get; set; }

        public int PagoPrincipal { get; set; }

        public double SegurodeVida {  get; set; }

        public double BalancePrincipal { get; set; }

        public virtual IEnumerable<Cliente_PlandePago> Pagos { get; set; }
      

    }
}
