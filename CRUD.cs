using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                var existingMovie = context.Movies.FirstOrDefault(a => a.Title == movieTitle);

                // adding a new movie
                if (existingMovie == null)
                {
                    string? movieDescription = Helpers.GetUserInput("Enter movie description:");
                    int movieRuntime = int.Parse(Helpers.GetUserInput("Enter movie runtime in minutes:"));
                    string? movieRating = Helpers.GetUserInput("Enter movie MPA rating:");
                    DateOnly movieReleaseDate = DateOnly.Parse(Helpers.GetUserInput("Enter movie release date (YYYY/MM/DD):"));

                    context.Movies.Add(new Movie { Title = movieTitle, Description = movieDescription, Runtime = movieRuntime, Rating = movieRating, ReleaseDate = movieReleaseDate });
                    context.SaveChanges();

                    Helpers.SetConsoleColor("green");
                    Console.WriteLine("Movie added successfully.");
                    Helpers.ResetConsoleColor();

                    Helpers.DisplayMovieList();
                }
                // updating an existing movie
                else
                {
                    Console.WriteLine("Entering edit mode.");
                    string? editedTitle = Helpers.GetUserInput("If you wish to update the movie's title, enter it here, otherwise press enter.");
                    string? editedDescription = Helpers.GetUserInput("Enter new movie description or enter to keep as is:");
                    int editedRuntime = int.Parse(Helpers.GetUserInput("Enter new movie runtime in minutes or enter to keep as is:"));
                    string? editedRating = Helpers.GetUserInput("Enter new movie MPA rating or enter to keep as is:");
                    DateOnly editedReleaseDate = DateOnly.Parse(Helpers.GetUserInput("Enter new movie release date (YYYY/MM/DD) or enter to keep as is:"));

                    if (!string.IsNullOrEmpty(editedTitle)) {
                        existingMovie.Title = editedTitle;
                    }
                    if (!string.IsNullOrEmpty(editedDescription))
                    {
                        existingMovie.Description = editedDescription;
                    }
                    if (editedRuntime != null || editedRuntime != 0)
                    {
                        existingMovie.Runtime = editedRuntime;
                    }
                    if (!string.IsNullOrEmpty(editedRating))
                    {
                        existingMovie.Rating = editedRating;
                    }
                    if (editedReleaseDate != null)
                    {
                        existingMovie.ReleaseDate = editedReleaseDate;
                    }

                    Helpers.SetConsoleColor("green");
                    Console.WriteLine("Movie updated successfully.");
                    Helpers.ResetConsoleColor();
                    context.SaveChanges();
                }
            }
        }
        

        // READ MOVIE
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
                    Console.WriteLine("\n..............................");
                    // Print title
                    Console.WriteLine($"🎥 {movie.Title}");
                    Console.WriteLine("---");

                    // Print description
                    if (!string.IsNullOrEmpty(movie.Description))
                        Console.WriteLine($"📄 Description: {movie.Description}");

                    // Print rating
                    Console.WriteLine($"👁️ Rated: {movie.Rating}");

                    // Print runtime
                    Console.WriteLine($"🕑 Runtime: {movie.Runtime} minutes");

                    // Print release date
                    Console.WriteLine($"📅 Release Date: {movie.ReleaseDate}");

                    // Print genre(s)
                    if (movie.Genres.Count() > 0)
                    {
                        Console.Write("🍿 Genre(s): ");
                        for (int i = 0; i < movie.Genres.Count(); i++)
                        {
                            if (i == movie.Genres.Count() - 1)
                            {
                                Console.Write(movie.Genres[i].Name);
                            }
                            else
                            {
                                Console.Write(movie.Genres[i].Name + ", ");
                            }
                        }
                    }

                    // Print cast
                    if (movie.Actors.Count() > 0)
                    {
                        Console.Write("\n👥 Top Cast: ");
                        for (int i = 0; i < movie.Actors.Count(); i++)
                        {
                            if (i == movie.Actors.Count() - 1)
                            {
                                Console.Write(movie.Actors[i].Name);
                            }
                            else
                            {
                                Console.Write(movie.Actors[i].Name + ", ");
                            }
                        }
                    }
                    
                    Console.WriteLine("\n..............................\n");
                }
                else
                {
                    Helpers.SetConsoleColor("red");
                    Console.WriteLine("Movie not found.");
                    Helpers.ResetConsoleColor();
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

                        Helpers.SetConsoleColor("green");
                        Console.WriteLine("Movie deleted successfully.");
                        Helpers.ResetConsoleColor();
                    }
                    else
                    {
                        Helpers.SetConsoleColor("red");
                        Console.WriteLine("❌ Movie not found.");
                        Helpers.ResetConsoleColor();
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

            Helpers.DisplayMovieList();
        }
    }
}