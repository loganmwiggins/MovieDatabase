using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

public class Movie
{
    public int MovieId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required int Runtime {  get; set; }
    public required string Rating { get; set; }
    public required DateOnly ReleaseDate { get; set; }


    [ForeignKey("Director")]
    public int DirectorId { get; set; }
    public Director Director { get; set; } = null!;


    // Used to make middle tables automatically
    public List<Actor> Actors { get; } = [];
    public List<Genre> Genres { get; } = [];

}


public class Genre
{
    public int GenreId { get; set; }
    public required string Name { get; set; }

    public List<Movie> Movies { get; } = [];

}


// this is a table to link one actor to their character name in one movie
public class Character
{
    [ForeignKey("Movie")]
    public int MovieId { get; set; }
    public Movie Movie { get; set; }


    [ForeignKey("Actor")]
    public int ActorId { get; set; }
    public Actor Actor { get; set; }


    public required int CharacterId { get; set; }
    public string? CharacterName { get; set; }
}


public class Actor
{
    public int ActorId { get; set; }
    public required string Name { get; set; }

    public List<Movie> Movies { get; } = [];
}


public class Director
{
    public int DirectorId { get; set; }
    public required string Name { get; set; }

    public List<Movie> Movies { get; } = [];
}