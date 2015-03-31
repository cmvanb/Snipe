using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
    public class Unit : Entity
    {
        public override string Name 
        { 
            get 
            {
                return "Unit/" + faction.ToString() + "/" + unitType.ToString();
            }
        }

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
            // TODO: Actually move unit.
        }

        public bool CanMove(Cell destination)
        {
            // There's already a unit there.
            if (destination.GetUnit() != null)
            {
                return false;
            }

            // Some other type of entity is blocking the way.
            if (destination.Entities.Count > 0)
            {
                return false;
            }

            return true;
        }

        public List<Cell> GetLegalMoves()
        {
            List<Cell> adjacentCells = Location.GetAdjacentCells();

            for (int i = 0; i < adjacentCells.Count; ++i)
            {
                if (!CanMove(adjacentCells[i]))
                {
                    adjacentCells.RemoveAt(i);
                    --i;
                }
            }

            return adjacentCells;
        }
    }
}