
namespace Game.Components
{
    /// <summary>
    /// Data from a <see cref="Map"/>
    /// </summary>
    public struct MapTile
    {
        /// <summary>
        /// Location of the tile
        /// </summary>
        public MapPosition Position;
        
        /// <summary>
        /// Value of the tile
        /// </summary>
        public readonly int Value;

        /// <summary>
        /// Construct a Tile with <see cref="MapPosition"/> and Value.
        /// </summary>
        /// <param name="position">Tile position</param>
        /// <param name="value">Tile value</param>
        public MapTile(MapPosition position, int value)
        {
            Position = position;
            Value = value;
        }

        /// <summary>
        /// Construct a Tile with X and Y integers.
        /// </summary>
        /// <param name="x">Column</param>
        /// <param name="y">Row</param>
        /// <param name="value">Tile Value</param>
        public MapTile(int x, int y, int value)
        {
            Position = new MapPosition(x, y);
            Value = value;
        }
    }
}
