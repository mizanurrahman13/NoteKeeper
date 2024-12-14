using MediatR;
using NOTEKEEPER.Api.Entities;
using NOTEKEEPER.Api.Exceptions;
using NOTEKEEPER.Api.Repositories;

namespace NOTEKEEPER.Api.Commands;

public class UpdateNoteCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }
    public DateTime? Reminder { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsComplete { get; set; }
    public string Url { get; set; }
    public int UserId { get; set; }
}

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateNoteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _unitOfWork.Notes.GetByIdAsync(request.Id);
        if (note == null)
        {
            throw new NotFoundException($"Note with ID {request.Id} not found.");
        }

        note.Text = request.Text;
        note.Type = request.Type;
        note.Reminder = request.Reminder;
        note.DueDate = request.DueDate;
        note.IsComplete = request.IsComplete;
        note.Url = request.Url;
        note.UserId = request.UserId;

        _unitOfWork.Notes.Update(note);
        await _unitOfWork.SaveChangesAsync();

        return note.Id;
    }
}

