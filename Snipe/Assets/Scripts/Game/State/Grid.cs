namespace Snipe
{
	public class Grid
	{
        public int Width { get { return width; } }
        public int Height { get { return height; } }
		public GridType GridType { get { return gridType; } }

		private Cell[,] cells;
        private int width;
        private int height;
		private GridType gridType;

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
                    this.cells[x, y] = new Cell();
                }
            }
		}

		public Cell GetCellAt(int x, int y)
		{
			return cells[x, y];
		}
	}
}