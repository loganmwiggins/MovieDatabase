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
        }



        // Print list of movies to console
        public static void DisplayMovieList()
        {
            Console.WriteLine(@"
                                                                                                           
███╗   ██╗ ██████╗ ██╗    ██╗    ██████╗ ██╗      █████╗ ██╗   ██╗██╗███╗   ██╗ ██████╗    
████╗  ██║██╔═══██╗██║    ██║    ██╔══██╗██║     ██╔══██╗╚██╗ ██╔╝██║████╗  ██║██╔════╝ ██╗
██╔██╗ ██║██║   ██║██║ █╗ ██║    ██████╔╝██║     ███████║ ╚████╔╝ ██║██╔██╗ ██║██║  ███╗╚═╝
██║╚██╗██║██║   ██║██║███╗██║    ██╔═══╝ ██║     ██╔══██║  ╚██╔╝  ██║██║╚██╗██║██║   ██║██╗
██║ ╚████║╚██████╔╝╚███╔███╔╝    ██║     ███████╗██║  ██║   ██║   ██║██║ ╚████║╚██████╔╝╚═╝
╚═╝  ╚═══╝ ╚═════╝  ╚══╝╚══╝     ╚═╝     ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝╚═╝  ╚═══╝ ╚═════╝    
                                                                                                
");

            Console.WriteLine("🎬 IN THEATERS:");
            Console.WriteLine("--------------------");

            using (var context = new MoviesDbContext())
            {
                List<Movie> movieList = context.Movies.ToList();

                foreach (Movie movie in movieList)
                {
                    Console.WriteLine($"[{movie.MovieId}]   {movie.Title}");
                }
            }
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