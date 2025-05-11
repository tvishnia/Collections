using CollectionBasic.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectionBasic.Infrastructure.EntityTypeConfigurations;

internal class PictureFileInfoConfiguration : IEntityTypeConfiguration<PictureFileInfo>
{
    public void Configure(EntityTypeBuilder<PictureFileInfo> builder)
    {
        builder.ToTable(nameof(CollectionContext.PictureFileInfos));
        builder.HasKey(entity => entity.Id);
        builder.Property(entity => entity.Id).ValueGeneratedNever();
        
    }
}