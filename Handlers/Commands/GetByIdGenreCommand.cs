using MediatR;
using testAPIDatabase.Context;
using testAPIDatabase.Entities;

namespace testAPIDatabase.Handlers.Commands;

public class GetByIdGenreCommand : IRequest<Genre>
{
    public required Guid Id { get; init; }
}

public class GetByIdGenreHandler : IRequestHandler<GetByIdGenreCommand, Genre>
{
    private readonly AppDbContext _dbContext;

    public GetByIdGenreHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Genre> Handle(GetByIdGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _dbContext.Genres.FindAsync(request.Id);
        if (genre is null)
        {
            throw new KeyNotFoundException("Genre not found (404).");
        }
        return genre;
    }
}

