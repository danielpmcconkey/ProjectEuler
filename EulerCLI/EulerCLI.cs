using EulerProblems.Lib;
using EulerProblems.Lib.Problems;
using System.Text.Json;
using EulerProblems.Lib.DAL.Data;
using EulerProblems.Lib.DAL.Operations;

class Program
{
    static void Main(string[] args)
    {
        // Check if problem number argument is provided
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a problem number as a command-line argument.");
            Console.WriteLine("Usage: dotnet run <problem_number>");
            return;
        }

        // Parse the problem number from command-line argument
        if (!int.TryParse(args[0], out int problemNumber))
        {
            Console.WriteLine($"Invalid problem number: {args[0]}");
            Console.WriteLine("Please provide a valid integer.");
            return;
        }

        try
        {
            var euler = EulerProblemFactory.GetEulerProblemClassByNumber(problemNumber);
            euler.Solve();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error solving problem {problemNumber}: {ex.Message}");
        }
    }
}