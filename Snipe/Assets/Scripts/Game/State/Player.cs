namespace Snipe
{
    public class Player
    {
        public string Name { get { return name; } }
        public Faction Faction { get { return faction; } }

        private string name;
        private Faction faction;

        public Player(string name, Faction faction)
        {
            this.name = name;
            this.faction = faction;
        }
    }
}