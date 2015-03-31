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
                    grid.Cells[x, y].TileType = (TileType)UnityEngine.Random.Range(1, 3);
                }
            }

            grid.Cells[4, 1].AddEntity(new Unit(Faction.B, UnitType.Soldier));
            grid.Cells[4, 6].AddEntity(new Unit(Faction.A, UnitType.Soldier));
            grid.Cells[3, 1].AddEntity(new Unit(Faction.B, UnitType.Sniper));
            grid.Cells[3, 6].AddEntity(new Unit(Faction.A, UnitType.Sniper));
            grid.Cells[5, 1].AddEntity(new Unit(Faction.B, UnitType.Medic));
            grid.Cells[5, 6].AddEntity(new Unit(Faction.A, UnitType.Medic));
		}
	}
}