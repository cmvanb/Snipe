using UnityEngine;
using System.Collections.Generic;

namespace Snipe
{
    public class GridView : IView
    {
        private Vector2 topLeftOffset;
        private bool gridInitialized;
        private GameObject gameObject;
        private GameObject[,] cellObjects;
        private Dictionary<TileType, SpriteID> tileToSpriteMapping;

        public GridView(Vector2 topLeftOffset)
        {
            this.topLeftOffset = topLeftOffset;

            tileToSpriteMapping = new Dictionary<TileType, SpriteID>();
        }
        
        public void Update(GameState gameState)
        {
            if (!gridInitialized)
            {
                InitializeGrid(gameState);

                gridInitialized = true;
            }

            // Update cell objects.
            Grid grid = gameState.Grid;

            for (int x = 0; x < grid.Width; ++x)
            {
                for (int y = 0; y < grid.Height; ++y)
                {
                    UpdateCellObject(x, y, grid.GetCellAt(x, y));
                }
            }
        }

        private void InitializeGrid(GameState gameState)
        {
            // Create parent object.
            gameObject = new GameObject("GridView");

            gameObject.transform.position = new Vector3(topLeftOffset.x, topLeftOffset.y, 0f);
            
            // Setup cell objects array.
            Grid grid = gameState.Grid;
            
            cellObjects = new GameObject[grid.Width, grid.Height];

            // Create cell objects.
            for (int x = 0; x < grid.Width; ++x)
            {
                for (int y = 0; y < grid.Height; ++y)
                {
                    GameObject cellObject = new GameObject("Cell " + x + ", " + y);

                    cellObject.transform.parent = gameObject.transform;
                    cellObject.AddComponent<SpriteRenderer>();

                    cellObjects[x, y] = cellObject;
                }
            }

            // Setup tile sprite mappings.
            tileToSpriteMapping.Clear();

            if (grid.GridType == GridType.Hexagonal)
            {
                // TODO: Map hexagonal sprites.
            }
            else if (grid.GridType == GridType.Rectangular)
            {
                tileToSpriteMapping[TileType.Empty] = SpriteID.EmptyRect;
                tileToSpriteMapping[TileType.Grass] = SpriteID.GrassRect;
            }
        }

        private void UpdateCellObject(int x, int y, Cell cell)
        {
            // Get sprite.
            SpriteManager spriteManager = SpriteManager.Instance;

            TileType tileType = cell.TileType;

            SpriteID spriteID = tileToSpriteMapping[tileType];

            Sprite sprite = spriteManager.GetSprite(spriteID);

            // Update cell object position and sprite.
            GameObject cellObject = cellObjects[x, y];

            cellObject.transform.position = new Vector3(x, -y, 0);
            
            SpriteRenderer spriteRenderer = cellObject.GetComponent<SpriteRenderer>();
            
            spriteRenderer.sprite = sprite;
        }
    }
}