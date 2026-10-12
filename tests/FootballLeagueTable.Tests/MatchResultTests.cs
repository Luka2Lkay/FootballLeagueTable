using System;
using FootballLeagueTable.Models;
using Xunit;

namespace FootballLeagueTable.Tests
{
    public class MatchResultTests
    {
        [Fact]
        public void Constructor_ThrowsOnInvalidNames()
        {
            Assert.Throws<ArgumentException>(() => new MatchResult("", "Away", 1, 0));
            Assert.Throws<ArgumentException>(() => new MatchResult("Home", " ", 1, 0));
        }

        [Fact]
        public void Constructor_ThrowsOnNegativeGoals()
        {
            Assert.Throws<ArgumentException>(() => new MatchResult("Home", "Away", -1, 0));
            Assert.Throws<ArgumentException>(() => new MatchResult("Home", "Away", 0, -2));
        }

        [Fact]
        public void Properties_AreSetCorrectly()
        {
            var m = new MatchResult("Arsenal", "Liverpool", 2, 1);
            Assert.Equal("Arsenal", m.HomeTeam);
            Assert.Equal("Liverpool", m.AwayTeam);
            Assert.Equal(2, m.HomeGoals);
            Assert.Equal(1, m.AwayGoals);
        }
    }
}
