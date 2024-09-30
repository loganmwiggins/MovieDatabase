using jm_sql;
using Microsoft.EntityFrameworkCore;

public static class Program
{
    public static void Main()
    {
        //    using (var context = new MoviesDbContext())
        //    {
        //        // Drop and create the database
        //        // context.Database.EnsureDeleted();
        //        // context.Database.EnsureCreated();
        //    }

        Console.Clear();
        Helpers.PrintWelcome();

        // Begin interaction loop
        bool exit = false;
        while (!exit)
        {
            Helpers.DisplayMainMenu();
            
            var input = Console.ReadKey(intercept: true).Key;
            switch (input)
            {
                case ConsoleKey.V:
                    Helpers.DisplayMovieList();
                    break;

                case ConsoleKey.A:
                    CRUD.AddMovie();
                    break;

                case ConsoleKey.D:
                    CRUD.DeleteMovie();
                    break;

                case ConsoleKey.Q:
                    exit = true;
                    break;

                default:
                    Helpers.SetConsoleColor("yellow");
                    Console.WriteLine("⚠️ Invalid selection. Try again...");
                    Helpers.ResetConsoleColor();
                    break;
            }
        }
    }
}