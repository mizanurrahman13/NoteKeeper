using Microsoft.EntityFrameworkCore;
using NOTEKEEPER.Api.Contexts;
using NOTEKEEPER.Api.Entities;

namespace NOTEKEEPER.Api.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(NoteKeeperContext context) : base(context)
    {
    }

    public async Task<User> AuthenticateAsync(string username, string password)
    { 
         return await _context.Users.FirstOrDefaultAsync(u => u.Email == username); }
    }
