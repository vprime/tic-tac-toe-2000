using System.Collections;
using UnityEngine;
using App.Components;
using App.Systems;
using Game.Components;
using Game.Systems;

namespace Game
{
    public class GameControl : MonoBehaviour
    {
        [SerializeField] private AppControl appControl;

        private GameState _gameState;

        private GameSetup _gameSetup;

        public GameSetup GameSetup
        {
            get => _gameSetup;
            set => _gameSetup = value;
        }
        
        
        private void Awake()
        {
            appControl.OnAppStateChange.AddListener(HandleAppStateChange);
        }

        private void HandleAppStateChange(AppStates prev, AppStates next)
        {
            if (next == AppStates.Game)
            {
                StartCoroutine(SequenceRoutine());
            }
        }

        IEnumerator SequenceRoutine()
        {
            _gameState = new GameState(_gameSetup.Board, _gameSetup.Players);
            while (!MoveCheck.CheckGameover(_gameState.CurrentMap, _gameState.Round))
            {
                GameTurnLoop.UpdateTurnState(ref _gameState);
                yield return null;
            }
            // Run the game, await a winner
            yield return null;
            RoutineComplete();
        }

        void RoutineComplete()
        {
            appControl.CurrentAppState = AppStates.CelebrateWinner;
        }
    }
}
