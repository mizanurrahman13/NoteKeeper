using MediatR;
using NOTEKEEPER.Api.Repositories;
using NOTEKEEPER.Api.Services;

namespace NOTEKEEPER.Api.Commands;

public class LoginUserCommand : IRequest<LoginResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public string Email { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; }
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.AuthenticateAsync(request.Email, request.Password);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var token = _tokenService.GenerateToken(user);

        return new LoginResponse
        {
            Email = user.Email,
            UserId = user.Id,
            Token = token
        };
    }
}

