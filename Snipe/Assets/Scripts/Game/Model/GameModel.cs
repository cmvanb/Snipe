using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
	public class GameModel
    {
        public delegate void GameResetHandler();
        public delegate void EnteredStateHandler(GameState enteredState);
        public delegate void ExitedStateHandler(GameState exitedState);

        public event GameResetHandler GameResetEvent;
        public event EnteredStateHandler EnteredStateEvent;
        public event ExitedStateHandler ExitedStateEvent;

        public Player CurrentPlayer
        {
            get
            {
                return players[turnIndex];
            }
        }
        public Grid Grid { get { return grid; } }
        public List<Player> Players { get { return players; } }
        public int TurnIndex { get { return turnIndex; } }
        public GameState CurrentState { get { return currentState; } }
        public bool GameOver { get { return gameOver; } }

		private Grid grid;
        private List<Player> players;
        private int turnIndex;
        private GameState currentState = GameState.None;
        private GameState targetState = GameState.None;
        private bool gameOver;
        private bool transitioningState = false;

		public GameModel()
		{
            SetupGrid();

            this.players = new List<Player>();
		}

        public void ChangeGameState(GameState newTargetState)
        {
            Debug.Log("Change " + currentState.ToString() + " to " + newTargetState.ToString());

            if (transitioningState)
            {
                Debug.Log("abort change, still transitioning");

                return;
            }

            targetState = newTargetState;

            transitioningState = true;

            ExitedStateEvent += OnExitedState;

            ExitGameState(currentState);
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

        public void CheckGameOver()
        {
            if (gameOver)
            {
                return;
            }

            int loserCount = 0;

            foreach (Player player in players)
            {
                if (HasPlayerLost(player))
                {
                    ++loserCount;
                }
            }

            // There can only be one winner!
            if (players.Count - loserCount == 1)
            {
                gameOver = true;

                Debug.Log("GAME OVER!");
            }
            else
            {
                gameOver = false;
            }
        }

        public void Reset()
        {
            turnIndex = 0;
            gameOver = false;

            CleanUpGrid();
            SetupGrid();

            if (GameResetEvent != null)
            {
                GameResetEvent();
            }
        }

        private void CleanUpGrid()
        {
            grid.CleanUp();

            grid = null;
        }

        private void SetupGrid()
        {
            grid = new Grid(8, 8, GridType.Rectangular);

            for (int x = 0; x < grid.Width; ++x)
            {
                for (int y = 0; y < grid.Height; ++y)
                {
                    /*
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

                    grid.Cells[Random.Range(0, grid.Width), y].TileType = TileType.Dirt;
                    */
                    grid.Cells[x, y].TileType = TileType.StoneLight;
                }
            }
        }

        private void EnterGameState(GameState newState)
        {
            transitioningState = false;

            Debug.Log("entering " + newState.ToString());
            
            if (EnteredStateEvent != null)
            {
                EnteredStateEvent(newState);
            }
        }

        private void ExitGameState(GameState oldState)
        {
            Debug.Log("exiting " + oldState.ToString());

            if (ExitedStateEvent != null)
            {
                ExitedStateEvent(oldState);
            }
        }

        private void OnExitedState(GameState exitedState)
        {
            ExitedStateEvent -= OnExitedState;

            currentState = targetState;

            Debug.Log("current state is " + currentState.ToString());

            EnterGameState(targetState);
        }

        private bool HasPlayerLost(Player player)
        {
            List<Unit> units = grid.GetUnits(player.Faction);

            if (units.Count == 0)
            {
                return true;
            }

            foreach (Unit unit in units)
            {
                if (!unit.IsWounded)
                {
                    return false;
                }
            }

            return true;
        }
	}
}