using Game.Components;

namespace Game.Systems
{
    public static class HumanPlayer
    {
        public static bool RequestMove(ref GameState state, int x, int y)
        {
            if (!state.CurrentMap.ValidateMove(x, y)) return false;
            var move = new Move {Player = state.Players[state.CurrentPlayer], Position = new MapPosition(x, y)};
            state.Moves.Add(move);
            state.CurrentMap.Set(move.Position.X, move.Position.Y, (int)state.CurrentPlayer);
            
            state.TurnState = GameTurnState.CheckMove;
            return true;
        }
    }
}
