using UnityEngine;

namespace Snipe
{
    public class GridMath
    {
        public static GridView GridView;

        public static Vector2 GridPositionFromScreenPosition(Vector2 screenPosition)
        {
            if (GridView != null)
            {
                float x = (screenPosition.x - GridView.GridPosition.x) * (Constants.PixelsPerUnit / Constants.TileWidth);
                float y = (-(screenPosition.y - GridView.GridPosition.y)) * (Constants.PixelsPerUnit / Constants.TileHeight);

                return new Vector2(x, y);
            }

            throw new System.Exception("Math is hard.");
        }

        public static Vector2 ScreenPositionFromGridPosition(Vector2 gridPosition)
        {
            if (GridView != null)
            {
                float x = GridView.GridPosition.x + (gridPosition.x * (Constants.TileWidth / Constants.PixelsPerUnit));
                float y = GridView.GridPosition.y - (gridPosition.y * (Constants.TileHeight / Constants.PixelsPerUnit));

                return new Vector2(x, y);
            }

            throw new System.Exception("Math is hard.");
        }
    }
}