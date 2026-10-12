namespace FootballLeagueTable.Cli
{
    public sealed class CliOptions
    {
        // InputPath: path to input CSV file, or null to indicate stdin when used by Program
        public string? InputPath { get; set; }

        // OutputPath: path to output CSV file, or null to indicate stdout when used by Program
        public string? OutputPath { get; set; }
    }
}
