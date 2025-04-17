using MediatR;
using Microsoft.EntityFrameworkCore;
using testAPIDatabase.Context;
using testAPIDatabase.Entities;

namespace testAPIDatabase.Handlers.Commands;

public class GetAllGenresCommand : IRequest<List<Genre>>
{
}
public class GetAllGenresHandler : IRequestHandler<GetAllGenresCommand, List<Genre>>
{
    private readonly AppDbContext _dbContext;

    public GetAllGenresHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Genre>> Handle(GetAllGenresCommand request, CancellationToken cancellationToken)
    {
        var allGenres = await _dbContext.Genres.ToListAsync(cancellationToken);
        return allGenres;
    }
}


