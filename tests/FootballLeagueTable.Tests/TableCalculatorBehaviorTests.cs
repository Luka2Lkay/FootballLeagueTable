using System.Collections.Generic;
using System.Linq;
using FootballLeagueTable.Models;
using FootballLeagueTable.Services;
using Xunit;

namespace FootballLeagueTable.Tests
{
    public class TableCalculatorBehaviorTests
    {
        [Fact]
        public void WinnerGetsTwoPointsAndLoserGetsZero()
        {
            var matches = new List<MatchResult>
            {
                new MatchResult("Alpha", "Beta", 3, 1)
            };

            var calc = new TableCalculator();
            var table = calc.CalculateTable(matches).ToList();

            var alpha = table.First(t => t.Team == "Alpha");
            var beta = table.First(t => t.Team == "Beta");

            Assert.Equal(2, alpha.Points);
            Assert.Equal(0, beta.Points);
            Assert.Equal(1, alpha.Won);
            Assert.Equal(1, beta.Lost);
        }

        [Fact]
        public void DrawGivesOnePointToBothTeams()
        {
            var matches = new List<MatchResult>
            {
                new MatchResult("Alpha", "Beta", 2, 2)
            };

            var calc = new TableCalculator();
            var table = calc.CalculateTable(matches).ToList();

            var alpha = table.First(t => t.Team == "Alpha");
            var beta = table.First(t => t.Team == "Beta");

            Assert.Equal(1, alpha.Points);
            Assert.Equal(1, beta.Points);
            Assert.Equal(1, alpha.Drawn);
            Assert.Equal(1, beta.Drawn);
        }

        [Fact]
        public void PointsTakePrecedenceOverGoalAverage()
        {
            var matches = new List<MatchResult>
            {
                new MatchResult("TeamA", "X1", 1, 0),
                new MatchResult("TeamA", "X2", 1, 0),
                new MatchResult("TeamC", "Y", 10, 0)
            };

            var calc = new TableCalculator();
            var table = calc.CalculateTable(matches).ToList();

            var first = table[0].Team;
            var second = table[1].Team;

            Assert.Equal("TeamA", first);
            Assert.Equal("TeamC", second);
        }

        [Fact]
        public void GoalAverageBreaksTiesCorrectly()
        {

            var matches = new List<MatchResult>
            {
                new MatchResult("TeamA", "B", 2, 0),
                new MatchResult("TeamC", "D", 3, 2)
            };

            var calc = new TableCalculator();
            var table = calc.CalculateTable(matches).ToList();

            // TeamA should be ahead of TeamC due to superior goal average
            Assert.Equal("TeamA", table[0].Team);
            Assert.Equal("TeamC", table[1].Team);
        }


        [Fact]
        public void GoalsScoredBreaksEqualPointsAndGoalAverageTie()
        {
            var matches = new List<MatchResult>
    {
        new MatchResult("TeamA", "B", 2, 1),
        new MatchResult("TeamC", "D", 4, 2)
    };

            var calc = new TableCalculator();
            var table = calc.CalculateTable(matches).ToList();

            // Both teams have 2 points and a goal average of 2.0.
            // TeamC has scored more goals.
            Assert.Equal("TeamC", table[0].Team);
            Assert.Equal("TeamA", table[1].Team);
        }

    }
}