using MediatR;
using Microsoft.EntityFrameworkCore;
using testAPIDatabase.Context;
using testAPIDatabase.Entities;

namespace testAPIDatabase.Handlers.Commands;

public sealed record AddGenreCommand : IRequest<Genre>
{
    public required string Name;
}

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}

public class AddGenreCommandHandler : IRequestHandler<AddGenreCommand, Genre>
{
    private readonly AppDbContext _dbContext;

    public AddGenreCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Genre> Handle(AddGenreCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request?.Name))
        {
            throw new Exception("Genre name cannot be empty.");
        }

        var existingGenre = await _dbContext.Genres.FirstOrDefaultAsync(g => g.Name == request.Name);

        if (existingGenre is not null)
        {
            throw new ConflictException("Genre already exists.");
        }

        var genreEntity = new Genre { Name = request.Name };

        _dbContext.Genres.Add(genreEntity);
        await _dbContext.SaveChangesAsync();

        return genreEntity;
    }
}
