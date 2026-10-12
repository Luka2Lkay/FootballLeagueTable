COPILOT INSTRUCTIONS
====================

The Problem
-----------
Create a production-ready command-line application that calculates a football league standing table from match results, then use it to calculate the English First Division table at week 10 of the 1974/75 season.

Rules (historical)
------------------
- Points: 2 points for a win, 1 for a draw, 0 for a loss (English First Division, 1974/75).
- Tie-breakers used in 1974/75: goal average (goals for divided by goals against) was used before goal difference became standard. Implement tie-breakers in this order:
  1) Points (desc)
  2) Goal average (GF / GA) (desc). When GA == 0, treat goal average as GF.
  3) Goals for (GF) (desc)
  4) Team name (asc)

Input / Output (required)
-------------------------
- Input MUST be a CSV file (or read from stdin) with one match per row. Required columns (header optional but recommended):
  HomeTeam,AwayTeam,HomeGoals,AwayGoals
  Example row: Arsenal,Liverpool,2,1
- Output MUST be a CSV file (or write to stdout) containing the league table in conventional format. Required columns:
  Position,Team,Played,Won,Drawn,Lost,GoalsFor,GoalsAgainst,GoalAverage,Points
  GoalAverage should be written as a numeric value with at least two decimal places.
- The CLI SHOULD support both reading/writing via filenames and using stdin/stdout when filenames are omitted. The repository MUST include at least one input CSV and the corresponding output CSV for the requested season/week.

Repository layout expectations
----------------------------
- FootballLeagueTable/Program.cs           - application entry (CLI)
- FootballLeagueTable/Models/              - domain models
- FootballLeagueTable/Services/            - parsing and calculation logic
- FootballLeagueTable/Cli/                 - CLI option types
- FootballLeagueTable/Data/                - input CSV(s) and output CSV(s) for submission
- tests/                                   - automated tests (unit/integration)

Development rules
-----------------
- Make minimal, focused changes that implement the feature and tests. Do not refactor unrelated code.
- Use apply_patch for edits.
- Read target files before editing; do not assume file contents or paths.
- Preserve existing coding style and naming conventions.
- Target framework: .NET 10. Use dotnet build to validate compilation.

Testing & submission
--------------------
- SPAN requires automated tests. Include unit tests that validate:
  - CSV parsing of input rows
  - Correct aggregation of statistics (P W D L F A)
  - Tie-breaker behavior (especially goal average ordering)
  - CLI end-to-end behavior reading input and writing output
- Include test data files in FootballLeagueTable/Data/ and include the expected output CSV for the 1974/75 week 10 case.
- Do NOT commit installed package folders (e.g., packages or virtualenvs). Commit only project files and test code.

Documentation
-------------
- If any non-trivial setup is required to run the solution or tests, document it concisely in README.md.
- Document how to run the CLI with examples using filenames and stdin/stdout.

If uncertain
----------
- Ask the user a concise question before making changes that impact multiple areas (for example: "Should I implement goal average exactly as GF/GA with two decimals, or use goal difference instead?").