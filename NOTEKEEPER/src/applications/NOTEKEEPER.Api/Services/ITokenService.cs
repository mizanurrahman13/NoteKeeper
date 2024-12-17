using NOTEKEEPER.Api.Entities;

namespace NOTEKEEPER.Api.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}


