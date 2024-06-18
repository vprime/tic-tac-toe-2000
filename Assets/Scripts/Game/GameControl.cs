using System;
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

        public GameSetup GameSetup { get; set; }

        public GameState GameState => _gameState;

        public event Action<MapTile> OnTileUpdate;

        public bool PlayerInput(int col, int row, out Player currentPlayer)
        {
            currentPlayer = _gameState.Players[_gameState.CurrentPlayer];
            return HumanPlayer.RequestMove(ref _gameState, col, row);
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

        private IEnumerator SequenceRoutine()
        {
            Debug.Log("Game sequence begun");
            _gameState = new GameState(GameSetup.Board, GameSetup.Players);
            _gameState.CurrentMap.OnTileUpdate += OnTileUpdate;
            while (!MoveCheck.CheckEndGame(_gameState))
            {
                GameTurnLoop.UpdateTurnState(ref _gameState);
                yield return null;
            }
            Debug.Log("Game sequence completed");
            // Run the game, await a winner
            yield return null;
            RoutineComplete();
        }

        private void RoutineComplete()
        {
            appControl.CurrentAppState = AppStates.CelebrateWinner;
        }
    }
}
