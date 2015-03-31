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
                    this.cells[x, y] = new Cell(new Vector2(x, y), TileType.Empty);//(TileType)UnityEngine.Random.Range(0, 2));
                }
            }
		}

		public Cell GetCellAt(int x, int y)
		{
			return cells[x, y];
		}
	}
}