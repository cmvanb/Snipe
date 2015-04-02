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
            gameState.StartTurn();
		}

		public void Update()
		{
		}

        public void EndTurn()
        {
            gameState.AdvanceTurn();
        }
	}
}