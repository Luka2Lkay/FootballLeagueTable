using FootballLeagueTable.Models;

namespace FootballLeagueTable.Services;

public class LeagueCalculator
{
    private readonly int _winPoints;
    private readonly int _drawPoints;

    public LeagueCalculator(int winPoints = 2, int drawPoints = 1)
    {
        _winPoints = winPoints;
        _drawPoints = drawPoints;
    }

    public List<TeamStanding> CalculateStandings(IEnumerable<MatchResult> matches)
    {
        var table = new Dictionary<string, TeamStanding>(StringComparer.OrdinalIgnoreCase);

        void Ensure(string team)
        {
            if (!table.ContainsKey(team))
                table[team] = new TeamStanding { Team = team };
        }

        foreach (var m in matches)
        {
            Ensure(m.HomeTeam);
            Ensure(m.AwayTeam);

            var home = table[m.HomeTeam];
            var away = table[m.AwayTeam];

            home.Played++; away.Played++;
            home.GoalsFor += m.HomeGoals; home.GoalsAgainst += m.AwayGoals;
            away.GoalsFor += m.AwayGoals; away.GoalsAgainst += m.HomeGoals;

            if (m.HomeGoals > m.AwayGoals)
            {
                home.Wins++; away.Losses++;
                home.Points += _winPoints;
            }
            else if (m.HomeGoals < m.AwayGoals)
            {
                away.Wins++; home.Losses++;
                away.Points += _winPoints;
            }
            else
            {
                home.Draws++; away.Draws++;
                home.Points += _drawPoints; away.Points += _drawPoints;
            }
        }

        var list = table.Values.ToList();
        // Sort by Points desc, GoalAverage desc, GoalsFor desc, Team asc
        list.Sort((a, b) =>
        {
            var cmp = b.Points.CompareTo(a.Points);
            if (cmp != 0) return cmp;
            var ga = b.GoalAverage.CompareTo(a.GoalAverage);
            if (ga != 0) return ga;
            var gf = b.GoalsFor.CompareTo(a.GoalsFor);
            if (gf != 0) return gf;
            return string.Compare(a.Team, b.Team, StringComparison.OrdinalIgnoreCase);
        });

        return list;
    }
}
