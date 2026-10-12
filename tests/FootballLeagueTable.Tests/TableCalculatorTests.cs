using System.Collections.Generic;
using System.Linq;
using FootballLeagueTable.Models;
using FootballLeagueTable.Services;
using Xunit;

namespace FootballLeagueTable.Tests
{
    public class TableCalculatorTests
    {
        [Fact]
        public void OrdersByPointsThenGoalAverageThenGoalsForThenName()
        {
            var matches = new List<MatchResult>
            {
                new MatchResult("A", "B", 2, 0), // A wins
                new MatchResult("C", "D", 2, 0), // C wins
                // Make A and C equal on points and GF/GA so name decides
                new MatchResult("E", "F", 1, 1)  // draw
            };

            var calc = new TableCalculator();
            var table = calc.CalculateTable(matches).ToList();

            Assert.Equal("A", table[0].Team);
            Assert.Equal("C", table[1].Team);
        }
    }
}