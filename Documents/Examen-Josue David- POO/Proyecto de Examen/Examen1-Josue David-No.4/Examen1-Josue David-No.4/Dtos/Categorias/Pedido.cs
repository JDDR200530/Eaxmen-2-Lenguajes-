using System.ComponentModel.DataAnnotations;

namespace Examen1_Josue_David_No._4.Dtos.Categorias
{
    public class Pedido
    {
            public Guid Id { get; set; }
           public Guid IdProducto { get; set; }

           public Guid IdCliente { get; set; }

            public string Fecha { get; set; }

            public string ListadeProductos { get; set; }
            
            public int Total {  get; set; }
    }
}
