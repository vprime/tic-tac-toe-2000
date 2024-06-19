using System.Collections.Generic;

namespace Game.Components
{
    public class GameState
    {
        public readonly Map CurrentMap;
        public readonly Dictionary<PlayerSymbol, Player> Players;
        public int Round = 0;
        public PlayerSymbol CurrentPlayer = PlayerSymbol.X;
        public readonly List<Move> Moves = new();
        public GameTurnState TurnState = GameTurnState.Init;
        public bool GameOver = false;
        public Player? Winner;
        public List<MapTile> WinningTiles;

        public GameState(Board board, Dictionary<PlayerSymbol, Player> players)
        {
            CurrentMap = new Map(board.rows, board.columns);
            Players = players;
        }
    }
}
