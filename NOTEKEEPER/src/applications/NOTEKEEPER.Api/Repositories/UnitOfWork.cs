using NOTEKEEPER.Api.Contexts;

namespace NOTEKEEPER.Api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly NoteKeeperContext _context;

    public UnitOfWork(NoteKeeperContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
        Notes = new NoteRepository(_context);
    }

    public IUserRepository Users { get; private set; }
    public INoteRepository Notes { get; private set; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
