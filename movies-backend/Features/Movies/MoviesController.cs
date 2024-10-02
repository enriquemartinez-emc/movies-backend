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

    /// <summary>
    /// Get the list of all the Movies
    /// </summary>
    /// <returns>List of all movies</returns>
    /// <remarks>
    /// </remarks>
    [HttpGet]
    public async Task<ActionResult<List<MovieDto>>> GetMovies()
        => await _mediator.Send(new GetMoviesQuery());

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto?>> GetMovie(int id)
        => await _mediator.Send(new GetMovieByIdQuery(id));

    /// <summary>
    /// Creates a Movie.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST api/movies
    ///     {        
    ///       {
    ///         "title": "Batman",
    ///         "director": "Christopher Nolan",
    ///         "releaseDate": "2024-10-02T05:23:13.642Z",
    ///         "categoryId": 1
    ///       }
    ///     }
    /// </remarks>
    /// <returns>A newly created movie</returns>
    /// <response code="201">Returns the newly created movie</response>
    /// <response code="400">If the movie is null</response>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
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

