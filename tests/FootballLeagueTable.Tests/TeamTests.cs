using System;
using FootballLeagueTable.Models;
using Xunit;

namespace FootballLeagueTable.Tests
{
    public class TeamTests
    {
        [Fact]
        public void Constructor_ThrowsOnInvalidNames()
        {
            Assert.Throws<ArgumentException>(() => new Team(null!));
            Assert.Throws<ArgumentException>(() => new Team(string.Empty));
            Assert.Throws<ArgumentException>(() => new Team("   "));
        }

        [Fact]
        public void Name_IsAssignedAndToStringReturnsName()
        {
            var t = new Team("Manchester United");
            Assert.Equal("Manchester United", t.Name);
            Assert.Equal("Manchester United", t.ToString());
        }

        [Fact]
        public void Name_TrimsSurroundingWhitespace()
        {
            var t = new Team("  Liverpool  ");

            Assert.Equal("Liverpool", t.Name);
            Assert.Equal("Liverpool", t.ToString());
        }
    }
}
