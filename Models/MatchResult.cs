using System;

namespace FootballLeagueTable.Models
{
    public sealed class MatchResult
    {
        public MatchResult(string homeTeam, string awayTeam, int homeGoals, int awayGoals)
        {

            if (string.IsNullOrWhiteSpace(homeTeam))
            {
                throw new ArgumentException("Home team name cannot be null or whitespace.", nameof(homeTeam));
            }
            if (string.IsNullOrWhiteSpace(awayTeam))
            {
                throw new ArgumentException("Away team name cannot be null or whitespace.", nameof(awayTeam));
            }
            if (homeGoals < 0)
            {
                throw new ArgumentException("Home goals cannot be negative.", nameof(homeGoals));
            }
            if (awayGoals < 0)
            {
                throw new ArgumentException("Away goals cannot be negative.", nameof(awayGoals));
            }

            if (string.Equals(homeTeam, awayTeam, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("A team cannot play against itself.");
            }

            HomeTeam = homeTeam.Trim();
            AwayTeam = awayTeam.Trim();
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
        }



        public string HomeTeam { get; }
        public string AwayTeam { get; }
        public int HomeGoals { get; }
        public int AwayGoals { get; }
    }
}