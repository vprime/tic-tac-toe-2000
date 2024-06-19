using System;
using Game.Components;

namespace Game.Systems
{
    public static class GameTurnLoop
    {
        public static void UpdateTurnState(ref GameState state)
        {
            switch (state.TurnState)
            {
                case GameTurnState.Init:
                    state.TurnState = GameTurnState.SelectPlayer;
                    break;
                case GameTurnState.SelectPlayer:
                    state.CurrentPlayer = SelectPlayer(ref state).symbol;
                    state.TurnState = GameTurnState.PlayerInput;
                    RequestPlayerInput(ref state);
                    break;
                case GameTurnState.PlayerInput:
                    break;
                case GameTurnState.CheckMove:
                    state.Round += 1;
                    state.TurnState = GameTurnState.Init;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static Player SelectPlayer(ref GameState state)
        {
            // Check if the round is even or odd with a modulo 2. X always goes first so it's even.
            var symbol = state.Round % 2 == 0 ? PlayerSymbol.X : PlayerSymbol.O;
            return state.Players[symbol];
        }

        private static void RequestPlayerInput(ref GameState state)
        {
            switch (state.Players[state.CurrentPlayer].type)
            {
                case PlayerType.Human:
                    // Await human input
                    break;
                case PlayerType.AI:
                    AiPlayer.PickMove(ref state);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
