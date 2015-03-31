namespace Snipe
{
    public class Entity
    {
        public Cell Location { get { return location; } set { location = value; } }

        private Cell location;

        public Entity()
        {
        }
    }
}