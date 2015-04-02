using UnityEngine;

namespace Snipe
{
    public class Dice
    {
        private static readonly int maxDistance = 6;

        public static bool RollForHit(Cell location, Cell target)
        {
            int xDifference = (int)Mathf.Abs(location.Position.x - target.Position.x);
            int yDifference = (int)Mathf.Abs(location.Position.y - target.Position.y);

            int distance = Mathf.Max(xDifference, yDifference);
            
            if (distance > maxDistance)
            {
                return false;
            }

            float successChance = (1f / distance);
            float roll = UnityEngine.Random.Range(0f, 1f);

            Debug.Log(roll + " <= " + successChance + " [" + (roll <= successChance ? "HIT]" : "MISS]"));

            if (roll <= successChance)
            {
                return true;
            }

            return false;
        }
    }
}