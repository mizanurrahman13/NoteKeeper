using MediatR;
using NOTEKEEPER.Api.Entities;
using NOTEKEEPER.Api.Exceptions;
using NOTEKEEPER.Api.Repositories;

namespace NOTEKEEPER.Api.Commands;

public class UpdateUserCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PasswordHash { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id);
        if (user == null)
        {
            throw new NotFoundException($"User with ID {request.Id} not found.");
        }

        user.Username = request.Username;
        user.Email = request.Email;
        user.DateOfBirth = request.DateOfBirth;
        //user.PasswordHash = request.PasswordHash;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return user.Id;
    }
}

