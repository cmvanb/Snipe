namespace Snipe
{
	public class Cell
	{
        public TileType TileType { get { return tileType; } }

        private TileType tileType = TileType.Empty;

        public Cell()
        {
            tileType = (TileType)UnityEngine.Random.Range(0, 2);
        }

		public bool IsEmpty()
        {
            return tileType == TileType.Empty;
        }
	}
}