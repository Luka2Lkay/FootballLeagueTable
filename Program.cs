using System.Globalization;
using FootballLeagueTable.Models;
using FootballLeagueTable.Services;

// CLI behaviour:
// - No args: read stdin, write stdout
// - One arg: input file (use stdout)
// - Two args: input file and output file

string? inputPath = null;
string? outputPath = null;

if (args.Length == 0)
{
    // use stdin/stdout
}
else if (args.Length == 1)
{
    inputPath = args[0];
}
else
{
    inputPath = args[0];
    outputPath = args[1];
}

IEnumerable<string> lines;
if (string.IsNullOrEmpty(inputPath) || inputPath == "-")
{
    // read from stdin
    var stdin = Console.In.ReadToEnd();
    lines = stdin.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
}
else
{
    if (!File.Exists(inputPath))
    {
        Console.Error.WriteLine($"Input file not found: {inputPath}");
        return 1;
    }
    lines = File.ReadAllLines(inputPath).Where(l => !string.IsNullOrWhiteSpace(l));
}

var parser = new CsvMatchParser();
// CsvMatchParser.ParseFile accepts a path; create a small adapter to parse from lines
IEnumerable<MatchResult> matchesFromLines(IEnumerable<string> inputLines)
{
    // if the first line is a header, join and write to a temp file for reuse of parser
    var arr = inputLines.ToArray();
    // write to temp file and reuse parser to keep parsing consistent
    var temp = Path.GetTempFileName();
    File.WriteAllLines(temp, arr);
    try
    {
        return parser.ParseFile(temp).ToList();
    }
    finally
    {
        File.Delete(temp);
    }
}

var matches = matchesFromLines(lines);
var calculator = new LeagueCalculator(winPoints: 2, drawPoints: 1);
var standings = calculator.CalculateStandings(matches);

if (string.IsNullOrEmpty(outputPath) || outputPath == "-")
{
    // write to stdout
    using var sw = Console.Out;
    sw.WriteLine("Position,Team,Played,Won,Drawn,Lost,GoalsFor,GoalsAgainst,GoalAverage,Points");
    int pos = 1;
    foreach (var s in standings)
    {
        var gaFormatted = s.GoalAverage.ToString("0.00", CultureInfo.InvariantCulture);
        sw.WriteLine($"{pos},{Escape(s.Team)},{s.Played},{s.Wins},{s.Draws},{s.Losses},{s.GoalsFor},{s.GoalsAgainst},{gaFormatted},{s.Points}");
        pos++;
    }
}
else
{
    Directory.CreateDirectory(Path.GetDirectoryName(outputPath) ?? ".");
    StandingsCsvWriter.Write(outputPath, standings);
    Console.WriteLine($"Standings written to: {outputPath}");
}

return 0;

static string Escape(string v) => v.Contains(',') ? '"' + v.Replace("\"", "\"\"") + '"' : v;
