using NOTEKEEPER.Api.Entities;

namespace NOTEKEEPER.Api.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> AuthenticateAsync(string username, string password);
}
