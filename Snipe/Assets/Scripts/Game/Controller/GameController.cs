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
            if (gameState.CurrentPlayer.ActionPoints == 0)
            {
                // TODO: Handle turn over.
                gameState.AdvanceTurn();
            }
		}
	}
}