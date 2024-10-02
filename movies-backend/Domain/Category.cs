namespace movies_backend.Domain;

public class Category
{
    public Category()
    {
    }
    public int Id { get; private set; }
    public string Name { get; private set; }
    public List<Movie> Movies { get; private set; } = new List<Movie>();

    public Category(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty.");

        Name = name;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty.");

        Name = name;
    }

    public void AddMovie(Movie movie)
    {
        if (movie == null)
            throw new ArgumentNullException(nameof(movie));

        Movies.Add(movie);
    }
}
