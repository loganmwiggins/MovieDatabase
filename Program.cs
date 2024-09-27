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


    //public static void AddAssignment()
    //{
    //    using (var context = new SchoolDbContext())
    //    {
    //        Console.WriteLine("Enter assignment name:");
    //        string name = Console.ReadLine() ?? "";

    //        Console.WriteLine("Enter assignment type:");
    //        string type = Console.ReadLine() ?? "";

    //        Console.WriteLine("Enter assignment difficulty:");
    //        string difficulty = Console.ReadLine() ?? "";

    //        context.Database.ExecuteSql($"INSERT INTO \"Assignments\" (\"Name\", \"Type\", \"Difficulty\") VALUES ({name}, {type}, {difficulty})");
    //        Console.WriteLine("Assignment added successfully.");

    //    }
    //}

    //public static void AddStudent()
    //{
    //    using (var context = new SchoolDbContext())
    //    {
    //        Console.WriteLine("Enter student first name:");
    //        string firstName = Console.ReadLine() ?? "";

    //        Console.WriteLine("Enter student last name:");
    //        string lastName = Console.ReadLine() ?? "";

    //        Console.WriteLine("Enter student program:");
    //        string program = Console.ReadLine() ?? "";

    //        context.Database.ExecuteSql($"INSERT INTO \"Students\" (\"FirstName\", \"LastName\", \"Program\") VALUES ({firstName}, {lastName}, {program})");
    //        Console.WriteLine("Student added successfully.");
    //    }
    //}

    //public static void AddGrade()
    //{
    //    using (var context = new SchoolDbContext())
    //    {
    //        try 
    //        {
    //            Console.WriteLine("Enter grade ID (or leave blank to add a new grade):");
    //            string input = Console.ReadLine() ?? "";
    //            int gradeId = string.IsNullOrEmpty(input) ? -1 : int.Parse(input);

    //            Console.WriteLine("Enter student ID:");
    //            int studentId = int.Parse(Console.ReadLine() ?? "");

    //            Console.WriteLine("Enter assignment ID:");
    //            int assignmentId = int.Parse(Console.ReadLine() ?? "");

    //            Console.WriteLine("Enter grade score:");
    //            int score = int.Parse(Console.ReadLine() ?? "");

    //            var existingGrade = context.Grades
    //            .FromSql($"SELECT * FROM \"Grades\" WHERE \"GradeId\" = {gradeId}")
    //            .FirstOrDefault();

    //            if (existingGrade == null)
    //            {
    //                context.Database.ExecuteSql($"INSERT INTO \"Grades\" (\"StudentId\", \"AssignmentId\", \"Score\") VALUES ({studentId}, {assignmentId}, {score})");
    //                Console.WriteLine("Grade recorded successfully.");
    //            } else {
    //                context.Database.ExecuteSql($"UPDATE \"Grades\" SET \"StudentId\" = {studentId}, \"AssignmentId\" = {assignmentId}, \"Score\" = {score} WHERE \"GradeId\" = {gradeId}");
    //            }

    //        } 
    //        catch (FormatException)
    //        {
    //            Console.WriteLine("Invalid input. Please enter a valid integer for the Grade ID.");
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    //        }
    //    }
    //}

    //public static void ViewData()
    //{
    //    using (var context = new SchoolDbContext())
    //    {
    //        Console.WriteLine("\nStudents:");
    //        var students = context.Students.FromSql($"SELECT * FROM \"Students\" ORDER BY \"StudentId\"").ToList();
    //        foreach (var student in students)
    //        {
    //            Console.WriteLine($"{student.StudentId}: {student.FirstName} {student.LastName}, Program: {student.Program}");
    //        }

    //        Console.WriteLine("\nAssignments:");
    //        var assignments = context.Assignments.FromSql($"SELECT * FROM \"Assignments\" ORDER BY \"AssignmentId\"").ToList();
    //        foreach (var assignment in assignments)
    //        {
    //            Console.WriteLine($"{assignment.AssignmentId}: {assignment.Name}, Type: {assignment.Type}, Difficulty: {assignment.Difficulty}");
    //        }

    //        Console.WriteLine("\nGrades:");
    //        var grades = context.Grades.FromSql($"SELECT * FROM \"Grades\" ORDER BY \"GradeId\"").ToList();
    //        foreach (var grade in grades)
    //        {
    //            Console.WriteLine($"GradeId: {grade.GradeId}, StudentId: {grade.StudentId}, AssignmentId: {grade.AssignmentId}, Score: {grade.Score}");
    //        }
    //    }
    //}

    //public static void DeleteGrade()
    //{
    //    using (var context = new SchoolDbContext())
    //    {
    //        Console.WriteLine("Enter the Grade ID of the grade you want to delete:");

    //        try
    //        {
    //            int gradeId = int.Parse(Console.ReadLine() ?? "");

    //            var existingGrade = context.Grades
    //                .FromSql($"SELECT * FROM \"Grades\" WHERE \"GradeId\" = {gradeId}")
    //                .FirstOrDefault();

    //            if (existingGrade != null)
    //            {
    //                context.Database.ExecuteSql($"DELETE FROM \"Grades\" WHERE \"GradeId\" = {gradeId}");
    //                Console.WriteLine("Grade deleted successfully.");
    //            }
    //            else
    //            {
    //                Console.WriteLine("Grade not found.");
    //            }
    //        }
    //        catch (FormatException)
    //        {
    //            Console.WriteLine("Invalid input. Please enter a valid integer for the Grade ID.");
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    //        }
    //    }
    //}
}