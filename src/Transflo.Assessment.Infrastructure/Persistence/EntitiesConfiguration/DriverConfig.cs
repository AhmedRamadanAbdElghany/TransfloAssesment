using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transflo.Assessment.Core.Domain;

namespace Transflo.Assessment.Infrastructure.Persistence.EntitiesConfiguration
{
    internal class DriverConfig : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.Property(p => p.LastName).HasMaxLength(PersistenceLayerConstants.EntityColumnSizeConstants.ShortTextLength);
            builder.Property(p => p.FirstName).HasMaxLength(PersistenceLayerConstants.EntityColumnSizeConstants.ShortTextLength);
            builder.Property(p => p.PhoneNumber).HasMaxLength(PersistenceLayerConstants.EntityColumnSizeConstants.ShortTextLength);
            builder.Property(p => p.EmailAddress).HasMaxLength(PersistenceLayerConstants.EntityColumnSizeConstants.EmailTextLength);
        }
    }
}
