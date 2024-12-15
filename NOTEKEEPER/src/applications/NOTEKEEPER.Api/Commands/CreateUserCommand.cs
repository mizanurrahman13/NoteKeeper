using MediatR;
using NOTEKEEPER.Api.Entities;
using NOTEKEEPER.Api.Exceptions;
using NOTEKEEPER.Api.Repositories;

namespace NOTEKEEPER.Api.Commands;

public class CreateUserCommand : IRequest<int>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PasswordHash { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _unitOfWork.Users.FindAsync(u => u.Email == request.Email);
        if (existingUser.Any()) 
        { 
            throw new EmailAlreadyInUseException($"Email '{request.Email}' is already in use."); 
        }

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash),
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return user.Id;
    }
}

