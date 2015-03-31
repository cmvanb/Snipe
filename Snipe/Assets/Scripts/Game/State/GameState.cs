using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
	public class GameState
	{
        public Grid Grid { get { return grid; } }
        public List<Player> Players { get { return players; } }
        public int TurnIndex { get { return turnIndex; } }
        public Player CurrentPlayer
        {
            get
            {
                return players[turnIndex];
            }
        }

		private Grid grid;
        private List<Player> players;
        private int turnIndex;

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

            this.players = new List<Player>();
		}

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void StartTurn()
        {
            CurrentPlayer.ResetActionPoints();

            Debug.Log("It is " + CurrentPlayer.Name + "'s turn.");
        }

        public void AdvanceTurn()
        {
            ++turnIndex;

            if (turnIndex > players.Count - 1)
            {
                turnIndex = 0;
            }

            StartTurn();
        }
	}
}