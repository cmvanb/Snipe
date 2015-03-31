using UnityEngine;

namespace Snipe
{
	public class GameState
	{
        public Grid Grid { get { return grid; } }

		private Grid grid;

		public GameState(LevelData levelData)
		{
			// TODO: Pipe level data into grid constructor.

            this.grid = new Grid(8, 8, GridType.Rectangular);

            for (int x = 0; x < grid.Width; ++x)
            {
                for (int y = 0; y < grid.Height; ++y)
                {
                    grid.Cells[x, y].TileType = (TileType)UnityEngine.Random.Range(0, 2);
                }
            }

            grid.Cells[4, 6].AddEntity(new Unit(Faction.A, UnitType.Soldier));
		}
	}
}