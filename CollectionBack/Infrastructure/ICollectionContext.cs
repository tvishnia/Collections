namespace CollectionBasic.Infrastructure;

public interface ICollectionContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}