using Humanizer;
using Microsoft.EntityFrameworkCore;

public class MoviesDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Genre> Genres { get; set; }
    //public DbSet<Director> Directors { get; set; }
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString:
           "Server=localhost;Port=5432;User Id=postgres;Password=password;Database=movies;Include Error Detail=true;"); 
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // tell EF core to use the characters table to
        // determine the relationship between actors and movies
        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Actors)
            .WithMany(a => a.Movies)
            .UsingEntity<Character>();

        // Configure many-to-many relationship
        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Movies)
            .UsingEntity<Dictionary<string, object>>(
                "MovieGenre",
                j => j.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                j => j.HasOne<Movie>().WithMany().HasForeignKey("MovieId"));

        base.OnModelCreating(modelBuilder);

        // Seeding tables with data    
        //modelBuilder.Entity<Director>().HasData(
        //    new Director { DirectorId = 1, Name = "Gil Junger" },
        //    new Director { DirectorId = 2, Name = "Martin Scorsese" },
        //    new Director { DirectorId = 3, Name = "Mary Harron" },
        //    new Director { DirectorId = 4, Name = "Shawn Levy" },
        //    new Director { DirectorId = 5, Name = "Russo Brothers" }
        //);

        modelBuilder.Entity<Movie>().HasData(
            new Movie { MovieId = 1, Title = "10 Things I Hate About You", Runtime = 97, Rating = "PG-13", ReleaseDate = new DateOnly(1999, 3, 31) },
            new Movie { MovieId = 2, Title = "The Wolf of Wall Street", Runtime = 180, Rating = "R", ReleaseDate = new DateOnly(2013, 12, 25) },
            new Movie { MovieId = 3, Title = "American Psycho", Runtime = 104, Rating = "R", ReleaseDate = new DateOnly(2000, 4, 14) },
            new Movie { MovieId = 4, Title = "Night at the Museum", Runtime = 108, Rating = "PG", ReleaseDate = new DateOnly(2006, 12, 17) },
            new Movie { MovieId = 5, Title = "Avengers: Endgame", Runtime = 181, Rating = "PG-13", ReleaseDate = new DateOnly(2019, 4, 26) }
        );
        
        modelBuilder.Entity<Genre>().HasData(
            new Genre { GenreId = 1, Name = "Action" },
            new Genre { GenreId = 2, Name = "Adventure" },
            new Genre { GenreId = 3, Name = "Comedy" },
            new Genre { GenreId = 4, Name = "Drama" },
            new Genre { GenreId = 5, Name = "Horror" },
            new Genre { GenreId = 6, Name = "Science Fiction" },
            new Genre { GenreId = 7, Name = "Fantasy" },
            new Genre { GenreId = 8, Name = "Thriller" },
            new Genre { GenreId = 9, Name = "Romance" },
            new Genre { GenreId = 10, Name = "Mystery" },
            new Genre { GenreId = 11, Name = "Documentary" },
            new Genre { GenreId = 12, Name = "Animation" },
            new Genre { GenreId = 13, Name = "Musical" },
            new Genre { GenreId = 14, Name = "Crime" },
            new Genre { GenreId = 15, Name = "War" },
            new Genre { GenreId = 16, Name = "Western" },
            new Genre { GenreId = 17, Name = "Historical" },
            new Genre { GenreId = 18, Name = "Biographical" },
            new Genre { GenreId = 19, Name = "Family" },
            new Genre { GenreId = 20, Name = "Superhero" }
        );

        modelBuilder.Entity<Actor>().HasData(
            // Movie 1
            new Actor { ActorId = 1, Name = "Heath Ledger" },
            new Actor { ActorId = 2, Name = "Julia Stiles" },
            new Actor { ActorId = 3, Name = "Joseph Gordon-Levitt" },
            // Movie 2
            new Actor { ActorId = 4, Name = "Leonardo DiCaprio" },
            new Actor { ActorId = 5, Name = "Jonah Hill" },
            new Actor { ActorId = 6, Name = "Margot Robbie" },
            // Movie 3
            new Actor { ActorId = 7, Name = "Christian Bale" },
            new Actor { ActorId = 8, Name = "Justin Theroux" },
            new Actor { ActorId = 9, Name = "Josh Lucas" },
            // Movie 4
            new Actor { ActorId = 10, Name = "Ben Stiller" },
            new Actor { ActorId = 11, Name = "Robin Williamss" },
            new Actor { ActorId = 12, Name = "Owen Wilson" },
            // Movie 5
            new Actor { ActorId = 13, Name = "Robert Downey Jr." },
            new Actor { ActorId = 14, Name = "Scarlett Johansson" },
            new Actor { ActorId = 15, Name = "Chris Evans" }
            
        );

        modelBuilder.Entity<Character>().HasData(
            // Movie 1
            new Character { CharacterId = 1, CharacterName = "Patrick Verona", ActorId = 1, MovieId = 1 },
            new Character { CharacterId = 2, CharacterName = "Kat Stratford", ActorId = 2, MovieId = 1 },
            new Character { CharacterId = 3, CharacterName = "Cameron James", ActorId = 3, MovieId = 1 },
            // Movie 2
            new Character { CharacterId = 4, CharacterName = "Jordan Belfort", ActorId = 4, MovieId = 2 },
            new Character { CharacterId = 5, CharacterName = "Donnie Azoff", ActorId = 5, MovieId = 2 },
            new Character { CharacterId = 6, CharacterName = "Naomi Lapaglia", ActorId = 6, MovieId = 2 },
            // Movie 3
            new Character { CharacterId = 7, CharacterName = "Patrick Bateman", ActorId = 7, MovieId = 3 },
            new Character { CharacterId = 8, CharacterName = "Timothy Bryce", ActorId = 8, MovieId = 3 },
            new Character { CharacterId = 9, CharacterName = "Craig McDermott", ActorId = 9, MovieId = 3 },
            // Movie 4
            new Character { CharacterId = 10, CharacterName = "Larry Daley", ActorId = 10, MovieId = 4 },
            new Character { CharacterId = 11, CharacterName = "Theodore Roosevelt",ActorId = 11, MovieId = 4 },
            new Character { CharacterId = 12, CharacterName = "Jebediah Smith", ActorId = 12, MovieId = 4 },
            // Movie 5
            new Character { CharacterId = 13, CharacterName = "Tony Stark / Iron Man", ActorId = 13, MovieId = 5 },
            new Character { CharacterId = 14, CharacterName = "Natasha Romanov / Black Widow", ActorId = 14, MovieId = 5 },
            new Character { CharacterId = 15, CharacterName = "Steve Rodgers / Captain America", ActorId = 15, MovieId = 5 }
        );

        // add genres to movies
        modelBuilder.Entity("MovieGenre").HasData(
            new { MovieId = 1, GenreId = 3 },
            new { MovieId = 1, GenreId = 9 },
            new { MovieId = 2, GenreId = 18 },
            new { MovieId = 2, GenreId = 3 },
            new { MovieId = 2, GenreId = 14 },
            new { MovieId = 3, GenreId = 5 },
            new { MovieId = 3, GenreId = 14 },
            new { MovieId = 4, GenreId = 3 },
            new { MovieId = 4, GenreId = 7 },
            new { MovieId = 5, GenreId = 1 },
            new { MovieId = 5, GenreId = 2 },
            new { MovieId = 5, GenreId = 4 },
            new { MovieId = 5, GenreId = 20 }
        );
    }
}