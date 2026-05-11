using System;
using System.IO;

class Program
{
    private const string ConnectionStringEnvironmentVariable = "WORDCOUNTER_CONNECTION_STRING";
    private const string InputPathEnvironmentVariable = "WORDCOUNTER_INPUT_PATH";

    static void Main(string[] args)
    {
        string filePath = ResolveFilePath(args);
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            Environment.ExitCode = 1;
            return;
        }

        string text = File.ReadAllText(filePath);
        string[] words = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine($"Number of words: {words.Length}");

        string? connectionString = Environment.GetEnvironmentVariable(ConnectionStringEnvironmentVariable);
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Console.WriteLine($"Skipped database save because {ConnectionStringEnvironmentVariable} is not set.");
            return;
        }

        try
        {
            var repo = new WordCountRepository(connectionString);
            repo.SaveWordCount(Path.GetFileName(filePath), words.Length);
            Console.WriteLine("Saved word count to database.");
        }
        catch (InvalidOperationException ex)
        {
            Console.Error.WriteLine(ex.Message);
            Environment.ExitCode = 1;
        }
    }

    private static string ResolveFilePath(string[] args)
    {
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            return Path.GetFullPath(args[0]);
        }

        string? configuredPath = Environment.GetEnvironmentVariable(InputPathEnvironmentVariable);
        if (!string.IsNullOrWhiteSpace(configuredPath))
        {
            return Path.GetFullPath(configuredPath);
        }

        return Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
    }
}
