using UnityEngine;
using System.Collections.Generic;

namespace Snipe
{
    public class EntityView : IView
    {
        private GridView gridView;
        private Entity entity;
        private bool entityInitialized;
        private GameObject gameObject;

        public EntityView(GridView gridView, Entity entity)
        {
            this.gridView = gridView;
            this.entity = entity;
        }

        public void Update(GameState gameState)
        {
            if (!entityInitialized)
            {
                InitializeEntity(gameState);

                entityInitialized = true;
            }

            // Get sprite.
            SpriteManager spriteManager = SpriteManager.Instance;

            SpriteID spriteID = spriteManager.GetSpriteIDForEntity(entity);

            Sprite sprite = spriteManager.GetSprite(spriteID);

            // Update object position and sprite.
            Vector2 position = entity.Location.Position;

            gameObject.transform.position = gridView.GridPosition + new Vector3(position.x, -position.y, 0);

            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            spriteRenderer.sprite = sprite;
        }

        private void InitializeEntity(GameState gameState)
        {
            gameObject = new GameObject(entity.Name);

            SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

            spriteRenderer.sortingOrder = 10;
        }
    }
}