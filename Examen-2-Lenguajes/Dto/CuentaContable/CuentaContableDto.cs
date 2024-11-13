using Examen_2_Lenguajes.Entity;

namespace Examen_2_Lenguajes.Dto.CuentaContable
{
    public class CuentaContableDto
    {
        public int CodigoCuenta { get; set; }
        public string NombreCuenta { get; set; }
        public decimal Monto { get; set; }
        public TipoMovimiento Movimiento { get; set; }

    }
}
