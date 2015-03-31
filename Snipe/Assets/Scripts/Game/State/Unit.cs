namespace Snipe
{
    public class Unit : Entity
    {
        public Faction Faction { get { return faction; } set { faction = value; } }
        public UnitType UnitType { get { return unitType; } set { unitType = value; } }

        private Faction faction;
        private UnitType unitType;

        public Unit(Faction faction, UnitType unitType) : base()
        {
            this.faction = faction;
            this.unitType = unitType;
        }

        public void Move(Cell destination)
        {
        }
    }
}