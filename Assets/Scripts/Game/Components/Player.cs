using System;

namespace Game.Components
{
    [Serializable]
    public struct Player
    {
        public PlayerSymbol symbol;

        public PlayerType type;

        public Player(PlayerSymbol symbol, PlayerType type)
        {
            this.symbol = symbol;
            this.type = type;
        }

        public override string ToString()
        {
            return $"player {type} {symbol}";
        }
    }
}
