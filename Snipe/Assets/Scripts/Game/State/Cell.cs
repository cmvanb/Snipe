using System.Collections.Generic;
using UnityEngine;

namespace Snipe
{
	public class Cell
	{
        public Vector2 Position { get { return position; } }
        public TileType TileType { get { return tileType; } set { tileType = value; } }

        private Vector2 position;
        private TileType tileType = TileType.Empty;
        private List<Entity> entities;

        public Cell(Vector2 position, TileType tileType)
        {
            this.position = position;

            this.tileType = tileType;

            this.entities = new List<Entity>();
        }

		public void AddEntity(Entity entity)
        {
            entity.Location = this;

            entities.Add(entity);
        }
	}
}