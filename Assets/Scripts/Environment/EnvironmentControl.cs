using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Components;
using App.Systems;
using Environment.Components;
using Game;
using Game.Components;
using UnityEngine;

namespace Environment
{
    public class EnvironmentControl : MonoBehaviour
    {
        [SerializeField] private AppControl appControl;
        [SerializeField] private GameControl gameControl;
        [SerializeField] private EnvironmentObjects environmentObjects;

        private readonly List<GameObject> _spawnedAssets = new();
        private readonly List<List<GamePiece>> _boardPieces = new();
        
        private void Awake()
        {
            appControl.OnAppStateChange.AddListener(HandleAppStateChange);
        }

        private void HandleAppStateChange(AppStates prev, AppStates next)
        {
            switch (next)
            {
                case AppStates.Init:
                    break;
                case AppStates.LoadAssets:
                    break;
                case AppStates.OpeningSequence:
                    CleanUpGameEnvironment();
                    break;
                case AppStates.MainMenu:
                    CleanUpGameEnvironment();
                    break;
                case AppStates.GameCountdown:
                    StartCoroutine(SequenceCountdownRoutine());
                    break;
                case AppStates.Game:
                    break;
                case AppStates.CelebrateWinner:
                    break;
                case AppStates.GameResults:
                    CleanUpGameEnvironment();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(next), next, null);
            }
        }

        private IEnumerator SequenceCountdownRoutine()
        {
            // Render the game environment
            SetupEnvironmentBackground();
            var gameSetup = gameControl.GameSetup;
            LayoutGameTiles(gameSetup.Board);
            gameControl.OnTileUpdate += UpdateTileDisplay;
            
            yield return null;
            CountdownRoutineComplete();
        }

        private void UpdateTileDisplay(MapTile tile)
        {
            if (tile.Value >= 0)
                _boardPieces[tile.Position.X]?[tile.Position.Y]?.SetPlayer(gameControl.GameState.Players[(PlayerSymbol)tile.Value]);
        }

        private void SetupEnvironmentBackground()
        {
            _spawnedAssets.Add(Instantiate(environmentObjects.gameBoardBackground));
        }

        private void LayoutGameTiles(Board gameBoard)
        {
            var boardRoot = Instantiate(new GameObject("GameBoard"));
            _spawnedAssets.Add(boardRoot);
            for (var iCol = 0; iCol < gameBoard.columns; iCol++)
            {
                var columnPosition = iCol * environmentObjects.columnHeight;
                var piecesCol = new List<GamePiece>();
                for (var iRow = 0; iRow < gameBoard.rows; iRow++)
                {
                    var rowPosition = iRow * environmentObjects.rowWidth;
                    var gamePiece = Instantiate(environmentObjects.gamePiece, boardRoot.transform);
                    gamePiece.Setup(iCol, iRow, HandlePieceInteraction);
                    gamePiece.transform.position = new Vector3(rowPosition, columnPosition, 0f);
                    piecesCol.Add(gamePiece);
                }
                _boardPieces.Add(piecesCol);
            }
            boardRoot.transform.position = Vector3.forward * environmentObjects.boardZOffset;
        }

        private void HandlePieceInteraction(int col, int row, GamePiece piece)
        {
            if (gameControl.PlayerInput(col, row, out var currentPlayer))
            {
                piece.SetPlayer(currentPlayer);
            }
            else
            {
                piece.RejectInput();
            }
        }

        private void CountdownRoutineComplete()
        {
            appControl.CurrentAppState = AppStates.Game;
        }

        private void CleanUpGameEnvironment()
        {
            foreach (var asset in _spawnedAssets.Where(asset => asset))
            {
                Destroy(asset);
            }
            _spawnedAssets.Clear();
            _boardPieces.Clear();
        }
    }
}
