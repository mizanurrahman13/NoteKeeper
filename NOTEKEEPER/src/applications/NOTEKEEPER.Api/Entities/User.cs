namespace NOTEKEEPER.Api.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PasswordHash { get; set; }
    public ICollection<Note> Notes { get; set; }
}
