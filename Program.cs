using System.Globalization;
using FootballLeagueTable.Services;
using FootballLeagueTable.Models;
using FootballLeagueTable.Cli;


// Usage:
// dotnet run -- [input.csv] [output.csv]
// If input is omitted, read from stdin.
// If output is omitted, write to stdout.

if (args.Length > 2)
{
    Console.Error.WriteLine(
        "Usage: FootballLeagueTable.Cli [input.csv] [output.csv]");
    return 1;
}

var opts = ArgsParser.Parse(args);
string? inputPath = opts.InputPath;
string? outputPath = opts.OutputPath;

try
{
    string input;

    if (!string.IsNullOrWhiteSpace(inputPath))
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return 1;
        }

        input = await File.ReadAllTextAsync(inputPath);
    }
    else
    {
        using StreamReader reader = new StreamReader(Console.OpenStandardInput());
        input = await reader.ReadToEndAsync();
    }

    InputParser parser = new InputParser();
    IEnumerable<MatchResult> matches = parser.ParseMatches(input);

    TableCalculator calculator = new TableCalculator();

    List<LeagueTableEntry> table = calculator.CalculateTable(matches).ToList();

    using StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);

    writer.WriteLine(
        "Position,Team,Played,Won,Drawn,Lost,GoalsFor,GoalsAgainst,GoalAverage,Points");

    for (int i = 0; i < table.Count(); i++)
    {
        LeagueTableEntry entry = table[i];

        writer.WriteLine(
            $"{i + 1}," +
            $"{EscapeCsv(entry.Team)}," +
            $"{entry.Played.ToString(CultureInfo.InvariantCulture)}," +
            $"{entry.Won.ToString(CultureInfo.InvariantCulture)}," +
            $"{entry.Drawn.ToString(CultureInfo.InvariantCulture)}," +
            $"{entry.Lost.ToString(CultureInfo.InvariantCulture)}," +
            $"{entry.GoalsFor.ToString(CultureInfo.InvariantCulture)}," +
            $"{entry.GoalsAgainst.ToString(CultureInfo.InvariantCulture)}," +
            $"{entry.GoalAverage.ToString("0.00", CultureInfo.InvariantCulture)}," +
            $"{entry.Points.ToString(CultureInfo.InvariantCulture)}");
    }

    string output = writer.ToString();

    if (!string.IsNullOrWhiteSpace(outputPath))
    {
        await File.WriteAllTextAsync(outputPath, output);
    }
    else
    {
        Console.Write(output);
    }

    return 0;
}
catch (Exception ex) when (
    ex is IOException ||
    ex is UnauthorizedAccessException ||
    ex is ArgumentException ||
    ex is FormatException ||
    ex is InvalidOperationException)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    return 1;
}

static string EscapeCsv(string value)
{
    if (value.Contains(',') ||
        value.Contains('"') ||
        value.Contains('\r') ||
        value.Contains('\n'))
    {
        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }

    return value;
}