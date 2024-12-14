using MediatR;
using NOTEKEEPER.Api.Entities;
using NOTEKEEPER.Api.Repositories;

namespace NOTEKEEPER.Api.Queries;

public class GetNoteByIdQuery : IRequest<Note>
{
    public int Id { get; set; }
}

public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, Note>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetNoteByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Note> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Notes.GetByIdAsync(request.Id);
    }
}

