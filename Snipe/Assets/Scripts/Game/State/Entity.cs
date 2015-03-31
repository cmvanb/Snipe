namespace Snipe
{
    public abstract class Entity
    {
        public Cell Location { get { return location; } set { location = value; } }
        public abstract string Name { get; }

        private Cell location;

        public Entity()
        {
        }
    }
}