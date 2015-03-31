using UnityEngine;
using System.Collections.Generic;

namespace Snipe
{
	public class GameView : IView
	{
        private List<IView> views;
		
		public GameView()
		{
            views = new List<IView>();

            Vector2 topLeftOffset = new Vector2(0, 0);

            GridView gridView = new GridView(topLeftOffset);

            views.Add(gridView);
		}

        public void Update(GameState gameState)
		{
			foreach (IView view in views)
            {
                view.Update(gameState);
            }
		}
	}
}