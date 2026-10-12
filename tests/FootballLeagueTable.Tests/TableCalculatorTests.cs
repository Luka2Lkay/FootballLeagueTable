using System.Linq;
using FootballLeagueTable.Models;
using FootballLeagueTable.Services;
using Xunit;

namespace FootballLeagueTable.Tests
{
    public class TableCalculatorTests
    {
        [Fact]
        public void AggregatesStatisticsCorrectly()
        {
            var matches = new[]
            {
                new MatchResult("A", "B", 2, 1),
                new MatchResult("B", "C", 1, 1),
                new MatchResult("C", "A", 0, 3),
            };

            var calc = new TableCalculator();
            var standings = calc.CalculateTable(matches).ToList();
            var a = standings.First(s => s.Team == "A");
            Assert.Equal(2, a.Played);
            Assert.Equal(2, a.Won);
            Assert.Equal(0, a.Drawn);
            Assert.Equal(0, a.Lost);
            Assert.Equal(5, a.GoalsFor);
            Assert.Equal(1, a.GoalsAgainst);
        }

        [Fact]
        public void TieBreaker_UsesGoalAverage()
        {
            var matches = new[]
            {
                new MatchResult("A", "B", 1, 0),
                new MatchResult("C", "D", 2, 1),
            };
            var calc = new TableCalculator();
            var standings = calc.CalculateTable(matches).ToList();
            Assert.Equal("C", standings[0].Team);
        }
    }
}
