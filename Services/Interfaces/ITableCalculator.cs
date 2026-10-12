using FootballLeagueTable.Models;

namespace FootballLeagueTable.Services.Interfaces
{
    public interface ITableCalculator
    {
        IEnumerable<LeagueTableEntry> CalculateTable(IEnumerable<MatchResult> matches);
    }
}