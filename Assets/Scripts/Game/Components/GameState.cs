using System.Collections.Generic;

namespace Game.Components
{
    public class GameState
    {
        public readonly Map CurrentMap;
        public readonly Dictionary<PlayerSymbol, Player> Players;
        public int Round = 0;
        public PlayerSymbol CurrentPlayer = PlayerSymbol.X;
        public List<Move> Moves = new();
        public GameTurnState TurnState = GameTurnState.Init;

        public GameState(Board board, Dictionary<PlayerSymbol, Player> players)
        {
            CurrentMap = new Map(board.rows, board.columns, -1);
            Players = players;
        }
    }
}
