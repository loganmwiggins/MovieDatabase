using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace jm_sql
{
    internal class CRUD
    {
        public static void AddMovie()
        {
            using (var context = new MoviesDbContext())
            {
                string? movieTitle = Helpers.GetUserInput("🎥 Enter movie title to add or update:");

                while (true)    // Input validation - must enter a title (can't be null)
                {
                    if (string.IsNullOrEmpty(movieTitle))
                    {
                        Helpers.SetConsoleColor("yellow");
                        Console.WriteLine("⚠️ Invalid selection. Try again...\n");
                        Helpers.ResetConsoleColor();
                        movieTitle = Helpers.GetUserInput($"🎥 Enter movie title to add or update:");
                    }
                    else break;
                }

                // If the movie title already exists in the database, it will be updated
                var existingMovie = context.Movies.FirstOrDefault(a => a.Title == movieTitle);

                // Adding a new movie
                if (existingMovie == null)
                {
                    string? movieDescription = Helpers.GetUserInput("📄 Enter movie description:");
                    string? rtInput = Helpers.GetUserInput("🕑 Enter movie runtime (in minutes):");

                    // Input validation for runtime
                    while (true)
                    {
                        if (string.IsNullOrEmpty(rtInput))
                        {
                            Helpers.SetConsoleColor("yellow");
                            Console.WriteLine("\n⚠️ Runtime cannot be null...");
                            Helpers.ResetConsoleColor();
                            rtInput = Helpers.GetUserInput("🕑 Enter movie runtime (in minutes):");
                        }
                        else break;
                    }

                    int movieRuntime = Regex.IsMatch(rtInput, @"^\d+$") ? int.Parse(rtInput) : -1;

                    while (movieRuntime < 0)
                    {
                        if (string.IsNullOrEmpty(rtInput))
                        {
                            Helpers.SetConsoleColor("yellow");
                            Console.WriteLine("\n⚠️ Runtime cannot be null...");
                            Helpers.ResetConsoleColor();
                            rtInput = Helpers.GetUserInput("🕑 Enter movie runtime (in minutes):");
                        }
                        else
                        {
                            movieRuntime = Regex.IsMatch(rtInput, @"^\d+$") ? int.Parse(rtInput) : -1;

                            if (movieRuntime < 0)
                            {
                                Helpers.SetConsoleColor("red");
                                Console.WriteLine("\n❌ Invalid movie runtime...");
                                Helpers.ResetConsoleColor();
                                rtInput = Helpers.GetUserInput("🕑 Enter movie runtime (in minutes):");
                            }
                            else break;
                        }
                    }

                    string? movieRating = Helpers.GetUserInput("👁️ Enter movie MPA rating:");
                    DateOnly movieReleaseDate = DateOnly.Parse(Helpers.GetUserInput("📅 Enter movie release date (YYYY/MM/DD):"));

                    // Display genre list
                    Helpers.DisplayGenreList();

                    // Prompt user for genres
                    string? genres = Helpers.GetUserInput(("🍿 Enter movie genre(s) separated by commas:"));
                    string[] genreList = genres.Split(',');

                    // Parse to ints
                    int[] genreIds = new int[genreList.Length];
                    for (int i = 0; i < genreList.Length; i++)
                    {
                        genreIds[i] = int.Parse(genreList[i]);
                    }

                    // Create a new movie object
                    Movie newMovie = new Movie { Title = movieTitle, Description = movieDescription, Runtime = movieRuntime, Rating = movieRating, ReleaseDate = movieReleaseDate };

                    // Add each genre to the movie
                    foreach (int genre in genreIds)
                    {
                        Genre genre1 = context.Genres.FirstOrDefault(a => a.GenreId == genre);
                        newMovie.Genres.Add(genre1);
                    }

                    // Add the new movie to the database
                    context.Movies.Add(newMovie);
                    context.SaveChanges();

                    Helpers.SetConsoleColor("green");
                    Console.WriteLine("\n✅ Movie added successfully.");
                    Helpers.ResetConsoleColor();

                    ViewMovieDetail(context.Movies.FirstOrDefault(a => a.Title == movieTitle).MovieId);
                }
                // Updating an existing movie
                else
                {
                    Helpers.SetConsoleColor("yellow");
                    Console.WriteLine("\n✏️ Entering edit mode...\n");
                    Helpers.ResetConsoleColor(); 

                    string? editedTitle = Helpers.GetUserInput("🎥 Updated movie title [press enter to skip]:");
                    string? editedDescription = Helpers.GetUserInput("📄 Updated movie description [press enter to skip]:");
                    int editedRuntime = int.Parse(Helpers.GetUserInput("🕑 Updated movie runtime [required]:"));
                    string? editedRating = Helpers.GetUserInput("👁️ Updated movie MPA rating [press enter to skip]:");
                    DateOnly editedReleaseDate = DateOnly.Parse(Helpers.GetUserInput("📅 Updated movie release date (YYYY/MM/DD) [required]:"));

                    // display genre list
                    Helpers.DisplayGenreList();

                    // ask the user for the genres they want the movie to have
                    string? editedGenres = Helpers.GetUserInput(("🍿 Updated ID(s) of movie genre(s) separated by commas [press enter to skip]:"));
                    // get the genres
                    string[] editedGenreList = editedGenres.Split(',');
                    // parse to ints
                    int[] genreIds = new int[editedGenreList.Length];
                    for (int i = 0; i < editedGenreList.Length; i++)
                    {
                        genreIds[i] = int.Parse(editedGenreList[i]);
                    }

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

                    // add the new genres if the genrelist isnt null
                    if (editedGenreList != null)
                    {
                        // clear the existing genres
                        existingMovie.Genres.Clear();
                        // add the new genres
                        foreach (int genre in genreIds)
                        {
                            Genre genre1 = context.Genres.FirstOrDefault(a => a.GenreId == genre);
                            existingMovie.Genres.Add(genre1);
                        }
                    }

                    context.SaveChanges();

                    Helpers.SetConsoleColor("green");
                    Console.WriteLine("\n✅ Movie updated successfully.");
                    Helpers.ResetConsoleColor();

                    ViewMovieDetail(context.Movies.FirstOrDefault(a => a.Title == existingMovie.Title).MovieId);
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
                    Console.WriteLine("\n............................................................");
                    // Print title
                    Console.WriteLine($"\n🎥 {movie.Title}");
                    Console.WriteLine("------------------------------");

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
                                Console.Write($"{movie.Genres[i].Name}\n");
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
                        Console.Write("👥 Top Cast: ");
                        for (int i = 0; i < movie.Actors.Count(); i++)
                        {
                            if (i == movie.Actors.Count() - 1)
                            {
                                Console.Write($"{movie.Actors[i].Name}\n");
                            }
                            else
                            {
                                Console.Write(movie.Actors[i].Name + ", ");
                            }
                        }
                    }
                    
                    Console.WriteLine("\n............................................................\n");
                }
                else
                {
                    Helpers.SetConsoleColor("red");
                    Console.WriteLine("❌ Movie not found.");
                    Helpers.ResetConsoleColor();
                }
            }
        }


        // DELETE MOVIE
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