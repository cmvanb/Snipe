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

			this.grid = new Grid(10, 10, GridType.Rectangular);
		}
	}
}