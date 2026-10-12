using FootballLeagueTable.Models;
using FootballLeagueTable.Services.Interfaces;

namespace FootballLeagueTable.Services
{
    public class TableCalculator : ITableCalculator
    {
        public IEnumerable<LeagueTableEntry> CalculateTable(IEnumerable<MatchResult> matches)
        {

            if (matches == null)
            {
                throw new ArgumentNullException(nameof(matches));

            }

            Dictionary<string, LeagueTableEntry> table = new Dictionary<string, LeagueTableEntry>(StringComparer.OrdinalIgnoreCase);

            foreach (MatchResult match in matches)
            {

                if (match == null)
                {
                    throw new ArgumentException("The match collection cannot contain null values.", nameof(matches));
                }

                if (!table.ContainsKey(match.HomeTeam))
                {
                    table[match.HomeTeam] = new LeagueTableEntry { Team = match.HomeTeam };
                }

                var home = table[match.HomeTeam];

                if (!table.ContainsKey(match.AwayTeam))
                {
                    table[match.AwayTeam] = new LeagueTableEntry { Team = match.AwayTeam };
                }

                var away = table[match.AwayTeam];

                home.Played++;
                away.Played++;
                home.GoalsFor += match.HomeGoals;
                home.GoalsAgainst += match.AwayGoals;
                away.GoalsFor += match.AwayGoals;
                away.GoalsAgainst += match.HomeGoals;

                if (match.HomeGoals > match.AwayGoals)
                {
                    home.Won++;
                    away.Lost++;
                }
                else if (match.HomeGoals < match.AwayGoals)
                {
                    away.Won++;
                    home.Lost++;
                }
                else
                {
                    home.Drawn++;
                    away.Drawn++;
                }
            }

            return table.Values
                .OrderByDescending(e => e.Points)
                .ThenByDescending(e => e.GoalAverage)
                .ThenByDescending(e => e.GoalsFor)
                .ThenBy(e => e.Team, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
    }
}