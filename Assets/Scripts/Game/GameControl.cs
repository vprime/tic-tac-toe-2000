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
        [SerializeField] private GameUi gameUi;

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
            switch (next)
            {
                case AppStates.Game:
                    StartCoroutine(SequenceRoutine());
                    break;
                case AppStates.GameResults when _gameState.Winner.HasValue:
                    gameUi.gameObject.SetActive(true);
                    gameUi.AnnounceWinner(_gameState.Winner.Value.symbol);
                    break;
                case AppStates.GameResults:
                    gameUi.gameObject.SetActive(true);
                    gameUi.AnnounceDraw();
                    break;
                default:
                    gameUi.gameObject.SetActive(false);
                    break;
            }
        }

        private IEnumerator SequenceRoutine()
        {
            gameUi.gameObject.SetActive(true);
            yield return null;
            gameUi.Clear();
            _gameState = new GameState(GameSetup.Board, GameSetup.Players);
            _gameState.CurrentMap.OnTileUpdate += OnTileUpdate;
            while (!MoveCheck.CheckEndGame(ref _gameState))
            {
                GameTurnLoop.UpdateTurnState(ref _gameState);
                gameUi.AnnouncePlayer(_gameState.CurrentPlayer);
                yield return null;
            }
            // Run the game, await a winner
            yield return null;
            RoutineComplete();
        }

        private void RoutineComplete()
        {
            gameUi.Clear();
            appControl.CurrentAppState = AppStates.CelebrateWinner;
        }
    }
}
