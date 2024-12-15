using Microsoft.AspNetCore.Mvc;
using MediatR;
using NOTEKEEPER.Api.Commands;
using NOTEKEEPER.Api.Queries;
using NOTEKEEPER.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace NOTEKEEPER.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public NotesController(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNoteCommand command)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        command.UserId = userId;
        var noteId = await _mediator.Send(command);
        return Ok(noteId);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var note = await _mediator.Send(new GetNoteByIdQuery { Id = id });
        if (note == null || note.UserId != userId)
        {
            return Unauthorized();
        }
        return Ok(note);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var notes = await _unitOfWork.Notes.FindAsync(n => n.UserId == userId);
        return Ok(notes);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateNoteCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var note = await _unitOfWork.Notes.GetByIdAsync(id);
        if (note == null || note.UserId != userId)
        {
            return Unauthorized();
        }

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var note = await _unitOfWork.Notes.GetByIdAsync(id);
        if (note == null || note.UserId != userId)
        {
            return Unauthorized();
        }

        _unitOfWork.Notes.Remove(note);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}


