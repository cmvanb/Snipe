using UnityEngine;

namespace Snipe
{
    public abstract class Entity
    {
        public Cell Location { get { return location; } set { location = value; } }
        public bool IsAlive { get { return isAlive; } }
        public abstract string Name { get; }

        protected Grid grid;
        protected Cell location;
        protected bool isAlive;
        
        public Entity(Grid grid)
        {
            this.grid = grid;

            isAlive = true;
        }

        public void CleanUp()
        {
            grid = null;
            location = null;
        }
    }
}