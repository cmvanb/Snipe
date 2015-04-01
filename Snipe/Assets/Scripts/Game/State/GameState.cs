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
                    bool xIsEven = x % 2 == 0;
                    bool yIsEven = y % 2 == 0;

                    if (yIsEven)
                    {
                        grid.Cells[x, y].TileType = (TileType)(xIsEven ? 1 : 2);
                    }
                    else
                    {
                        grid.Cells[x, y].TileType = (TileType)(xIsEven ? 2 : 1);
                    }
                }
            }

            grid.Cells[4, 6].AddEntity(new Unit(Faction.A, UnitType.Soldier, NameStack.GetName(), grid));
            grid.Cells[3, 6].AddEntity(new Unit(Faction.A, UnitType.Sniper, NameStack.GetName(), grid));
            grid.Cells[5, 6].AddEntity(new Unit(Faction.A, UnitType.Medic, NameStack.GetName(), grid));
            grid.Cells[6, 6].AddEntity(new Unit(Faction.A, UnitType.Soldier, NameStack.GetName(), grid));

            grid.Cells[4, 1].AddEntity(new Unit(Faction.B, UnitType.Soldier, NameStack.GetName(), grid));
            grid.Cells[3, 1].AddEntity(new Unit(Faction.B, UnitType.Sniper, NameStack.GetName(), grid));
            grid.Cells[5, 1].AddEntity(new Unit(Faction.B, UnitType.Medic, NameStack.GetName(), grid));

            this.players = new List<Player>();
		}

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void StartTurn()
        {
            Debug.Log("It is " + CurrentPlayer.Name + "'s turn.");

            CurrentPlayer.ResetActionPoints();
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