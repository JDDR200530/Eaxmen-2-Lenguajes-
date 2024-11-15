
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

            // Solo aplicar colación a las columnas de tipo string
            modelBuilder.Entity<PartidaEntity>()
                .Property(e => e.Description)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            

            // Configurar el comportamiento de las claves foráneas para restringir el borrado en cascada
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }

        public DbSet<PartidaEntity> Partidas { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CuentaContableEntity> CuentaContables { get; set; }
        public DbSet<SaldoEntity> SaldoContable { get; set; }
    }




}

