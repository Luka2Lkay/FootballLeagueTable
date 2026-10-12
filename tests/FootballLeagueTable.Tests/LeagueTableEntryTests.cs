using FootballLeagueTable.Models;
using Xunit;

namespace FootballLeagueTable.Tests
{
    public class LeagueTableEntryTests
    {
        [Fact]
        public void GoalAverage_WhenGoalsAgainstZero_ReturnsGoalsFor()
        {
            var e = new LeagueTableEntry { Team = "X", GoalsFor = 5, GoalsAgainst = 0 };
            Assert.Equal(5.0, e.GoalAverage);
        }

        [Fact]
        public void GoalAverage_ComputesRatio()
        {
            var e = new LeagueTableEntry { Team = "Y", GoalsFor = 3, GoalsAgainst = 2 };
            Assert.Equal(1.5, e.GoalAverage, 3);
        }

        [Fact]
        public void Points_AreComputedAsTwoPerWinAndOnePerDraw()
        {
            var e = new LeagueTableEntry { Team = "Z", Won = 3, Drawn = 2 };
            Assert.Equal(3 * 2 + 2, e.Points);
        }
    }
}
