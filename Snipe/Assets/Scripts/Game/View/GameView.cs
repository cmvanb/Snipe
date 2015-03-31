using UnityEngine;
using System.Collections.Generic;

namespace Snipe
{
	public class GameView : IView
	{
        public GridView GridView { get { return gridView; } }

        private GridView gridView;
        private List<IView> views;
		
		public GameView(Camera camera)
		{
            views = new List<IView>();

            gridView = new GridView();

            views.Add(gridView);

            camera.GetComponent<CameraOrthoSize>().ResolutionChangedEvent += OnResolutionChanged;
		}

        public void Update(GameState gameState)
		{
			foreach (IView view in views)
            {
                view.Update(gameState);
            }
		}

        public void OnResolutionChanged(int width, int height)
        {
            // TODO: Update views by polling game state.
        }
	}
}