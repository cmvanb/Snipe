using UnityEngine;
using System.Collections.Generic;
using CFramework.Core.Patterns;

namespace Snipe
{
    public class SpriteManager : Singleton<SpriteManager>
    {
        private Dictionary<SpriteID, Sprite> spriteLookup;
        
        public SpriteManager()
        {
            spriteLookup = new Dictionary<SpriteID, Sprite>();
        }

        public void AddSprite(SpriteID id, string texturePath)
        {
            ResourceManager resourceManager = ResourceManager.Instance;
            
            Texture2D spriteTexture = (Texture2D)resourceManager.GetObject(texturePath);

            Rect rect = new Rect(0, 0, spriteTexture.width, spriteTexture.height);

            Vector2 pivot = new Vector2(0f, 1f);

            Sprite sprite = Sprite.Create(spriteTexture, rect, pivot, Constants.PixelsPerUnit);

            spriteLookup[id] = sprite;
        }

        public Sprite GetSprite(SpriteID id)
        {
            return spriteLookup[id];
        }

        public SpriteID GetSpriteIDForTileType(TileType tileType, GridType gridType)
        {
            if (gridType == GridType.Hexagonal)
            {
                switch (tileType)
                {
                    case TileType.Empty:
                        return SpriteID.EmptyHex;
                    case TileType.Grass:
                        return SpriteID.GrassHex;
                    case TileType.Grass2:
                        return SpriteID.Grass2Hex;
                    case TileType.Dirt:
                        return SpriteID.DirtHex;
                }
            }
            else if (gridType == GridType.Rectangular)
            {
                switch (tileType)
                {
                    case TileType.Empty:
                        return SpriteID.EmptyRect;
                    case TileType.Grass:
                        return SpriteID.GrassRect;
                    case TileType.Grass2:
                        return SpriteID.Grass2Rect;
                    case TileType.Dirt:
                        return SpriteID.DirtRect;
                }
            }

            throw new System.Exception("Could not find appropriate sprite for tile type: " + tileType + " on grid of type: " + gridType);
        }

        public SpriteID GetSpriteIDForEntity(Entity entity, GameModel gameModel)
        {
            Unit unit = entity as Unit;

            if (unit != null)
            {
                int index = (20 + (int)unit.Faction * 10);

                if (unit.Faction == gameModel.CurrentPlayer.Faction
                    || unit.IsRevealed)
                {
                    index += (int)unit.UnitType;
                }

                return (SpriteID)index;
            }

            throw new System.Exception("Could not find appropriate sprite for entity: " + entity);
        }
    }
}