using UnityEngine;

namespace Snipe
{
	public class GameController
	{
		private GameState gameState;

		public GameController(GameState gameState)
		{
			this.gameState = gameState;
		}
		
		public void Start()
		{
            Debug.Log("It is " + gameState.Players[gameState.TurnIndex].Name + "'s turn.");
		}

		public void Update()
		{
		}
	}
}