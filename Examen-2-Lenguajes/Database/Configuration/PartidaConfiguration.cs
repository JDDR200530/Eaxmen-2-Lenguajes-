using Examen_2_Lenguajes.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Examen_2_Lenguajes.Database.Configuration
{
    public class PartidaConfiguration : IEntityTypeConfiguration<PartidaEntity>
    {

        public void Configure(EntityTypeBuilder<PartidaEntity> builder)
        {
            builder.HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedBy)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();

            builder.HasOne(e => e.UpdatedByUser)
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();
                
        }
    }
}
