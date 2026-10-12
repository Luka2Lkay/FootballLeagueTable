# Football League Table

A small .NET console application that calculates football (soccer) league standings from a list of match results. Useful as an exercise or starting point for building a league-table utility.

Features
- Parse simple plain-text match results (e.g. "HomeTeam 2-1 AwayTeam").
- Compute league table entries: matches played, wins, draws, losses, goals for/against, goal difference, points.
- Deterministic ranking by points, then goal difference, then goals scored.

Prerequisites
- .NET 10 SDK (the project targets net10.0)
- A terminal (PowerShell, cmd, or any shell that can run dotnet)

Build and run
1. Restore and build:

   dotnet build

2. Run the application from the project folder:

   dotnet run --project FootballLeagueTable.csproj

Usage
- Place an input file in the Data/ folder (or point your Program.cs to a different path). Each line must represent one match using this format:

  HomeTeam 2-1 AwayTeam

- Team names may contain spaces. The score must be two integers separated by a hyphen.

Example input (Data/matches.txt):

  Liverpool 3-1 Manchester City
  Arsenal 2-2 Chelsea
  Manchester City 0-2 Arsenal

Repository structure
- Models: domain models (Team, MatchResult, LeagueTableEntry)
- Services: parser and table calculator implementations
- Cli: DTO for command-line options
- Data: seed data files and example input

Running tests
-------------

The repository uses xUnit for unit tests. The test project is tests/FootballLeagueTable.Tests and is included in the solution.

From the repository root you can run the tests with dotnet:

- Run all tests in the solution:

  dotnet test FootballLeagueTable.slnx

- Or run tests for the test project only:

  dotnet test tests/FootballLeagueTable.Tests/FootballLeagueTable.Tests.csproj

The commands will build the code and run the xUnit tests; a successful run reports all tests passed. In Visual Studio open Test Explorer to run or debug tests interactively.
