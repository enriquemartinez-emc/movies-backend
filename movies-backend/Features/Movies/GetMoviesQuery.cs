using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using movies_backend.Infrastructure;

namespace movies_backend.Features.Movies;

public class GetMoviesQuery : IRequest<List<MovieDto>> { }

public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, List<MovieDto>>
{
    private readonly MoviesContext _context;
    private readonly IMapper _mapper;

    public GetMoviesQueryHandler(MoviesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MovieDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _context.Movies
            .Include(m => m.Category)
            .ToListAsync();

        return _mapper.Map<List<MovieDto>>(movies);
    }
}
