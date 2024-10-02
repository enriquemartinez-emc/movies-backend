namespace movies_backend.Features.Movies;

public class MovieDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
    public required string CategoryName { get; set; }
}

