namespace movies_backend.Domain;

public class Movie
{
    public Movie()
    {
    }
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Director { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }

    public Movie(string title, string director, DateTime releaseDate, Category category)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Movie title cannot be empty.");
        if (string.IsNullOrWhiteSpace(director))
            throw new ArgumentException("Director name cannot be empty.");
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        Title = title;
        Director = director;
        ReleaseDate = releaseDate;
        Category = category;
        CategoryId = category.Id;
    }

    public void UpdateDetails(string title, string director, DateTime releaseDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Movie title cannot be empty.");
        if (string.IsNullOrWhiteSpace(director))
            throw new ArgumentException("Director name cannot be empty.");

        Title = title;
        Director = director;
        ReleaseDate = releaseDate;
    }
}

