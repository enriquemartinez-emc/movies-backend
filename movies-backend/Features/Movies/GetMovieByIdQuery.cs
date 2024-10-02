using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using movies_backend.Infrastructure;

namespace movies_backend.Features.Movies;

public class GetMovieByIdQuery : IRequest<MovieDto?>
{
    public GetMovieByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}

public class Handler : IRequestHandler<GetMovieByIdQuery, MovieDto?>
{
    private readonly MoviesContext _context;
    private readonly IMapper _mapper;

    public Handler(MoviesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MovieDto?> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies
            .Include(m => m.Category)
            .FirstOrDefaultAsync(m => m.Id == request.Id);

        // TODO: Create a well defined exception when item is not found
        // so null is not returned
        // if (movie == null) throw new NotFoundException(nameof(Movie), request.Id);
        if (movie == null) return null;

        return _mapper.Map<MovieDto>(movie);
    }
}
