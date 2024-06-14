using System;
using System.Collections;
using Game.Components;
using UnityEngine;

namespace Game.Systems
{
    public class GameTurnLoop
    {
        public static void UpdateTurnState(ref GameState state)
        {
            switch (state.TurnState)
            {
                case GameTurnState.Init:
                    state.TurnState = GameTurnState.SelectPlayer;
                    break;
                case GameTurnState.SelectPlayer:
                    state.CurrentPlayer = SelectPlayer(ref state).index;
                    state.TurnState = GameTurnState.PlayerInput;
                    break;
                case GameTurnState.PlayerInput:
                    RequestPlayerInput(ref state);
                    break;
                case GameTurnState.CheckMove:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        

        static Player SelectPlayer(ref GameState state)
        {
            return state.Players[state.Round % state.Players.Count];
        }

        static void RequestPlayerInput(ref GameState state)
        {
            switch (state.Players[state.CurrentPlayer].type)
            {
                case PlayerType.Human:
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
