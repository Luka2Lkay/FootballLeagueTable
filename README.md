Football League Table - Command Line Application

This repository contains a small command-line application that reads a CSV of match results and computes league standings using the historical English First Division rules (2 points for a win, 1 for a draw). Tie-breakers: points, goal average (GF/GA), goals for, then team name.

Usage:
- dotnet run -- <input.csv> <output.csv>
- dotnet run -- <input.csv>           (writes CSV to stdout)
- cat input.csv | dotnet run --       (reads from stdin, writes to stdout)
- Use '-' as filename to explicitly mean stdin or stdout.

If no arguments are provided the program will read from stdin and write to stdout; sample data is included in FootballLeagueTable/Data/ for convenience.

CSV input format (header optional):
Date,HomeTeam,AwayTeam,HomeGoals,AwayGoals

Included files:
- Data/first_division_1974_75_week10.csv - sample match results (small illustrative dataset)
- Output/standings_week10_1974_75.csv - standings produced from the sample data

Note: The sample match data is illustrative and not an authoritative reproduction of the real 1974/75 results. The program is generic and can be used with historical data provided in the CSV format described above.
