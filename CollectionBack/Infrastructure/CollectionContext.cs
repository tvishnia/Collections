using CollectionBasic.Domain;
using Microsoft.EntityFrameworkCore;

namespace CollectionBasic.Infrastructure;

public class CollectionContext : 
    DbContext, 
    ICollectionContext
{
    public DbSet<PictureFileInfo> PictureFileInfos => Set<PictureFileInfo>();
    public DbSet<SpoonInfo> SpoonInfos => Set<SpoonInfo>();
    
    public CollectionContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}