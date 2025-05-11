using Collections.DB.Domain;
using Microsoft.EntityFrameworkCore;

namespace Collections.DB.Infrastructure;

public class CollectionContext(DbContextOptions options) :
    DbContext(options),
    ICollectionContext
{
    public DbSet<PictureFileInfo> PictureFileInfos => Set<PictureFileInfo>();
    // public DbSet<SpoonInfo> SpoonInfos => Set<SpoonInfo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}