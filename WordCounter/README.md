# WordCounter Project

## Description
The WordCounter project is a simple .NET console application that counts the words in a text file and can optionally save the result to PostgreSQL.

## Features
- **Word Count**: Quickly counts the number of words in the input.
- **Character Count**: Provides the total number of characters, including spaces.
- **Line Count**: Counts the number of lines in the input.
- **Tokenization**: Breaks down the text into tokens for more detailed analysis. 

## Setup Instructions
1. Clone the repository
   ```bash
   git clone https://github.com/Pandiannpn/Samples.git
   cd Samples/WordCounter
   ```
2. Restore and run the application
   ```bash
   dotnet run
   ```
3. Optionally configure a PostgreSQL connection string before running if you want to save the word count result:
   ```bash
   export WORDCOUNTER_CONNECTION_STRING="Host=localhost;Port=5432;Database=Powerhouse;Username=postgres;Password=your-password"
   dotnet run
   ```

## Usage
By default, the application reads `input.txt` from the current working directory.

- To use a different file, pass the path as the first command-line argument:
  ```bash
  dotnet run -- ./path/to/input.txt
  ```
- Or set the `WORDCOUNTER_INPUT_PATH` environment variable.
- If `WORDCOUNTER_CONNECTION_STRING` is set, the app will also save the word count to the configured PostgreSQL database.

## Project Structure
- `Program.cs`: Entry point for the console application.
- `DBWords.cs`: PostgreSQL persistence logic for saving and retrieving word counts.
- `README.md`: Documentation for the project.

## Dependencies
- .NET 10 SDK
- PostgreSQL (optional, only if database persistence is enabled)
- NuGet packages restored from `WordCounter.csproj`

## Contribution
Contributions are welcome! Please open an issue or submit a pull request if you have suggestions or improvements.
