using Game.Components;
using UnityEngine;

namespace Game.Systems
{
    public class AiPlayer
    {
        public static void PickMove(ref GameState state)
        {
            var availableMoves = state.CurrentMap.OpenPositions();
            var moveSelection = availableMoves[Random.Range(0, availableMoves.Count)];
            var move = new Move(){Player = state.Players[state.CurrentPlayer], Position = moveSelection.Position};
            
            state.Moves.Add(move);
            state.CurrentMap.Set(moveSelection.Position.X, moveSelection.Position.Y, state.CurrentPlayer);
            
            state.TurnState = GameTurnState.CheckMove;
        }

    }
}
