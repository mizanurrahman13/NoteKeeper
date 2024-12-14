using MediatR;
using NOTEKEEPER.Api.Entities;
using NOTEKEEPER.Api.Repositories;

namespace NOTEKEEPER.Api.Queries;

public class GetUserByIdQuery : IRequest<User>
{
    public int Id { get; set; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Users.GetByIdAsync(request.Id);
    }
}

