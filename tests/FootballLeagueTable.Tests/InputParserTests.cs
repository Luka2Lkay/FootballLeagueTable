using System.Linq;
using FootballLeagueTable.Services;
using Xunit;

namespace FootballLeagueTable.Tests
{
    public class InputParserTests
    {
        [Fact]
        public void ParsesCsvRows()
        {
            var csv = "HomeTeam,AwayTeam,HomeGoals,AwayGoals\nArsenal,Liverpool,2,1";
            var p = new InputParser();
            var matches = p.ParseMatches(csv).ToList();
            Assert.Single(matches);
            Assert.Equal("Arsenal", matches[0].HomeTeam);
            Assert.Equal("Liverpool", matches[0].AwayTeam);
            Assert.Equal(2, matches[0].HomeGoals);
            Assert.Equal(1, matches[0].AwayGoals);
        }

        [Fact]
        public void ParsesPlainTextMatch()
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
        public void SkipsCsvHeader()
        {
            var csv = "HomeTeam,AwayTeam,HomeGoals,AwayGoals\nArsenal,Liverpool,2,1";
            var p = new InputParser();
            var matches = p.ParseMatches(csv).ToList();
            // header line should be ignored and the data row parsed
            Assert.Single(matches);
            Assert.Equal("Arsenal", matches[0].HomeTeam);
        }

        [Fact]
        public void SkipsInvalidScores()
        {
            var csv = "Arsenal,Liverpool,2,1\nFoo,Bar,notint,1";
            var p = new InputParser();
            var matches = p.ParseMatches(csv).ToList();
            // only the valid numeric row should be parsed
            Assert.Single(matches);
            Assert.Equal("Arsenal", matches[0].HomeTeam);
        }

        [Fact]
        public void EmptyInputReturnsNoMatch()
        {
            var p = new InputParser();
            Assert.Empty(p.ParseMatches(null));
            Assert.Empty(p.ParseMatches(string.Empty));
            Assert.Empty(p.ParseMatches("   \r\n  "));
        }


        [Fact]
        public void ParsesMatchWithMultiWordTeamNames()
        {
            var input = "Manchester United 2-1 West Ham United";

            var match = new InputParser()
                .ParseMatches(input)
                .Single();

            Assert.Equal("Manchester United", match.HomeTeam);
            Assert.Equal("West Ham United", match.AwayTeam);
            Assert.Equal(2, match.HomeGoals);
            Assert.Equal(1, match.AwayGoals);
        }

        [Fact]
        public void ParsesDraw()
        {
            var input = "Arsenal 1-1 Liverpool";

            var match = new InputParser()
                .ParseMatches(input)
                .Single();

            Assert.Equal(1, match.HomeGoals);
            Assert.Equal(1, match.AwayGoals);
        }

        [Fact]
        public void SkipsCsvRowWithEmptyTeamName()
        {
            var input = ",Liverpool,2,1";

            var matches = new InputParser()
                .ParseMatches(input)
                .ToList();

            Assert.Empty(matches);
        }

    }
}