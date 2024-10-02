using MediatR;
using movies_backend.Infrastructure;

namespace movies_backend.Features.Movies;

public class EditMovieCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
}

public class EditMovieCommandHandler : IRequestHandler<EditMovieCommand, Unit>
{
    private readonly MoviesContext _context;

    public EditMovieCommandHandler(MoviesContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(EditMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FindAsync(request.Id);

        // TODO: Create a well defined exception when item is not found
        // so null is not returned
        // if (movie == null) throw new NotFoundException(nameof(Movie), request.Id);
        if (movie == null) return Unit.Value;


        movie.UpdateDetails(request.Title, request.Director, request.ReleaseDate);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

