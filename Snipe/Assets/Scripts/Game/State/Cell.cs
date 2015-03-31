using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
	public class Cell
	{
        public Vector2 Position { get { return position; } }
        public TileType TileType { get { return tileType; } set { tileType = value; } }
        public List<Entity> Entities { get { return entities; } }

        private Vector2 position;
        private TileType tileType = TileType.Empty;
        private Grid grid;
        private List<Entity> entities;

        public Cell(Vector2 position, TileType tileType, Grid grid)
        {
            this.position = position;
            this.tileType = tileType;
            this.grid = grid;
            this.entities = new List<Entity>();
        }

		public void AddEntity(Entity entity)
        {
            entity.Location = this;

            entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            if (entities.Contains(entity))
            {
                entities.Remove(entity);
            }
        }

        public Unit GetUnit()
        {
            foreach (Entity entity in Entities)
            {
                Unit unit = entity as Unit;

                if (unit != null)
                {
                    return unit;
                }
            }

            return null;
        }

        public List<Cell> GetAdjacentCells()
        {
            List<Cell> adjacentCells = new List<Cell>();

            if (grid.GridType == GridType.Hexagonal)
            {
            }
            else if (grid.GridType == GridType.Rectangular)
            {
                bool leftHasCell = position.x > 0;
                bool rightHasCell = position.x < grid.Width - 1;
                bool upHasCell = position.y > 0;
                bool downHasCell = position.y < grid.Height - 1;

                if (leftHasCell)
                {
                    adjacentCells.Add(grid.GetCellAt(
                        (int)position.x - 1, (int)position.y));

                    if (upHasCell)
                    {
                        adjacentCells.Add(grid.GetCellAt(
                            (int)position.x - 1, (int)position.y - 1));
                    }

                    if (downHasCell)
                    {
                        adjacentCells.Add(grid.GetCellAt(
                            (int)position.x - 1, (int)position.y + 1));
                    }
                }

                if (rightHasCell)
                {
                    adjacentCells.Add(grid.GetCellAt(
                        (int)position.x + 1, (int)position.y));

                    if (upHasCell)
                    {
                        adjacentCells.Add(grid.GetCellAt(
                            (int)position.x + 1, (int)position.y - 1));
                    }

                    if (downHasCell)
                    {
                        adjacentCells.Add(grid.GetCellAt(
                            (int)position.x + 1, (int)position.y + 1));
                    }
                }

                if (upHasCell)
                {
                    adjacentCells.Add(grid.GetCellAt(
                        (int)position.x, (int)position.y - 1));
                }

                if (downHasCell)
                {
                    adjacentCells.Add(grid.GetCellAt(
                        (int)position.x, (int)position.y + 1));
                }
            }

            return adjacentCells;
        }
	}
}