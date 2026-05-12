using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Accept file path as command-line argument or fall back to a local default
        string filePath = args.Length > 0 ? args[0] : Path.Combine(AppContext.BaseDirectory, "input.txt");

        if (File.Exists(filePath))
        {
            string text = File.ReadAllText(filePath);
            string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine($"Number of words: {words.Length}");

            // Read connection string from environment variable to avoid hardcoded credentials
            string? connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                Console.Error.WriteLine("Error: DB_CONNECTION_STRING environment variable is not set. " +
                    "Please set it to your PostgreSQL connection string. " +
                    "Example (replace values with your actual credentials): " +
                    "DB_CONNECTION_STRING=\"Host=myhost;Port=5432;Database=mydb;Username=myuser;Password=mypassword\"");
                Environment.Exit(1);
            }

            // Basic validation: ensure the connection string contains required components
            if (!connectionString.Contains("Host=", StringComparison.OrdinalIgnoreCase) ||
                !connectionString.Contains("Database=", StringComparison.OrdinalIgnoreCase))
            {
                Console.Error.WriteLine("Error: DB_CONNECTION_STRING appears to be malformed. " +
                    "It must include at least 'Host' and 'Database' components.");
                Environment.Exit(1);
            }

            var repo = new WordCountRepository(connectionString);
            repo.SaveWordCount(Path.GetFileName(filePath), words.Length);

            Console.WriteLine("Saved word count to database.");
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }
}
