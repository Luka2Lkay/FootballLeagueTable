using System.Globalization;
using FootballLeagueTable.Models;

namespace FootballLeagueTable.Services;

public class CsvMatchParser
{
    /// <summary>
    /// Accepts CSV with header or without. Expected columns: Date?,HomeTeam,AwayTeam,HomeGoals,AwayGoals
    /// Date is optional and can be ignored.
    /// </summary>
    public IEnumerable<MatchResult> ParseFile(string path)
    {
        var lines = File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
        if (lines.Length == 0) yield break;

        int start = 0;
        // detect header
        if (lines[0].ToLower().Contains("team") || lines[0].ToLower().Contains("home") || lines[0].ToLower().Contains("date"))
        {
            start = 1;
        }

        for (int i = start; i < lines.Length; i++)
        {
            var parts = SplitCsvLine(lines[i]);
            if (parts.Length < 4) continue;

            int idx = 0;
            DateTime? date = null;
            // If first column looks like a date, try parse
            if (DateTime.TryParse(parts[0], CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
            {
                date = dt;
                idx = 1;
            }

            string home = parts[idx].Trim();
            string away = parts[idx + 1].Trim();
            if (!int.TryParse(parts[idx + 2], out var homeGoals)) continue;
            if (!int.TryParse(parts[idx + 3], out var awayGoals)) continue;

            yield return new MatchResult(date, home, away, homeGoals, awayGoals);
        }
    }

    private static string[] SplitCsvLine(string line)
    {
        // naive CSV split that handles simple quoted values
        var values = new List<string>();
        bool inQuotes = false;
        var current = new System.Text.StringBuilder();
        foreach (var c in line)
        {
            if (c == '"') { inQuotes = !inQuotes; continue; }
            if (c == ',' && !inQuotes)
            {
                values.Add(current.ToString());
                current.Clear();
                continue;
            }
            current.Append(c);
        }
        values.Add(current.ToString());
        return values.ToArray();
    }
}
