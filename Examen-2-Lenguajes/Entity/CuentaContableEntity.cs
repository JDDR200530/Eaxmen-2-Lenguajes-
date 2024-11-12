namespace Examen_2_Lenguajes.Entity
{
    public class CuentaContableEntity
    {
        public Guid CuentaId { get; set; }

        public int CodigoCuenta {  get; set; }

        public string NombreCuenta { get; set; }

        public virtual IEnumerable<PartidaEntity> Partida { get; set; }

    }
}
