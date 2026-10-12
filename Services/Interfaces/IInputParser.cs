using FootballLeagueTable.Models;

namespace FootballLeagueTable.Services.Interfaces
{
    public interface IInputParser
    {
        IEnumerable<MatchResult> ParseMatches(string input);
    }
}