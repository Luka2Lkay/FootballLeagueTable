using FootballLeagueTable.Cli;
using Xunit;
using System;

namespace FootballLeagueTable.Tests
{
    public class ArgsParserTests
    {
        [Fact]
        public void NoArgs_MapsToNullPaths()
        {
            var opts = ArgsParser.Parse(Array.Empty<string>());
            Assert.Null(opts.InputPath);
            Assert.Null(opts.OutputPath);
        }

        [Fact]
        public void SingleArg_MapsToInputPath()
        {
            var opts = ArgsParser.Parse(new[] { "in.csv" });
            Assert.Equal("in.csv", opts.InputPath);
            Assert.Null(opts.OutputPath);
        }

        [Fact]
        public void TwoArgs_MapsToInputAndOutput()
        {
            var opts = ArgsParser.Parse(new[] { "in.csv", "out.csv" });
            Assert.Equal("in.csv", opts.InputPath);
            Assert.Equal("out.csv", opts.OutputPath);
        }
    }
}
