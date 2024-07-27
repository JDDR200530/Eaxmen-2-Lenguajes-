using Examen_2__Josue_David.Entity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Service.Interface;
using System.Data;

namespace Examen_2__Josue_David.DataBase.Context
{
    public class GestionDeTiendaDbContext : DbContext
    {
        private readonly IAuthService _authService;
        public GestionDeTiendaDbContext(DbContext options, IAuthService authService)
        {
            _authService = authService;
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is PlandePago && (e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach (var entry in entries) 
            {
                var entity = entry.Entity as PlandePago;
                if (entity != null)
                {
                   
                }
            }   
            return base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PlandePago> PlandePagos { get; set; }
        public DbSet <Cliente_PlandePago> Cliente_Plandes { get; set; }
    }
}
