namespace FootballLeagueTable.Models
{
    public sealed class Team
    {
        public Team(string name)
        {

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Team name cannot be null, empty, or whitespace.", nameof(name));

            Name = name.Trim();
        }

        public string Name { get; }

        public override string ToString() => Name;
    }
}