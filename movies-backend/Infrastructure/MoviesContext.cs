using Microsoft.EntityFrameworkCore;
using movies_backend.Domain;

namespace movies_backend.Infrastructure;

public class MoviesContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Movie> Movies { get; set; }

    public MoviesContext(DbContextOptions<MoviesContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Movies)
            .WithOne(m => m.Category)
            .HasForeignKey(m => m.CategoryId);

        modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Movie>().Property(m => m.Title).IsRequired().HasMaxLength(200);
        modelBuilder.Entity<Movie>().Property(m => m.Director).IsRequired().HasMaxLength(100);
    }
}

