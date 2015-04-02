using UnityEngine;

namespace Snipe
{
    public class Constants
    {
        public static float PixelsPerUnit = 32f;
        public static int ActionPointsPerTurn = 2;
        public static int TileWidth = 64;
        public static int TileHeight = 56;
        public static Vector2 GridOffset = new Vector2(0f, 1f);
        public static Vector2 EntityOffset = new Vector2(0f, 0.5f);

        public static float UnitsPerPixel
        {
            get
            {
                return 1f / PixelsPerUnit;
            }
        }
    }
}