using NOTEKEEPER.Api.Contexts;
using NOTEKEEPER.Api.Entities;

namespace NOTEKEEPER.Api.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(NoteKeeperContext context) : base(context)
    {
    }

    // Add any user-specific methods here if needed
}
