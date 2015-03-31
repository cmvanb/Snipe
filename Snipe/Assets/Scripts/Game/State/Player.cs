using UnityEngine;

namespace Snipe
{
    public class Player
    {
        public string Name { get { return name; } }
        public Faction Faction { get { return faction; } }
        public int ActionPoints { get { return actionPoints; } }

        private string name;
        private Faction faction;
        private int actionPoints;

        public Player(string name, Faction faction)
        {
            this.name = name;
            this.faction = faction;
        }

        public void UseActionPoint()
        {
            --actionPoints;

            if (actionPoints <= 0)
            {
                actionPoints = 0;
            }

            Debug.Log(name + " has " + actionPoints + " action points remaining.");
        }

        public void ResetActionPoints()
        {
            actionPoints = Constants.ActionPointsPerTurn;

            Debug.Log(name + " has " + actionPoints + " action points remaining.");
        }
    }
}