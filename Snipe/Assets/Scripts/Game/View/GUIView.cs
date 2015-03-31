using UnityEngine;

namespace Snipe
{
    public class GUIView
    {
        private GameView gameView;
        private bool guiInitialized = false;
        private GameObject selector;

        public GUIView(GameView gameView)
        {
            this.gameView = gameView;
        }

        public void Update(GUIState guiState)
        {
            if (!guiInitialized)
            {
                InitializeGUI(guiState);

                guiInitialized = true;
            }

            UpdateSelector(guiState);
        }

        private void InitializeGUI(GUIState guiState)
        {
            selector = new GameObject("Selector");

            SpriteRenderer spriteRenderer = selector.AddComponent<SpriteRenderer>();

            spriteRenderer.sortingOrder = 20;
        }

        private void UpdateSelector(GUIState guiState)
        {
            if (guiState.SelectorActive)
            {
                if (!selector.activeSelf)
                {
                    selector.SetActive(true);
                }

                Vector3 gridPosition = gameView.GridView.GridPosition;

                selector.transform.localPosition = new Vector3(guiState.CursorPosition.x, guiState.CursorPosition.y, 0);
                                
                SpriteRenderer spriteRenderer = selector.GetComponent<SpriteRenderer>();

                if (spriteRenderer.sprite == null)
                {
                    SpriteManager spriteManager = SpriteManager.Instance;

                    Sprite sprite = spriteManager.GetSprite(SpriteID.Selector);

                    spriteRenderer.sprite = sprite;
                }
            }
            else
            {
                if (selector.activeSelf)
                {
                    selector.SetActive(false);
                }
            }
        }
    }
}