
using System.Text.RegularExpressions;
using FootballLeagueTable.Models;
using FootballLeagueTable.Services.Interfaces;

namespace FootballLeagueTable.Services
{
    // Supports CSV rows:
    // HomeTeam,AwayTeam,HomeGoals,AwayGoals
    // and plain text:
    // Arsenal 2-1 Liverpool
    public class InputParser : IInputParser
    {
        private static readonly Regex LineRegex = new(
            @"^\s*(.+?)\s+(\d+)\s*-\s*(\d+)\s+(.+?)\s*$");

        public IEnumerable<MatchResult> ParseMatches(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                yield break;

            string[] lines = input.Split(
                new[] { '\r', '\n' },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (string raw in lines)
            {
                string line = raw.Trim();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(',');

                if (parts.Length >= 4)
                {
                    if (parts[0].Trim().Equals(
                        "HomeTeam",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        // skip header line
                        continue;
                    }

                    string home = parts[0].Trim();
                    string away = parts[1].Trim();

                    if (string.IsNullOrWhiteSpace(home) ||
                        string.IsNullOrWhiteSpace(away))
                    {
                        Console.Error.WriteLine("Warning: Invalid team names in line: " + line);
                        continue;
                    }

                    if (!int.TryParse(parts[2].Trim(), out int homeGoals) ||
                        !int.TryParse(parts[3].Trim(), out int awayGoals))
                    {
                        Console.Error.WriteLine("Warning: Invalid score format in line: " + line);
                        continue;
                    }

                    if (homeGoals < 0 || awayGoals < 0)
                    {
                        Console.Error.WriteLine("Warning: Negative score in line: " + line);
                        continue;
                    }

                    yield return new MatchResult(
                        home, away, homeGoals, awayGoals);

                    continue;
                }

                Match match = LineRegex.Match(line);

                if (!match.Success)
                {
                    Console.Error.WriteLine("Warning: Invalid line format: " + line);
                    continue;
                }

                string homeTeam = match.Groups[1].Value.Trim();
                string awayTeam = match.Groups[4].Value.Trim();

                if (string.IsNullOrWhiteSpace(homeTeam) ||
                    string.IsNullOrWhiteSpace(awayTeam))
                {
                    Console.Error.WriteLine("Warning: Invalid team names in line: " + line);
                    continue;
                }

                if (!int.TryParse(match.Groups[2].Value, out int homeScore) ||
                    !int.TryParse(match.Groups[3].Value, out int awayScore))
                {
                    Console.Error.WriteLine("Warning: Invalid score format in line: " + line);
                    continue;
                }

                if (homeScore < 0 || awayScore < 0)
                {
                    Console.Error.WriteLine("Warning: Negative score in line: " + line);
                    continue;
                }

                yield return new MatchResult(
                    homeTeam, awayTeam, homeScore, awayScore);
            }
        }
    }
}