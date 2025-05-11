namespace Collections.DB.Infrastructure;

public interface ICollectionContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}