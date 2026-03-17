using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string filePath = @"C:\Pandian\GitHubCopilotTraining\Sample .NetApp\SampleWebApp\WordCounter\input.txt";
        if (File.Exists(filePath))
        {
            string text = File.ReadAllText(filePath);
            string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine($"Number of words: {words.Length}");
         // Update this connection string with your actual Postgres host/port/credentials
        string connectionString = "Host=localhost;Port=5432;Database=Powerhouse;Username=postgres;Password=admin";

        var repo = new WordCountRepository(connectionString);
        repo.SaveWordCount(Path.GetFileName(filePath), words.Length);

        Console.WriteLine("Saved word count to database.");   
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}
