using AutoMapper;
using MediatR;
using movies_backend.Domain;
using movies_backend.Infrastructure;

namespace movies_backend.Features.Movies;

public class CreateMovieCommand : IRequest<MovieDto?>
{
    public required string Title { get; set; }
    public required string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int CategoryId { get; set; }
}

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, MovieDto?>
{
    private readonly MoviesContext _context;
    private readonly IMapper _mapper;

    public CreateMovieCommandHandler(MoviesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MovieDto?> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(request.CategoryId);

        // TODO: Create a well defined exception when item is not found
        // so null is not returned
        // if (category == null) throw new NotFoundException(nameof(Category), request.CategoryId);
        if (category == null) return null;

        var movie = new Movie(request.Title, request.Director, request.ReleaseDate, category);
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<MovieDto>(movie);
    }
}
