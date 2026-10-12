using FootballLeagueTable.Models;

namespace FootballLeagueTable.Services;

public static class StandingsCsvWriter
{
    public static void Write(string path, IEnumerable<TeamStanding> standings)
    {
        using var w = new StreamWriter(path);
        w.WriteLine("Pos,Team,P,W,D,L,GF,GA,GD,GAve,Points");
        int pos = 1;
        foreach (var s in standings)
        {
            var gaFormatted = s.GoalAverage.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            w.WriteLine($"{pos},{Escape(s.Team)},{s.Played},{s.Wins},{s.Draws},{s.Losses},{s.GoalsFor},{s.GoalsAgainst},{s.GoalDifference},{gaFormatted},{s.Points}");
            pos++;
        }
    }

    private static string Escape(string v) => v.Contains(',') ? '"' + v.Replace("\"", "\"\"") + '"' : v;
}
