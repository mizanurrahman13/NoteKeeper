using Moq;
using FluentAssertions;
using NOTEKEEPER.Api.Repositories;
using NOTEKEEPER.Api.Services;
using NOTEKEEPER.Api.Commands;
using NOTEKEEPER.Api.Entities;

namespace NOTEKEEPER.Api.Tests.Commands;
[TestFixture]
public class LoginUserCommandHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<ITokenService> _tokenServiceMock;
    private LoginUserCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userRepositoryMock = new Mock<IUserRepository>();

        // Mock the TokenService
        _tokenServiceMock = new Mock<ITokenService>();

        _unitOfWorkMock.SetupGet(u => u.Users).Returns(_userRepositoryMock.Object);
        _handler = new LoginUserCommandHandler(_unitOfWorkMock.Object, _tokenServiceMock.Object);
    }

    [Test]
    public async Task Handle_ValidCredentials_ShouldReturnLoginResponse()
    {
        var command = new LoginUserCommand
        {
            Email = "mizan@gmail.com",
            Password = "password"
        };

        var user = new User
        {
            Id = 1,
            Username = "mizan",
            Email = "mizan@gmail.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("password")
        };

        _userRepositoryMock
            .Setup(repo => repo.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(user);

        // Mock the GenerateToken method
        _tokenServiceMock
            .Setup(service => service.GenerateToken(It.IsAny<User>()))
            .Returns("mocked_token");

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Email.Should().Be("mizan@gmail.com");
        result.UserId.Should().Be(1);
        result.Token.Should().Be("mocked_token");
    }

    [Test]
    public void Handle_InvalidCredentials_ShouldThrowUnauthorizedAccessException()
    {
        var command = new LoginUserCommand
        {
            Email = "mizan@gmail.com",
            Password = "wrongpassword"
        };

        _userRepositoryMock
            .Setup(repo => repo.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((User)null);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("Invalid email or password.");
    }
}