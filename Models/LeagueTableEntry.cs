using System.Globalization;

namespace FootballLeagueTable.Models
{
    public sealed class LeagueTableEntry
    {
        public string Team { get; set; } = string.Empty;
        public int Played { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }

        public int GoalDifference => GoalsFor - GoalsAgainst;

        // Historical points: 2 per win, 1 per draw
        public int Points => Won * 2 + Drawn;

        // Goal average used in 1974/75 tie-breakers: GF / GA. When GA == 0 treat the ratio as GF (per instructions).
        public double GoalAverage => GoalsAgainst == 0 ? (double)GoalsFor : (double)GoalsFor / GoalsAgainst;

        public override string ToString()
        {
            string goalAverageDisplay = GoalAverage.ToString("0.00", CultureInfo.InvariantCulture);

            return $"{Team,-30} P:{Played,2} W:{Won,2} D:{Drawn,2} L:{Lost,2} GF:{GoalsFor,2} GA:{GoalsAgainst,2} GAve:{goalAverageDisplay,5} Pts:{Points,3}";
        }
    }
}