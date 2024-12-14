namespace NOTEKEEPER.Api.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    INoteRepository Notes { get; }
    Task<int> SaveChangesAsync();
}
