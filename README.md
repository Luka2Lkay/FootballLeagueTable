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

This repository includes a small self-contained console-style test runner located at tests/FootballLeagueTable.Tests. It runs a few unit checks and returns a non-zero exit code on failure.

From the repository root you can run the tests with dotnet:

  dotnet build
  dotnet run --project FootballLeagueTable.Tests

A zero exit code and the message "All tests passed" indicate success. In Visual Studio you can set tests/FootballLeagueTable.Tests as the startup project and run it; console output shows the same results.
