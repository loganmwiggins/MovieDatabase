using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jm_sql
{
    internal class Helpers
    {
        public static void PrintWelcome()
        {
            SetConsoleColor("cyan");
            Console.WriteLine(@"                                     |
                          ___________I____________
                         ( _____________________ ()
                       _.-'|                    ||
                   _.-'   ||     WELCOME TO     ||
  ______       _.-'       ||                    ||
 |      |_ _.-'           ||      THE LOCAL     ||
 |      |_|_              ||                    ||
 |______|   `-._          ||       MOVIE        ||
    /\          `-._      ||                    ||
   /  \             `-._  ||      DATABASE      ||
  /    \                `-.I____________________||
 /      \                 ------------------------
/________\___________________/________________\______");

            ResetConsoleColor();
        }



        // Print list of movies to console
        public static void DisplayMovieList()
        {
            Console.WriteLine("|-----------------|");
            Console.WriteLine("| 🎬 IN THEATERS: |");
            Console.WriteLine("|-----------------|");

            using (var context = new MoviesDbContext())
            {
                // Display full movie list
                List<Movie> movieList = context.Movies.ToList();

                foreach (Movie movie in movieList)
                {
                    Console.WriteLine($"[{movie.MovieId}]   {movie.Title}");
                }

                // Prompt user to select a movie for details
                string? movieNum = Helpers.GetUserInput($"\n➡️ Which movie would you like to view?:");

                while (true)    // User choice input validation
                {
                    if (string.IsNullOrEmpty(movieNum) || !int.TryParse(movieNum, out int result) || int.Parse(movieNum) < 1)
                    {
                        Helpers.SetConsoleColor("yellow");
                        Console.WriteLine("⚠️ Invalid selection. Try again...\n");
                        Helpers.ResetConsoleColor();
                        movieNum = Helpers.GetUserInput($"➡️ Which movie would you like to view?:");
                    }
                    else break;
                }

                CRUD.ViewMovieDetail(int.Parse(movieNum));
            }
        }

        public static void DisplayGenreList()
        {
            using (var context = new MoviesDbContext())
            {
                // Display full genre list
                List<Genre> genreList = context.Genres.ToList();

                foreach (Genre genre in genreList)
                {
                    Console.WriteLine($"[{genre.GenreId}]   {genre.Name}");
                }
            }
        }


        // Print main select menu
        public static void DisplayMainMenu()
        {
            Console.WriteLine("\n|-------------------|");
            Console.WriteLine("| ➡️ PLEASE SELECT: |");
            Console.WriteLine("|-------------------|");

            Console.WriteLine("[V]   View movie list.");
            Console.WriteLine("[A]   Add or update movie.");
            Console.WriteLine("[D]   Delete a movie.");
            Console.WriteLine("[Q]   Quit the application.\n");
        }



        // Method to prompt the user for input
        public static string? GetUserInput(string prompt)
        {
            Console.Write(prompt + " ");
            Helpers.ResetConsoleColor();
            string? inputLine = Console.ReadLine();

            return string.IsNullOrEmpty(inputLine) ? null : inputLine;
        }


        // Method to change the console text color
        public static void SetConsoleColor(string color)
        {
            switch (color)
            {
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                default:
                    break;
            }
        }

        // Method to reset the console text color to white
        public static void ResetConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}