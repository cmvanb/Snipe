using UnityEngine;
using System.Collections.Generic;

namespace Snipe
{
	public class GameView : IView
	{
        public GridView GridView { get { return gridView; } }

        private GridView gridView;
        private List<IView> views;
		
		public GameView(GameModel gameModel, Camera camera)
		{
            views = new List<IView>();

            gridView = new GridView();

            views.Add(gridView);

            gameModel.GameResetEvent += OnGameReset;
            camera.GetComponent<CameraOrthoSize>().ResolutionChangedEvent += OnResolutionChanged;
		}

        public void Update(GameModel gameModel)
		{
			foreach (IView view in views)
            {
                view.Update(gameModel);
            }
		}

        public void CleanUp()
        {
            foreach (IView view in views)
            {
                view.CleanUp();
            }
        }

        public void OnGameReset()
        {
            gridView.Reset();
        }

        public void OnResolutionChanged(int width, int height)
        {
            // TODO: Update views by polling game model.
        }
	}
}