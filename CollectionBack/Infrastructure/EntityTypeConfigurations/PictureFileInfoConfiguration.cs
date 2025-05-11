// using ComparerBasic.Domain;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace ComparerBasic.Infrastructure.EntityTypeConfigurations;
//
// internal class PictureFileInfoConfiguration : IEntityTypeConfiguration<PictureFileInfo>
// {
//     public void Configure(EntityTypeBuilder<PictureFileInfo> builder)
//     {
//         builder.ToTable(nameof(PicServiceContext.PictureFileInfos));
//         builder.HasKey(entity => entity.Id);
//         builder.Property(entity => entity.Id).ValueGeneratedNever();
//         
//     }
// }