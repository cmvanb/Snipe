using UnityEngine;

namespace Snipe
{
	public class GameController
	{
		private GameModel gameModel;

		public GameController(GameModel gameModel)
		{
			this.gameModel = gameModel;

            gameModel.EnteredStateEvent += OnEnteredState;
            gameModel.ExitedStateEvent += OnExitedState;
		}
		
		public void StartGame()
        {
            gameModel.ChangeGameState(GameState.PreGamePhase);
		}

		public void Update()
		{
            switch (gameModel.CurrentState)
            {
                case GameState.PreGamePhase:
                    break;
                case GameState.GamePhase:
                    gameModel.CheckGameOver();
                    
                    if (gameModel.GameOver)
                    {
                        gameModel.ChangeGameState(GameState.PostGamePhase);
                    }
                    break;
                case GameState.PostGamePhase:
                    break;
                default:
                    break;
            }
		}

        public void EndTurn()
        {
            if (gameModel.CurrentState == GameState.GamePhase
                && !gameModel.GameOver)
            {
                gameModel.AdvanceTurn();
            }
        }

        public void NewGame()
        {
            if (gameModel.CurrentState == GameState.PostGamePhase
                && gameModel.GameOver)
            {
                Debug.Log("NEW GAME!!");
                gameModel.Reset();
                gameModel.ChangeGameState(GameState.PreGamePhase);
            }
        }

        private void OnEnteredState(GameState enteredState)
        {
            switch (enteredState)
            {
                case GameState.PreGamePhase:
                    gameModel.Grid.Cells[4, 6].AddEntity(new Unit(Faction.A, UnitType.Soldier, NameStack.GetName(), gameModel.Grid));
                    gameModel.Grid.Cells[3, 6].AddEntity(new Unit(Faction.A, UnitType.Sniper, NameStack.GetName(), gameModel.Grid));
                    gameModel.Grid.Cells[5, 6].AddEntity(new Unit(Faction.A, UnitType.Medic, NameStack.GetName(), gameModel.Grid));
                    gameModel.Grid.Cells[6, 6].AddEntity(new Unit(Faction.A, UnitType.Soldier, NameStack.GetName(), gameModel.Grid));

                    gameModel.Grid.Cells[4, 1].AddEntity(new Unit(Faction.B, UnitType.Soldier, NameStack.GetName(), gameModel.Grid));
                    gameModel.Grid.Cells[3, 1].AddEntity(new Unit(Faction.B, UnitType.Sniper, NameStack.GetName(), gameModel.Grid));
                    gameModel.Grid.Cells[5, 1].AddEntity(new Unit(Faction.B, UnitType.Medic, NameStack.GetName(), gameModel.Grid));
                    gameModel.Grid.Cells[6, 1].AddEntity(new Unit(Faction.B, UnitType.Soldier, NameStack.GetName(), gameModel.Grid));

                    // TODO: Animate units from portraits onto field.

                    // TODO: When done animating, goto game phase.
                    gameModel.ChangeGameState(GameState.GamePhase);
                    break;
                case GameState.GamePhase:
                    gameModel.StartTurn();
                    break;
                case GameState.PostGamePhase:
                    break;
                default:
                    break;
            }
        }

        private void OnExitedState(GameState exitedState)
        {
            switch (exitedState)
            {
                case GameState.PreGamePhase:
                    break;
                case GameState.GamePhase:
                    break;
                case GameState.PostGamePhase:
                    break;
                default:
                    break;
            }
        }
	}
}