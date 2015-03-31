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

            Sprite sprite = Sprite.Create(spriteTexture, rect, Vector2.zero, Constants.PixelsPerUnit);

            spriteLookup[id] = sprite;
        }

        public Sprite GetSprite(SpriteID id)
        {
            return spriteLookup[id];
        }
    }
}