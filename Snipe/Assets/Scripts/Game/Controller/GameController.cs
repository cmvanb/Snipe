using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
	public class GameController
	{
		private GameModel gameModel;
        private int playerDeployIndex = 0;
        private float startDeployTime;

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
                    if (Time.time - startDeployTime > 1f)
                    {
                        if (playerDeployIndex >= gameModel.Players.Count)
                        {
                            gameModel.ChangeGameState(GameState.GamePhase);
                        }
                        else
                        {
                            List<Unit> units = gameModel.Grid.GetUnits(gameModel.Players[playerDeployIndex].Faction);

                            int unitIndex = 0;

                            while (unitIndex < units.Count
                                && units[unitIndex].IsDeployed)
                            {
                                ++unitIndex;
                            }

                            if (unitIndex < units.Count)
                            {
                                units[unitIndex].Deploy();
                            }
                            else
                            {
                                if (playerDeployIndex < gameModel.Players.Count)
                                {
                                    startDeployTime = Time.time;
                                    ++playerDeployIndex;
                                }
                            }
                        }
                    }
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
                    NameStack.Reset();

                    Unit[] aUnits = new Unit[]
                    {
                        new Unit(Faction.A, UnitType.Soldier, NameStack.GetName(), gameModel.Grid, 0),
                        new Unit(Faction.A, UnitType.Soldier, NameStack.GetName(), gameModel.Grid, 1),
                        new Unit(Faction.A, UnitType.Soldier, NameStack.GetName(), gameModel.Grid, 2),
                        new Unit(Faction.A, UnitType.Soldier, NameStack.GetName(), gameModel.Grid, 3),
                        new Unit(Faction.A, UnitType.Soldier, NameStack.GetName(), gameModel.Grid, 4),
                        new Unit(Faction.A, UnitType.Soldier, NameStack.GetName(), gameModel.Grid, 5),
                    };

                    Vector2[] aPositions = new Vector2[]
                    {
                        new Vector2(0, 1),
                        new Vector2(0, 2),
                        new Vector2(0, 3),
                        new Vector2(0, 4),
                        new Vector2(0, 5),
                        new Vector2(0, 6),
                    };

                    for (int i = 0; i < aUnits.Length; ++i)
                    {
                        gameModel.Grid.Cells[(int)aPositions[i].x, (int)aPositions[i].y].AddEntity(aUnits[i]);
                    }

                    Unit[] bUnits = new Unit[]
                    {
                        new Unit(Faction.B, UnitType.Soldier, NameStack.GetName(), gameModel.Grid, 6),
                        new Unit(Faction.B, UnitType.Soldier, NameStack.GetName(), gameModel.Grid, 7),
                        new Unit(Faction.B, UnitType.Soldier, NameStack.GetName(), gameModel.Grid, 8),
                        new Unit(Faction.B, UnitType.Soldier, NameStack.GetName(), gameModel.Grid, 9),
                        new Unit(Faction.B, UnitType.Soldier, NameStack.GetName(), gameModel.Grid, 10),
                        new Unit(Faction.B, UnitType.Soldier, NameStack.GetName(), gameModel.Grid, 11),
                    };

                    Vector2[] bPositions = new Vector2[]
                    {
                        new Vector2(7, 1),
                        new Vector2(7, 2),
                        new Vector2(7, 3),
                        new Vector2(7, 4),
                        new Vector2(7, 5),
                        new Vector2(7, 6),
                    };

                    for (int i = 0; i < bUnits.Length; ++i)
                    {
                        gameModel.Grid.Cells[(int)bPositions[i].x, (int)bPositions[i].y].AddEntity(bUnits[i]);
                    }

                    playerDeployIndex = 0;
                    startDeployTime = Time.time;
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