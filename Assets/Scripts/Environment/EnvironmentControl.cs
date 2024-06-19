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
        [SerializeField] private AudioSource gameMusicSource;
        [SerializeField] private AudioClip startGameMusic;
        [SerializeField] private AudioClip successMusic;
        [SerializeField] private AudioClip lossMusic;
        [SerializeField] private AudioClip drawMusic;
        

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
                    StartCoroutine(SequenceWinnerRoutine());
                    break;
                case AppStates.GameResults:
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
            gameMusicSource.PlayOneShot(startGameMusic);
            yield return AnimateSetBoard();
            CountdownRoutineComplete();
        }

        private IEnumerator SequenceWinnerRoutine()
        {
            yield return AnimateClearingBoard();
            // Display the game's result.
            if (gameControl.GameState.Winner is not null)
            {
                if (gameControl.GameState.Winner.Value.type is PlayerType.Human)
                    gameMusicSource.PlayOneShot(successMusic);
                else
                    gameMusicSource.PlayOneShot(lossMusic);
                // There was a winner
                yield return DisplayWinningRow();
            }
            else
            {
                gameMusicSource.PlayOneShot(drawMusic);
                
            }
            yield return null;
            appControl.CurrentAppState = AppStates.GameResults;
        }

        private IEnumerator AnimateSetBoard()
        {
            for (var iCol = 0; iCol < _boardPieces.Count; iCol++)
            {
                var columnList = _boardPieces[iCol];
                for (var iRow = 0; iRow < columnList.Count; iRow++)
                {
                    columnList[iRow]?.Light();
                }
            }
            yield return new WaitForSeconds(0.2f);
            for (var iCol = 0; iCol < _boardPieces.Count; iCol++)
            {
                var columnList = _boardPieces[iCol];
                for (var iRow = 0; iRow < columnList.Count; iRow++)
                {
                    columnList[iRow]?.ClearPlayer();
                }
            }
            yield return new WaitForSeconds(0.2f);
        }

        private IEnumerator AnimateClearingBoard()
        {
            for (var iCol = 0; iCol < _boardPieces.Count; iCol++)
            {
                var columnList = _boardPieces[iCol];
                for (var iRow = 0; iRow < columnList.Count; iRow++)
                {
                    columnList[iRow]?.Light();
                    yield return new WaitForSeconds(0.1f);
                    columnList[iRow]?.ClearPlayer();
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        private IEnumerator DisplayWinningRow()
        {
            foreach (var tile in gameControl.GameState.WinningTiles)
            {
                if (gameControl.GameState.Winner.HasValue)
                {
                    _boardPieces[tile.Position.X][tile.Position.Y]?.SetPlayer(gameControl.GameState.Winner.Value);
                    yield return  new WaitForSeconds(0.5f);
                }
            }
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
            for (var iCol = 0; iCol < gameBoard.columns; iCol++)
            {
                var columnPosition = iCol * environmentObjects.columnHeight;
                var piecesCol = new List<GamePiece>();
                for (var iRow = 0; iRow < gameBoard.rows; iRow++)
                {
                    var rowPosition = iRow * environmentObjects.rowWidth;
                    var gamePiece = Instantiate(environmentObjects.gamePiece);
                    gamePiece.Setup(iCol, iRow, HandlePieceInteraction);
                    gamePiece.transform.position = new Vector3(rowPosition, columnPosition, 0f);
                    piecesCol.Add(gamePiece);
                    _spawnedAssets.Add(gamePiece.gameObject);
                }
                _boardPieces.Add(piecesCol);
            }
        }

        private void HandlePieceInteraction(int col, int row, GamePiece piece)
        {
            switch (appControl.CurrentAppState)
            {
                case AppStates.Game when gameControl.PlayerInput(col, row, out var currentPlayer):
                    piece.SetPlayer(currentPlayer);
                    break;
                case AppStates.GameResults:
                    piece.RejectInput();
                    StartCoroutine(EndGameRoutine());
                    break;
                default:
                    piece.RejectInput();
                    break;
            }
        }

        private IEnumerator EndGameRoutine()
        {
            yield return AnimateSetBoard();
            
            appControl.CurrentAppState = AppStates.MainMenu;
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
