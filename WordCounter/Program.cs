using System;
using System.IO;

class Program
{
    private const string ConnectionStringEnvironmentVariable = "WORDCOUNTER_CONNECTION_STRING";

    static void Main(string[] args)
    {
        string filePath = @"C:\Pandian\GitHubCopilotTraining\Sample .NetApp\SampleWebApp\WordCounter\input.txt";
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
