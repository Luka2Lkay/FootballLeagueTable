namespace FootballLeagueTable.Models;

public record MatchResult
(
    DateTime? Date,
    string HomeTeam,
    string AwayTeam,
    int HomeGoals,
    int AwayGoals
);
