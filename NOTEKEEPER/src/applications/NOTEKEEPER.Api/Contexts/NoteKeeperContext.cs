using Microsoft.EntityFrameworkCore;
using NOTEKEEPER.Api.Entities;

namespace NOTEKEEPER.Api.Contexts;
public class NoteKeeperContext : DbContext
{
    public NoteKeeperContext(DbContextOptions<NoteKeeperContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }
}

