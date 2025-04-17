using MediatR;
using testAPIDatabase.Context;
using testAPIDatabase.Entities;

namespace testAPIDatabase.Handlers.Commands;

public sealed record DeleteGenreCommand : IRequest<Genre>
{
    public required Guid Id { get; init; }
}

public class DeleteGenreHandler : IRequestHandler<DeleteGenreCommand, Genre>
{
    private readonly AppDbContext _dbContext;

    public DeleteGenreHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Genre> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _dbContext.Genres.FindAsync(request.Id);
        if (genre is null)
        {
            throw new KeyNotFoundException("Genre not found (404).");
        }
        _dbContext.Genres.Remove(genre);
        await _dbContext.SaveChangesAsync();

        return genre;
    }
}

