using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace movies_backend.Features.Movies;

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoviesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<MovieDto>>> GetMovies()
        => await _mediator.Send(new GetMoviesQuery());

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto?>> GetMovie(int id)
        => await _mediator.Send(new GetMovieByIdQuery(id));

    [HttpPost]
    public async Task<ActionResult<MovieDto>> CreateMovie([FromBody] CreateMovieCommand command)
    {
        var movie = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetMovie), new { id = movie?.Id }, movie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, [FromBody] EditMovieCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }
}

