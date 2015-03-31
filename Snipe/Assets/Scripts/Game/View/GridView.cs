using UnityEngine;

namespace Snipe
{
    public class GridView : IView
    {
        private Vector2 topLeftOffset;
        private bool gameObjectInitialized;
        private GameObject gameObject;

        public GridView(Vector2 topLeftOffset)
        {
            this.topLeftOffset = topLeftOffset;
        }
        
        public void Update(GameState gameState)
        {
            if (!gameObjectInitialized)
            {
                InitializeGameObject(gameState);

                gameObjectInitialized = true;
            }

            // TODO: Update grid objects/sprites.
        }

        private void InitializeGameObject(GameState gameState)
        {
            ResourceManager resourceManager = ResourceManager.Instance;

            Texture2D emptyRectTexture = (Texture2D)resourceManager.GetObject("Sprites/empty-rect");

            Sprite emptyRectSprite = Sprite.Create(emptyRectTexture, new Rect(0, 0, emptyRectTexture.width, emptyRectTexture.height), Vector2.zero, Constants.PixelsPerUnit);

            gameObject = new GameObject("GridView");

            gameObject.transform.position = new Vector3(topLeftOffset.x, topLeftOffset.y, 0f);
            
            Grid grid = gameState.Grid;

            for (int x = 0; x < grid.Width; ++x)
            {
                for (int y = 0; y < grid.Height; ++y)
                {
                    GameObject cellObject = new GameObject("Cell " + x + ", " + y);

                    cellObject.transform.parent = gameObject.transform;
                    cellObject.transform.position = new Vector3(x, -y, 0);

                    SpriteRenderer spriteRenderer = cellObject.AddComponent<SpriteRenderer>();

                    spriteRenderer.sprite = emptyRectSprite;
                }
            }
        }
    }
}