using UnityEngine;
using System.Collections.Generic;

namespace Snipe
{
    public class GridView : IView
    {
        public Vector3 GridPosition { get { return gridPosition; } }

        private Vector3 gridPosition = Vector3.zero;
        private bool gridInitialized;
        private GameObject gameObject;
        private GameObject[,] cellObjects;
        private List<EntityView> entityViews;

        public GridView()
        {
            GridMath.GridView = this;

            entityViews = new List<EntityView>();
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
                    UpdateCellObject(x, y, grid.GetCellAt(x, y), grid.GridType);
                }
            }

            // Update entity views.
            foreach (EntityView entityView in entityViews)
            {
                entityView.Update(gameState);
            }

            // Update grid position (can react to resolution changes).
            UpdateGridPosition(gameState);
        }

        private void InitializeGrid(GameState gameState)
        {
            // Create parent object.
            gameObject = new GameObject("GridView");
            
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

                    BoxCollider boxCollider = cellObject.AddComponent<BoxCollider>();

                    boxCollider.size = new Vector3(1f, 1f, 0.1f);
                    boxCollider.center = new Vector3(0.5f, -0.5f, 0f);

                    cellObjects[x, y] = cellObject;
                }
            }

            // Create entity views.
            for (int x = 0; x < grid.Width; ++x)
            {
                for (int y = 0; y < grid.Height; ++y)
                {
                    Cell cell = grid.GetCellAt(x, y);

                    foreach (Entity entity in cell.Entities)
                    {
                        EntityView entityView = new EntityView(this, entity);

                        entityViews.Add(entityView);
                    }
                }
            }
        }

        private void UpdateGridPosition(GameState gameState)
        {
            Grid grid = gameState.Grid;
            float gridUnitWidth = grid.Width;
            float gridUnitHeight = grid.Height;

            gridPosition = new Vector3((-gridUnitWidth / 2f), (gridUnitHeight / 2f), 0f);

            gameObject.transform.position = gridPosition;
        }

        private void UpdateCellObject(int x, int y, Cell cell, GridType gridType)
        {
            GameObject cellObject = cellObjects[x, y];

            cellObject.transform.localPosition = new Vector3(x, -y, 0);

            SpriteRenderer spriteRenderer = cellObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer.sprite == null)
            {
                SpriteManager spriteManager = SpriteManager.Instance;

                TileType tileType = cell.TileType;

                SpriteID spriteID = spriteManager.GetSpriteIDForTileType(tileType, gridType);

                Sprite sprite = spriteManager.GetSprite(spriteID);

                spriteRenderer.sprite = sprite;
            }
        }
    }
}