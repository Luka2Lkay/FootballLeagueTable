using FootballLeagueTable.Models;
using FootballLeagueTable.Services;

using System.Linq;

internal static class TestRunner
{
    private static int Main(string[] args)
    {
        int failures = 0;

        void AssertEqual<T>(T expected, T actual, string message)
        {
            if (!EqualityComparer<T>.Default.Equals(expected, actual))
            {
                Console.Error.WriteLine($"FAIL: {message} - expected: {expected}, actual: {actual}");
                failures++;
            }
        }

        // Parser test
        var temp = Path.GetTempFileName();
        try
        {
            File.WriteAllLines(temp, new[] { "Home,Away,2,1", "Arsenal,Liverpool,0,3" });
            var parser = new InputParser();
            var input = File.ReadAllText(temp);
            var results = parser.ParseMatches(input).ToList();
            AssertEqual(2, results.Count, "Parser should read two rows");
            AssertEqual("Home", results[0].HomeTeam, "First row home team");
            AssertEqual(2, results[0].HomeGoals, "First row home goals");
        }
        finally { File.Delete(temp); }

        // Calculator test
        {
            var matches = new[]
            {
                new MatchResult("A", "B", 2, 1),
                new MatchResult("B", "C", 1, 1),
                new MatchResult("C", "A", 0, 3),
            };

            var calc = new TableCalculator();
            var standings = calc.CalculateTable(matches).ToList();
            var a = standings.First(s => s.Team == "A");
            AssertEqual(2, a.Played, "Team A played");
            AssertEqual(2, a.Won, "Team A wins");
            AssertEqual(0, a.Drawn, "Team A draws");
            AssertEqual(0, a.Lost, "Team A losses");
            AssertEqual(5, a.GoalsFor, "Team A GF");
            AssertEqual(1, a.GoalsAgainst, "Team A GA");
        }

        // Tie-breaker test
        {
            var matches = new[]
            {
                new MatchResult("A", "B", 1, 0),
                new MatchResult("C", "D", 2, 1),
            };
            var calc = new TableCalculator();
            var standings = calc.CalculateTable(matches).ToList();
            // C has GAve 2.0, A has GAve = GF when GA==0 -> 1
            AssertEqual("C", standings[0].Team, "Goal average tie-breaker places C before A");
        }

        if (failures > 0)
        {
            Console.Error.WriteLine($"{failures} test(s) failed");
            return 1;
        }

        Console.WriteLine("All tests passed");
        return 0;
    }
}
