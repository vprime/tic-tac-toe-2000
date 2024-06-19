using System.Collections.Generic;

namespace Game.Components
{
    public class GameSetup
    {
        public readonly Dictionary<PlayerSymbol, Player> Players = new();
        public Board Board;
    }
}
