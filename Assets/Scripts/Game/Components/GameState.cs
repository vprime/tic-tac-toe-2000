using System.Collections.Generic;

namespace Game.Components
{
    public class GameState
    {
        public Map CurrentMap;
        public List<Player> Players;
        public int Round = 0;
        public int CurrentPlayer = 0;
        public List<Move> Moves = new List<Move>();
        public GameTurnState TurnState = GameTurnState.Init;

        public GameState(Board board, List<Player> players)
        {
            CurrentMap = new Map(board.rows, board.columns, -1);
            Players = players;
        }
    }
}
