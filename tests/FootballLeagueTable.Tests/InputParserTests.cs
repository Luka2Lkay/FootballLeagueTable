using System.Linq;
using FootballLeagueTable.Services;
using Xunit;

namespace FootballLeagueTable.Tests
{
    public class InputParserTests
    {
        [Fact]
        public void ParsesCsvAndPlainText()
        {
            var parser = new InputParser();
            var input = "Home,Away,2,1\nArsenal,Liverpool,0,3";
            var results = parser.ParseMatches(input).ToList();
            Assert.Equal(2, results.Count);
            Assert.Equal("Home", results[0].HomeTeam);
            Assert.Equal(2, results[0].HomeGoals);

            var text = "Arsenal 2-1 Liverpool\nDerby 0-0 Leeds";
            var results2 = parser.ParseMatches(text).ToList();
            Assert.Equal(2, results2.Count);
        }
    }
}
