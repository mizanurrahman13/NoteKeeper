using MediatR;
using NOTEKEEPER.Api.Entities;
using NOTEKEEPER.Api.Repositories;

namespace NOTEKEEPER.Api.Commands;

public class CreateNoteCommand : IRequest<int>
{
    public string Text { get; set; }
    public string Type { get; set; }
    public DateTime? Reminder { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsComplete { get; set; }
    public string Url { get; set; }
    public int UserId { get; set; }
}

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateNoteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Note
        {
            Text = request.Text,
            Type = request.Type,
            Reminder = request.Reminder,
            DueDate = request.DueDate,
            IsComplete = request.IsComplete,
            Url = request.Url,
            UserId = request.UserId
        };

        await _unitOfWork.Notes.AddAsync(note);
        await _unitOfWork.SaveChangesAsync();

        return note.Id;
    }
}

