using Microsoft.AspNetCore.Mvc;
using MediatR;
using NOTEKEEPER.Api.Commands;
using NOTEKEEPER.Api.Queries;
using NOTEKEEPER.Api.Repositories;
using NOTEKEEPER.Api.Services;
using NOTEKEEPER.Api.Exceptions;

namespace NOTEKEEPER.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly TokenService _tokenService;

    public UsersController(IMediator mediator, IUnitOfWork unitOfWork, TokenService tokenService)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        //var userId = await _mediator.Send(command);
        //return Ok(userId);
        try
        {
            var userId = await _mediator.Send(command);
            return Ok(userId);
        }
        catch (EmailAlreadyInUseException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery { Id = id });
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        return Ok(users);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _unitOfWork.Users.Remove(user);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}

