using System;
using System.IO;

class Program
{
    private const string ConnectionStringEnvironmentVariable = "WORDCOUNTER_CONNECTION_STRING";
    private const string InputFilePathEnvironmentVariable = "WORDCOUNTER_INPUT_FILE";

    static void Main(string[] args)
    {
        string? filePath = args.Length > 0
            ? args[0]
            : Environment.GetEnvironmentVariable(InputFilePathEnvironmentVariable);

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine($"File path not configured. Pass a file path argument or set the {InputFilePathEnvironmentVariable} environment variable.");
            return;
        }

        if (File.Exists(filePath))
        {
            string text = File.ReadAllText(filePath);
            string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine($"Number of words: {words.Length}");

            string? connectionString = Environment.GetEnvironmentVariable(ConnectionStringEnvironmentVariable);
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                Console.WriteLine($"Skipping database save. Set the {ConnectionStringEnvironmentVariable} environment variable to enable it.");
            }
            else
            {
                var repo = new WordCountRepository(connectionString);
                repo.SaveWordCount(Path.GetFileName(filePath), words.Length);

                Console.WriteLine("Saved word count to database.");
            }
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}
