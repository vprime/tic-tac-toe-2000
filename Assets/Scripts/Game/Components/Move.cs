
namespace Game.Components
{
    public struct Move
    {
        public Player Player;
        public MapPosition Position;
        public override string ToString()
        {
            return $"{Player} selects {Position.X}:{Position.Y}";
        }
    }
}
