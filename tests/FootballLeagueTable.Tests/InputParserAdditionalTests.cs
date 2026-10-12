using System.Linq;
using FootballLeagueTable.Services;
using Xunit;

namespace FootballLeagueTable.Tests
{
    public class InputParserAdditionalTests
    {
        [Fact]
        public void ParsesFreeformLines()
        {
            var input = "Arsenal 2-1 Liverpool";
            var p = new InputParser();
            var m = p.ParseMatches(input).Single();
            Assert.Equal("Arsenal", m.HomeTeam);
            Assert.Equal("Liverpool", m.AwayTeam);
            Assert.Equal(2, m.HomeGoals);
            Assert.Equal(1, m.AwayGoals);
        }

        [Fact]
        public void IgnoresMalformedLines()
        {
            var input = "Bad Line\nArsenal 2-1 Liverpool\nHomeTeam,AwayTeam,HomeGoals,AwayGoals\nFoo,Bar,notint,1";
            var p = new InputParser();
            var matches = p.ParseMatches(input).ToList();
            Assert.Single(matches);
            Assert.Equal("Arsenal", matches[0].HomeTeam);
        }

        [Fact]
        public void HandlesCsvWithExtraColumnsAndHeader()
        {
            var input = "HomeTeam,AwayTeam,HomeGoals,AwayGoals,Extra\nArsenal,Liverpool,3,2,ignore\n";
            var p = new InputParser();
            var matches = p.ParseMatches(input).ToList();
            Assert.Single(matches);
            Assert.Equal(3, matches[0].HomeGoals);
        }

        [Fact]
        public void EmptyOrWhitespaceInputReturnsNoMatches()
        {
            var p = new InputParser();
            Assert.Empty(p.ParseMatches(null));
            Assert.Empty(p.ParseMatches(""));
            Assert.Empty(p.ParseMatches("   \n  "));
        }

        [Fact]
        public void ParsesMultipleLines()
        {
            var input = "Arsenal 2-1 Liverpool\r\nChelsea,ManCity,1,1\nTottenham 0-0 Everton";
            var p = new InputParser();
            var list = p.ParseMatches(input).ToList();
            Assert.Equal(3, list.Count);
            Assert.Equal("Chelsea", list[1].HomeTeam);
        }
    }
}