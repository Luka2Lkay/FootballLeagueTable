namespace FootballLeagueTable.Cli;

public static class ArgsParser
{
    /// <summary>
    /// Map raw command-line args to CliOptions.
    /// Behavior:
    /// - no args: InputPath=null, OutputPath=null (use stdin/stdout)
    /// - one arg: InputPath=args[0], OutputPath=null
    /// - two or more args: InputPath=args[0], OutputPath=args[1]
    /// </summary>
    public static CliOptions Parse(string[] args)
    {
        var opts = new CliOptions();
        if (args == null || args.Length == 0)
            return opts;

        if (args.Length == 1)
        {
            opts.InputPath = args[0];
            return opts;
        }

        opts.InputPath = args[0];
        opts.OutputPath = args[1];
        return opts;
    }
}
