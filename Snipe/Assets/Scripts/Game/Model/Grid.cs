using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
	public class Grid
	{
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public GridType GridType { get { return gridType; } }
        public Cell[,] Cells { get { return cells; } }

        private int width;
        private int height;
        private GridType gridType;
        private Cell[,] cells;

		public Grid(int width, int height, GridType gridType)
		{
            this.width = width;

            this.height = height;
            
            this.gridType = gridType;

			this.cells = new Cell[width, height];

            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    this.cells[x, y] = new Cell(new Vector2(x, y), TileType.Empty);
                }
            }
		}

        public void CleanUp()
        {
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    this.cells[x, y].CleanUp();
                    this.cells[x, y] = null;
                }
            }

            this.cells = null;
        }

        public List<Unit> GetUnits(Faction faction)
        {
            List<Unit> units = new List<Unit>();

            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    Unit unit = GetCellAt(x, y).GetUnit();

                    if (unit != null
                        && unit.Faction == faction)
                    {
                        units.Add(unit);
                    }
                }
            }

            return units;
        }

        public List<Unit> GetAllUnits()
        {
            List<Unit> units = new List<Unit>();

            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    Unit unit = GetCellAt(x, y).GetUnit();

                    if (unit != null)
                    {
                        units.Add(unit);
                    }
                }
            }

            return units;
        }

        public bool AreCellsAdjacent(Cell cell1, Cell cell2)
        {
            Vector2 difference = cell1.Position - cell2.Position;

            if ((int)Mathf.Abs(difference.x) <= 1
                && (int)Mathf.Abs(difference.y) <= 1)
            {
                return true;
            }

            return false;
        }

		public Cell GetCellAt(int x, int y)
		{
			return cells[x, y];
		}

        public List<Cell> GetCellsAdjacentTo(Cell cell)
        {
            List<Cell> adjacentCells = new List<Cell>();

            if (gridType == GridType.Hexagonal)
            {
                // TODO: Implement hex grid.
            }
            else if (gridType == GridType.Rectangular)
            {
                bool leftHasCell = cell.Position.x > 0;
                bool rightHasCell = cell.Position.x < width - 1;
                bool upHasCell = cell.Position.y > 0;
                bool downHasCell = cell.Position.y < height - 1;

                if (leftHasCell)
                {
                    adjacentCells.Add(GetCellAt(
                        (int)cell.Position.x - 1, (int)cell.Position.y));

                    if (upHasCell)
                    {
                        adjacentCells.Add(GetCellAt(
                            (int)cell.Position.x - 1, (int)cell.Position.y - 1));
                    }

                    if (downHasCell)
                    {
                        adjacentCells.Add(GetCellAt(
                            (int)cell.Position.x - 1, (int)cell.Position.y + 1));
                    }
                }

                if (rightHasCell)
                {
                    adjacentCells.Add(GetCellAt(
                        (int)cell.Position.x + 1, (int)cell.Position.y));

                    if (upHasCell)
                    {
                        adjacentCells.Add(GetCellAt(
                            (int)cell.Position.x + 1, (int)cell.Position.y - 1));
                    }

                    if (downHasCell)
                    {
                        adjacentCells.Add(GetCellAt(
                            (int)cell.Position.x + 1, (int)cell.Position.y + 1));
                    }
                }

                if (upHasCell)
                {
                    adjacentCells.Add(GetCellAt(
                        (int)cell.Position.x, (int)cell.Position.y - 1));
                }

                if (downHasCell)
                {
                    adjacentCells.Add(GetCellAt(
                        (int)cell.Position.x, (int)cell.Position.y + 1));
                }
            }

            return adjacentCells;
        }

        public List<Cell> GetCellsVisibleFrom(Cell cell)
        {
            List<Cell> visibleCells = new List<Cell>();
            
            if (gridType == GridType.Hexagonal)
            {
                // TODO: Implement hex grid.
            }
            else if (gridType == GridType.Rectangular)
            {
                // left
                visibleCells.AddRange(GetCellsInLineOfSight(cell, 
                    new Vector2(-1, 0)));

                // left, up
                visibleCells.AddRange(GetCellsInLineOfSight(cell,
                    new Vector2(-1, -1)));

                // up
                visibleCells.AddRange(GetCellsInLineOfSight(cell,
                    new Vector2(0, -1)));

                // right, up
                visibleCells.AddRange(GetCellsInLineOfSight(cell,
                    new Vector2(1, -1)));

                // right
                visibleCells.AddRange(GetCellsInLineOfSight(cell,
                    new Vector2(1, 0)));

                // right, down
                visibleCells.AddRange(GetCellsInLineOfSight(cell,
                    new Vector2(1, 1)));

                // down
                visibleCells.AddRange(GetCellsInLineOfSight(cell,
                    new Vector2(0, 1)));

                // left, down
                visibleCells.AddRange(GetCellsInLineOfSight(cell,
                    new Vector2(-1, 1)));
            }

            return visibleCells;
        }

        private List<Cell> GetCellsInLineOfSight(Cell cell, Vector2 direction)
        {
            List<Cell> visibleCells = new List<Cell>();

            Cell nextCell = GetCellInDirection(cell, direction);

            while (nextCell != null)
            {
                visibleCells.Add(nextCell);

                if (nextCell.IsEmpty())
                {
                    nextCell = GetCellInDirection(nextCell, direction);
                }
                else
                {
                    nextCell = null;
                }
            }

            return visibleCells;
        }

        private Cell GetCellInDirection(Cell cell, Vector2 direction)
        {
            Vector2 newPosition = cell.Position + direction;

            if (newPosition.x >= 0
                && newPosition.x <= width - 1
                && newPosition.y >= 0
                && newPosition.y <= height - 1)
            {
                return cells[(int)newPosition.x, (int)newPosition.y];
            }

            return null;
        }
	}
}