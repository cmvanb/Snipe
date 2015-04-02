using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
	public class Cell
	{
        public Vector2 Position { get { return position; } }
        public TileType TileType { get { return tileType; } set { tileType = value; } }
        public List<Entity> Entities { get { return entities; } }

        private Vector2 position;
        private TileType tileType = TileType.Empty;
        private List<Entity> entities;

        public Cell(Vector2 position, TileType tileType)
        {
            this.position = position;
            this.tileType = tileType;
            this.entities = new List<Entity>();
        }

        public void CleanUp()
        {
            foreach (Entity entity in Entities)
            {
                entity.CleanUp();
            }

            entities.Clear();

            entities = null;
        }

		public void AddEntity(Entity entity)
        {
            entity.Location = this;

            entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            if (entities.Contains(entity))
            {
                entities.Remove(entity);
            }
        }

        public Unit GetUnit()
        {
            foreach (Entity entity in Entities)
            {
                Unit unit = entity as Unit;

                if (unit != null)
                {
                    return unit;
                }
            }

            return null;
        }

        public bool IsEmpty()
        {
            return entities.Count == 0;
        }
	}
}