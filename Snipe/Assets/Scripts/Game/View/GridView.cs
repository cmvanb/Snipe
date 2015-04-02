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
        private Dictionary<Entity, EntityView> entityViews;
        private List<Entity> deadEntities;

        public GridView()
        {
            GridMath.GridView = this;

            entityViews = new Dictionary<Entity, EntityView>();
            deadEntities = new List<Entity>();
        }
        
        public void Update(GameModel gameModel)
        {
            if (!gridInitialized)
            {
                InitializeGrid(gameModel);

                gridInitialized = true;
            }

            // Update cell objects.
            Grid grid = gameModel.Grid;

            for (int x = 0; x < grid.Width; ++x)
            {
                for (int y = 0; y < grid.Height; ++y)
                {
                    UpdateCellObject(x, y, grid.GetCellAt(x, y), grid.GridType);
                }
            }

            // Update entity views.
            UpdateEntityViews(gameModel);

            // Update grid position (can react to resolution changes).
            UpdateGridPosition(gameModel);
        }

        public void CleanUp()
        {
            foreach (Entity entity in entityViews.Keys)
            {
                entityViews[entity].CleanUp();
            }

            entityViews.Clear();
            deadEntities.Clear();

            GameObject.Destroy(gameObject);

            gameObject = null;
            cellObjects = null;
            entityViews = null;
            deadEntities = null;
        }

        public void Reset()
        {
            foreach (Entity entity in entityViews.Keys)
            {
                entityViews[entity].CleanUp();
            }

            entityViews.Clear();
            deadEntities.Clear();
        }

        private void InitializeGrid(GameModel gameModel)
        {
            // Create parent object.
            gameObject = new GameObject("GridView");
            
            // Setup cell objects array.
            Grid grid = gameModel.Grid;
            
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
        }

        private void UpdateEntityViews(GameModel gameModel)
        {
            Grid grid = gameModel.Grid;

            // Create entity views if necessary.
            for (int x = 0; x < grid.Width; ++x)
            {
                for (int y = 0; y < grid.Height; ++y)
                {
                    Cell cell = grid.GetCellAt(x, y);

                    foreach (Entity entity in cell.Entities)
                    {
                        if (!entityViews.ContainsKey(entity))
                        {
                            entityViews[entity] = new EntityView(this, entity);
                        }
                    }
                }
            }

            // Update entities.
            foreach (Entity entity in entityViews.Keys)
            {
                if (!entity.IsAlive)
                {
                    entityViews[entity].CleanUp();
                    deadEntities.Add(entity);
                }
                else
                {
                    entityViews[entity].Update(gameModel);
                }
            }

            // Remove dead entities.
            foreach (Entity entity in deadEntities)
            {
                entityViews.Remove(entity);
            }

            deadEntities.Clear();

        }

        private void UpdateGridPosition(GameModel gameModel)
        {
            Grid grid = gameModel.Grid;
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

            SpriteManager spriteManager = SpriteManager.Instance;

            TileType tileType = cell.TileType;

            SpriteID spriteID = spriteManager.GetSpriteIDForTileType(tileType, gridType);

            Sprite sprite = spriteManager.GetSprite(spriteID);

            spriteRenderer.sprite = sprite;
        }
    }
}