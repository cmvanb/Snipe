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
            Location.RemoveEntity(this as Entity);
            destination.AddEntity(this as Entity);

            Location = destination;
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

        public List<Cell> GetLegalMoves(Grid grid)
        {
            List<Cell> adjacentCells = grid.GetCellsAdjacentTo(Location);

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

        public void Attack(Cell target)
        {
            throw new System.NotImplementedException();
        }

        public bool CanAttack(Cell target)
        {
            Unit targetUnit = target.GetUnit();

            // There's no unit here to attack.
            if (targetUnit == null)
            {
                return false;
            }

            // Can't attack same faction.
            if (targetUnit.Faction == faction)
            {
                return false;
            }

            return true;
        }

        public List<Cell> GetLegalAttacks(Grid grid)
        {
            List<Cell> visibleCells = grid.GetCellsVisibleFrom(Location);

            for (int i = 0; i < visibleCells.Count; ++i)
            {
                if (!CanAttack(visibleCells[i]))
                {
                    visibleCells.RemoveAt(i);
                    --i;
                }
            }

            return visibleCells;
        }
    }
}