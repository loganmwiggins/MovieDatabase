using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jm_sql
{
    internal class CRUD
    {
        public static void AddMovie()
        {
            using (var context = new MoviesDbContext())
            {
                string? movieTitle = Helpers.GetUserInput("Enter movie title you want to add or update:");

                // if the movie title already exists in the database, it will be updated
                string? movieDescription = Helpers.GetUserInput("Enter movie description:");
                int movieRuntime = int.Parse(Helpers.GetUserInput("Enter movie runtime in minutes:"));
                string? movieRating = Helpers.GetUserInput("Enter movie MPA rating:");
                DateOnly movieReleaseDate = DateOnly.Parse(Helpers.GetUserInput("Enter movie release date (YYYY/MM/DD):"));

                context.Movies.Add(new Movie { Title = movieTitle, Description = movieDescription, Runtime = movieRuntime, Rating = movieRating, ReleaseDate = movieReleaseDate});
                context.SaveChanges();
                Console.WriteLine("Movie added successfully.");

                Helpers.DisplayMovieList();
            }
        }


        public static void ViewMovieDetail(int movieId)
        {
            using (var context = new MoviesDbContext())
            {
                Movie? movie = context.Movies
                    .Include(m => m.Genres)
                    .Include(m => m.Actors)
                    .FirstOrDefault(o => o.MovieId == movieId);

                if (movie != null)
                {
                    Console.WriteLine("\n---");
                    // Print title
                    Console.WriteLine(movie.Title);

                    // Print description
                    if (!string.IsNullOrEmpty(movie.Description))
                        Console.WriteLine(movie.Description);

                    // Print rating
                    Console.WriteLine($"Rated: {movie.Rating}");

                    // Print runtime
                    Console.WriteLine($"Runtime: {movie.Runtime} minutes");

                    // Print release date
                    Console.WriteLine($"Release Date: {movie.ReleaseDate}");

                    // Print genre(s)
                    Console.Write("Genre(s):");
                    foreach (var genre in movie.Genres)
                    {
                        Console.WriteLine(genre.Name + " / ");
                    }

                    // Print cast
                    Console.WriteLine("Cast:");
                    foreach (var actor in movie.Actors)
                    {
                        Console.WriteLine(actor.Name);
                    }
                    Console.WriteLine("---\n");
                }
                else
                {
                    Console.WriteLine("Movie not found.");
                }
            }
        }


        public static void DeleteMovie()
        {
            using (var context = new MoviesDbContext())
            {
                Console.WriteLine("Enter the Movie ID of the movie you want to delete:");

                try
                {
                    int movieId = int.Parse(Console.ReadLine() ?? "");

                    var existingMovie = context.Movies.FirstOrDefault(a => a.MovieId == movieId);

                    if (existingMovie != null)
                    {
                        context.Movies.Remove(existingMovie);
                        context.SaveChanges();
                        Console.WriteLine("Movie deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Movie not found.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the Movie ID.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }
    }
}