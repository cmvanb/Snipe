namespace Snipe
{
    public abstract class Entity
    {
        public Cell Location { get { return location; } set { location = value; } }
        public bool IsAlive { get { return isAlive; } }
        public abstract string Name { get; }

        protected Cell location;
        protected bool isAlive;

        public Entity()
        {
            isAlive = true;
        }
    }
}