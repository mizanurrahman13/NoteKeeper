using NOTEKEEPER.Api.Contexts;
using NOTEKEEPER.Api.Entities;

namespace NOTEKEEPER.Api.Repositories;

public class NoteRepository : Repository<Note>, INoteRepository
{
    public NoteRepository(NoteKeeperContext context) : base(context)
    {
    }

    // Add any note-specific methods here if needed
}
