using Examen_2_Lenguajes.Entity;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Examen_2_Lenguajes.Database.Context
{
    public class PartidasDbContext : IdentityDbContext<UserEntity>
    {
        private readonly IAuditServices _auditServices;

        public PartidasDbContext(DbContextOptions<PartidasDbContext> options, IAuditServices auditServices)
            : base(options)
        {
            _auditServices = auditServices ?? throw new ArgumentNullException(nameof(auditServices));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<PartidaEntity>().Property(e => e.NumPartida)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS"); 

            modelBuilder.HasDefaultSchema("security");

            // Mapear tablas con nombres específicos
            modelBuilder.Entity<UserEntity>().ToTable("users");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("users_logins"); 

           
            var entityTypes = modelBuilder.Model.GetEntityTypes();
            foreach (var type in entityTypes)
            {
                var foreignKeys = type.GetForeignKeys();
                foreach (var foreignKey in foreignKeys)
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            userId ??= _auditServices.GetUserId();
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = entry.Entity as BaseEntity;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedBy = userId ?? "System";
                    entity.CreatedData = DateTime.UtcNow; // Mejor usar UtcNow
                }
                else
                {
                    entity.UpdatedBy = userId ?? "System";
                    entity.UpdatedData = DateTime.UtcNow; // Mejor usar UtcNow
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        // Definición de DbSets
        public DbSet<PartidaEntity> Partidas { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CuentaContableEntity> CuentaContables { get; set; }
    }

}
