# Football League Table

This repository contains a small, production-oriented command-line application that calculates a football (soccer) league table from match results. The implementation follows the historical rules for the English First Division in 1974/75 (2 points for a win, goal average tie-breaker).

## Contents
- FootballLeagueTable/        : Main application project
- FootballLeagueTable/Cli/    : CLI DTO and helpers
- FootballLeagueTable/Models/ : Domain models (MatchResult, Team, LeagueTableEntry)
- FootballLeagueTable/Services/: Parsing and table-calculation logic
- FootballLeagueTable/Data/   : Sample input CSVs and example output
- tests/                      : xUnit test project

## Prerequisites
- .NET 10 SDK (target framework: net10.0)
- A terminal (PowerShell, Bash, or cmd) or Visual Studio 2026+ for IDE runs

## Build

From the repository root:

  ```dotnet build```

## Running the CLI

The command-line application supports three ways to provide input and receive output:

Input and output files: Pass the input CSV filename followed by the output CSV filename. The program reads match results from the input file and writes the calculated league table to the output file. If the output file already exists, it will be overwritten.

Standard input and standard output (stdin/stdout). The program reads match results from standard input and prints the league table to standard output. This is useful when piping data from another command.


### Examples

1. Read from an input file and write to an output file

```dotnet run --project FootballLeagueTable.csproj -- Data/input_week10_1974_75.csv Data/output_week10_1974_75.csv```

The program reads the match results from Data/input_week10_1974_75.csv and writes the league table to Data/output_week10_1974_75.csv. The output file is created if it does not exist.

2. Read from standard input and write to standard output

```cat Data/input_week10_1974_75.csv | dotnet run --project FootballLeagueTable.csproj```

The program reads the CSV content piped into it and prints the resulting league table to the terminal.

3. Read from a file and print the results to the terminal

```dotnet run --project FootballLeagueTable.csproj -- Data/input_week10_1974_75.csv```

The program reads the input file and prints the calculated league table to standard output.

### Input format

- The program accepts CSV input (header optional) or simple plain-text lines. Required CSV columns (header optional):

  HomeTeam,AwayTeam,HomeGoals,AwayGoals

  Example CSV row: Arsenal,Liverpool,2,1

### Output format

- The program writes a CSV table with the following columns (GoalAverage formatted with two decimals):

  Position,Team,Played,Won,Drawn,Lost,GoalsFor,GoalsAgainst,GoalAverage,Points

Rules implemented

- Points: 2 points for a win, 1 point for a draw, 0 for a loss (English First Division, 1974/75)
- Tie-breakers (implemented in this order):
  1) Points (desc)
  2) Goal average (GF / GA) (desc). If GA == 0, goal average is treated as GF.
  3) Goals for (GF) (desc)
  4) Team name (asc)

## Tests

This repository uses xUnit for unit tests. The test project is tests/FootballLeagueTable.Tests and is included in the solution.

Run all tests from the repository root:

  ```dotnet test FootballLeagueTable.slnx```

Or run tests for the test project only:

  ```dotnet test tests/FootballLeagueTable.Tests/FootballLeagueTable.Tests.csproj```

The test suite covers CSV and plain-text parsing, aggregation logic, goal-average tie-break behavior, and CLI argument parsing.

## Developer notes

- The CLI DTO is defined in FootballLeagueTable/Cli/CliOptions.cs. Arguments are parsed by Cli/ArgsParser.cs and mapped to InputPath/OutputPath (null indicates stdin/stdout; '-' is accepted as explicit stdin/stdout).
- Parsing is implemented in Services/InputParser.cs and supports both CSV rows and free-form "HomeTeam 2-1 AwayTeam" lines.
- Table calculation is implemented in Services/TableCalculator.cs using LeagueTableEntry as the aggregate record type.
