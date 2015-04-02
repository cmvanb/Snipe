using UnityEngine;
using System.Collections.Generic;

namespace Snipe
{
    public class EntityView : IView
    {
        public Entity Entity { get { return entity; } }

        private GridView gridView;
        private Entity entity;
        private bool entityInitialized;
        private GameObject gameObject;
        private Player currentPlayer;

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

            Vector2 position = entity.Location.Position;

            gameObject.transform.position = gridView.GridPosition + new Vector3(position.x, -position.y, -0.5f);

            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer.sprite == null
                || currentPlayer != gameModel.CurrentPlayer)
            {
                SpriteManager spriteManager = SpriteManager.Instance;

                SpriteID spriteID = spriteManager.GetSpriteIDForEntity(entity, gameModel);

                Sprite sprite = spriteManager.GetSprite(spriteID);

                spriteRenderer.sprite = sprite;

                currentPlayer = gameModel.CurrentPlayer;
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

            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();

            boxCollider.size = new Vector3(1f, 1f, 0.1f);
            boxCollider.center = new Vector3(0.5f, -0.5f, 0f);
        }
    }
}