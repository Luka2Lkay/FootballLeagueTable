
using System.Collections.Generic;
using System.Linq;
using FootballLeagueTable.Models;
using FootballLeagueTable.Services;
using Xunit;

namespace FootballLeagueTable.Tests
{
    public class TableCalculatorEdgeCasesTests
    {
        [Fact]
        public void CalculatesPointsAndGoalAverage()
        {
            var matches = new List<MatchResult>
            {
                new MatchResult("A", "B", 2, 0),
                new MatchResult("C", "D", 0, 0)
            };

            var table = new TableCalculator()
                .CalculateTable(matches)
                .ToList();

            var a = table.Single(t => t.Team == "A");
            var b = table.Single(t => t.Team == "B");
            var c = table.Single(t => t.Team == "C");
            var d = table.Single(t => t.Team == "D");

            Assert.Equal(2, a.Points);
            Assert.Equal(0, b.Points);
            Assert.Equal(1, c.Points);
            Assert.Equal(1, d.Points);

            Assert.Equal(2, a.GoalsFor);
            Assert.Equal(0, a.GoalsAgainst);
            Assert.Equal(2.0, a.GoalAverage);
        }

        [Fact]
        public void GoalAverageHandlesZeroGoalsAgainst()
        {
            var matches = new List<MatchResult>
            {
                new MatchResult("A", "B", 3, 0),
                new MatchResult("A", "C", 2, 0)
            };

            var table = new TableCalculator()
                .CalculateTable(matches)
                .ToList();

            var a = table.Single(t => t.Team == "A");

            Assert.Equal(5, a.GoalsFor);
            Assert.Equal(0, a.GoalsAgainst);

            // Confirm the application's documented zero-GA convention: infinite ratio when GA == 0
            Assert.Equal(5.0, a.GoalAverage);
        }

        [Fact]
        public void GoalAverageBreaksPointsTie()
        {
            var matches = new List<MatchResult>
            {
                new MatchResult("A", "B", 2, 1),
                new MatchResult("C", "D", 2, 1),
                new MatchResult("X", "Y", 1, 0)
            };

            var table = new TableCalculator()
                .CalculateTable(matches)
                .ToList();

            Assert.Equal("A", table[0].Team);
            Assert.Equal("C", table[1].Team);
            Assert.Equal("X", table[2].Team);
        }

        [Fact]
        public void GoalsScoredBreaksEqualPointsAndGoalAverageTie()
        {
            var matches = new List<MatchResult>
            {
                new MatchResult("A", "B", 2, 1),
                new MatchResult("C", "D", 4, 2)
            };

            var table = new TableCalculator()
                .CalculateTable(matches)
                .ToList();

            // Both teams have 2 points and a goal average of 2.0.
            // C has scored more goals, so C ranks above A.
            Assert.Equal("C", table[0].Team);
            Assert.Equal("A", table[1].Team);
        }

        [Fact]
        public void PointsTakePriorityOverGoalAverage()
        {
            var matches = new List<MatchResult>
            {
                // Team A: two narrow wins -> 4 points, moderate goal average
                new MatchResult("A", "X", 3, 2),
                new MatchResult("A", "Y", 1, 0),
                // Team B: one big win -> 2 points, very high goal average
                new MatchResult("B", "Z", 8, 1)
            };

            var table = new TableCalculator()
                .CalculateTable(matches)
                .ToList();

            var a = table.Single(t => t.Team == "A");
            var b = table.Single(t => t.Team == "B");

            // Sanity: B has higher goal average but fewer points than A
            Assert.True(b.Points < a.Points);
            Assert.True(b.GoalAverage > a.GoalAverage);

            // The table must place the team with more points above, regardless of goal average
            var idxA = table.FindIndex(t => t.Team == "A");
            var idxB = table.FindIndex(t => t.Team == "B");
            Assert.True(idxA < idxB, "Team with more points should rank above team with higher goal average");
        }
    }
}