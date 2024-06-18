
namespace Game.Components
{
    public struct MapTile
    {
        public MapPosition Position;
        public readonly int Value;

        public MapTile(MapPosition position, int value)
        {
            Position = position;
            Value = value;
        }

        public MapTile(int x, int y, int value)
        {
            Position = new MapPosition(x, y);
            Value = value;
        }
    }
}
