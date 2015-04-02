using UnityEngine;
using System.Collections.Generic;

namespace Snipe
{
    public class EntityView : IView
    {
        private static List<Vector3> deployPositions = new List<Vector3>()
        {
            new Vector3(-10.1f, 8.6f, 0f),
            new Vector3(-10.1f, 5.85f, 0f),
            new Vector3(-10.1f, 3.1f, 0f),
            new Vector3(-10.1f, 0.35f, 0f),
            new Vector3(-10.1f, -2.4f, 0f),
            new Vector3(-10.1f, -5.15f, 0f),
            new Vector3(8.1f, 8.6f, 0f),
            new Vector3(8.1f, 5.85f, 0f),
            new Vector3(8.1f, 3.1f, 0f),
            new Vector3(8.1f, 0.35f, 0f),
            new Vector3(8.1f, -2.4f, 0f),
            new Vector3(8.1f, -5.15f, 0f),
        };

        public Entity Entity { get { return entity; } }

        private GridView gridView;
        private Entity entity;
        private bool entityInitialized;
        private GameObject gameObject;
        private SpriteID currentSpriteID;

        public EntityView(GridView gridView, Entity entity)
        {
            this.gridView = gridView;
            this.entity = entity;
        }

        public void Update(GameModel gameModel)
        {
            if (!entityInitialized)
            {
                InitializeEntity(gameModel);

                entityInitialized = true;
            }
            
            Vector3 targetPosition = gridView.GridPosition + new Vector3(
                entity.Location.Position.x * (Constants.TileWidth / Constants.PixelsPerUnit) + Constants.EntityOffset.x,
                -entity.Location.Position.y * (Constants.TileHeight / Constants.PixelsPerUnit) + Constants.EntityOffset.y,
                -0.5f);

            Unit unit = entity as Unit;

            if (unit != null
                && !unit.IsDeployed)
            {
                Vector3 startPosition = deployPositions[unit.DeployIndex];

                gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, unit.DeployPercentage);
            }
            else
            {
                gameObject.transform.position = targetPosition;
            }

            gameObject.transform.localScale = new Vector3(2f, 2f, 1f);

            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            
            SpriteManager spriteManager = SpriteManager.Instance;

            SpriteID spriteID = spriteManager.GetSpriteIDForEntity(entity, gameModel);

            if (spriteRenderer.sprite == null
                || spriteID != currentSpriteID)
            {
                currentSpriteID = spriteID;

                Sprite sprite = spriteManager.GetSprite(spriteID);

                spriteRenderer.sprite = sprite;
            }
        }

        public void CleanUp()
        {
            GameObject.Destroy(gameObject);

            gridView = null;
            entity = null;
            gameObject = null;
        }
        
        private void InitializeEntity(GameModel gameModel)
        {
            gameObject = new GameObject(entity.Name);

            SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

            spriteRenderer.sortingOrder = 10;
            /*
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();

            boxCollider.size = new Vector3(1f, 1f, 0.1f);
            boxCollider.center = new Vector3(0.5f, -0.5f, 0f);*/
        }
    }
}