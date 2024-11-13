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
            _auditServices = auditServices;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Establecer la colación por defecto para las columnas de tipo string
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            // Colación específica para las propiedades de tipo string, no aplicable para 'int'
            modelBuilder.Entity<PartidaEntity>()
                .Property(e => e.Description)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            // Definir el esquema por defecto
            modelBuilder.HasDefaultSchema("security");

            // Mapear las tablas con nombres específicos
            modelBuilder.Entity<UserEntity>().ToTable("users");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("users_logins");

            // Configuración de las relaciones entre las entidades

            // Relación de PartidaEntity con CuentaContableEntity
            modelBuilder.Entity<PartidaEntity>()
                .HasOne(p => p.CuentaContable)
                .WithMany(c => c.Partida)
                .HasForeignKey(p => p.CodigoCuenta)
                .HasPrincipalKey(c => c.CodigoCuenta)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación de PartidaEntity con UserEntity (Creado por Usuario)
            modelBuilder.Entity<PartidaEntity>()
                .HasOne(p => p.CreatedByUser)
                .WithMany() // No es necesario especificar la colección inversa
                .HasForeignKey(p => p.UserId) // Usando la misma clave foránea UserId
                .HasPrincipalKey(u => u.Id) // Id es la clave primaria en UserEntity (string)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación de PartidaEntity con UserEntity (Actualizado por Usuario)
            modelBuilder.Entity<PartidaEntity>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany() // No es necesario especificar la colección inversa
                .HasForeignKey(p => p.UserId) // Usando la misma clave foránea UserId
                .HasPrincipalKey(u => u.Id) // Usando la misma clave principal
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar la eliminación en cascada para todas las claves foráneas
            var entityTypes = modelBuilder.Model.GetEntityTypes();
            foreach (var type in entityTypes)
            {
                var foreignKeys = type.GetForeignKeys();
                foreach (var foreignKey in foreignKeys)
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict; // Evitar eliminación en cascada
                }
            }

            // Configuración de las claves primarias de cada entidad
            modelBuilder.Entity<PartidaEntity>()
                .HasKey(p => p.NumPartida); // Usar NumPartida como clave primaria

            modelBuilder.Entity<CuentaContableEntity>()
                .HasKey(c => c.CodigoCuenta); // Definir clave primaria para CuentaContableEntity
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            userId ??= _auditServices.GetUserId();  // Obtener el ID del usuario desde el servicio de auditoría

            // Obtener las entidades que se han agregado o modificado
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            // Asignar valores de auditoría (quién creó y actualizó)
            foreach (var entry in entries)
            {
                var entity = entry.Entity as BaseEntity;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedBy = userId ?? "System";
                    entity.CreatedDate = DateTime.UtcNow; // Usar la hora UTC
                }
                else
                {
                    entity.UpdatedBy = userId ?? "System";
                    entity.UpdatedDate = DateTime.UtcNow; // Usar la hora UTC
                }
            }

            // Guardar cambios en la base de datos
            return await base.SaveChangesAsync(cancellationToken);
        }

        // Definición de DbSets para las entidades
        public DbSet<PartidaEntity> Partidas { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CuentaContableEntity> CuentaContables { get; set; }
    }






}
