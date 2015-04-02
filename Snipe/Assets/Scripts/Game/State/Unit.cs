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
                return "Unit/" + faction.ToString() + "/" + gameName + "/" + unitType.ToString();
            }
        }

        public Faction Faction { get { return faction; } }
        public UnitType UnitType { get { return unitType; } }
        public string GameName { get { return gameName; } }
        public bool IsWounded { get { return isWounded; } set { isWounded = value; } }
        public bool IsRevealed { get { return isRevealed; } set { isRevealed = value; } }

        private Faction faction;
        private UnitType unitType;
        private string gameName;
        private bool isWounded;
        private bool isRevealed;

        public Unit(Faction faction, UnitType unitType, string gameName, Grid grid) : base(grid)
        {
            this.faction = faction;
            this.unitType = unitType;
            this.gameName = gameName;
        }

        public void Move(Cell destination)
        {
            Location.RemoveEntity(this as Entity);

            destination.AddEntity(this as Entity);

            Location = destination;
        }

        public void Attack(Cell target)
        {
            Unit targetUnit = target.GetUnit();

            if (unitType == UnitType.Sniper)
            {
                isRevealed = true;
            }

            if (grid.AreCellsAdjacent(Location, target))
            {
                if (targetUnit.IsWounded)
                {
                    targetUnit.Kill();
                }
                else
                {
                    targetUnit.SetIsWounded(true);
                }
            }
            else if (unitType == UnitType.Sniper)
            {
                targetUnit.SetIsWounded(true);
            }
            else
            {
                if (Dice.RollForHit(Location, target))
                {
                    targetUnit.SetIsWounded(true);
                }
            }
        }

        public void Heal(Cell target)
        {
            Unit targetUnit = target.GetUnit();

            if (unitType == UnitType.Medic)
            {
                isRevealed = true;
            }

            if (targetUnit.IsWounded)
            {
                targetUnit.SetIsWounded(false);
            }
        }

        public void SetIsWounded(bool isWounded)
        {
            if (isWounded)
            {
                Debug.Log("I'm wounded at " + Location.Position);
            }
            else
            {
                Debug.Log("My wound is healed at " + Location.Position);
            }

            this.isWounded = isWounded;
        }

        public void Kill()
        {
            Debug.Log("I'm dead at " + Location.Position);

            Location.RemoveEntity(this as Entity);

            Location = null;

            isAlive = false;
        }

        public List<Cell> GetLegalMoves()
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

        public List<Cell> GetLegalAttacks()
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

        public List<Cell> GetLegalHeals()
        {
            // Non-medic units return empty list.
            if (unitType != UnitType.Medic)
            {
                return new List<Cell>();
            }

            List<Cell> adjacentCells = grid.GetCellsAdjacentTo(Location);

            for (int i = 0; i < adjacentCells.Count; ++i)
            {
                if (!CanHeal(adjacentCells[i]))
                {
                    adjacentCells.RemoveAt(i);
                    --i;
                }
            }

            return adjacentCells;
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

            // Can't attack wounded non-adjacent units.
            if (targetUnit.IsWounded
                && !grid.AreCellsAdjacent(Location, target))
            {
                return false;
            }

            return true;
        }

        public bool CanHeal(Cell target)
        {
            // Can't heal if we're not a medic.
            if (unitType != UnitType.Medic)
            {
                return false;
            }

            Unit targetUnit = target.GetUnit();

            // There's no unit here to heal.
            if (targetUnit == null)
            {
                return false;
            }

            // Can't heal different faction.
            if (targetUnit.Faction != faction)
            {
                return false;
            }

            // Can't heal unit if it's not wounded.
            if (!targetUnit.IsWounded)
            {
                return false;
            }

            return true;
        }
    }
}