using MediatR;
using Microsoft.EntityFrameworkCore;
using testAPIDatabase.Context;
using testAPIDatabase.Entities;

namespace testAPIDatabase.Handlers.Commands;

public sealed record UpdateGenreCommand : IRequest<Genre>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}

public class UpdateGenreHandler : IRequestHandler<UpdateGenreCommand, Genre>
{
    private readonly AppDbContext _dbContext;

    public UpdateGenreHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Genre> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Genre name cannot be empty.");
        }

        var genre = await _dbContext.Genres.FindAsync(request.Id);
        if (genre is null)
        {
            throw new KeyNotFoundException("Genre not found (404).");
        }

        var existingGenre = await _dbContext.Genres
            .FirstOrDefaultAsync(g => g.Name == request.Name && g.Id != request.Id, cancellationToken);

        if (existingGenre is not null)
        {
            throw new ConflictException("Genre already exists.");
        }

        genre.Name = request.Name;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return genre;
    }
}
