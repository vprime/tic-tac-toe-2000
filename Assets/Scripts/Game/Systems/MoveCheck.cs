using System.Collections.Generic;
using Game.Components;
using UnityEngine;

namespace Game.Systems
{
    public static class MoveCheck
    {
        public static bool CheckEndGame(GameState state)
        {
            if (CheckWin(state.CurrentMap, out var winner, out _))
            {
                Debug.Log($"Winner is player {(PlayerSymbol)winner}!");
                return true;
            }

            if (!CheckGameover(state.CurrentMap, state.Round)) return false;
            // Game over!
            Debug.Log("It was a draw!");
            return true;

        }

        private static bool CheckGameover(Map map, int round)
        {
            return map.Area <= round;
        }

        public static bool CheckWin(Map map, out int winningPlayer, out List<MapTile> winningTiles)
        {
            var pathsToTest = new List<List<MapTile>>();
            
            // Check each row for win
            for (var i = 0; i < map.Rows; i++)
            {
                pathsToTest.Add(map.Row(i));
            }

            // Check each column for a win
            for (var i = 0; i < map.Columns; i++)
            {
                pathsToTest.Add(map.Column(i));
            }

            // Check Positive Diagonal for win
            pathsToTest.Add(map.Diagonal(1));

            // Check Negative Diagonal for win
            pathsToTest.Add(map.Diagonal(-1));

            // Loop through the list
            foreach(var path in pathsToTest)
            {
                // Test for a winner
                var result = WinnerInList(path, map.DefaultData);
                if (result == map.DefaultData) continue;
                // Return the winner's ID and the winning tiles
                winningPlayer = result;
                winningTiles = path;
                return true;
            }
            // No wins, return null tiles and default number
            winningTiles = null;
            winningPlayer = map.DefaultData;
            return false;
        }

        /// <summary>
        /// Check the list to see if it's a winning row.
        /// </summary>
        /// <param name="positions">List of player indexes from a row</param>
        /// <param name="empty">Empty value</param>
        /// <returns>The winner's index, or default value</returns>
        private static int WinnerInList(List<MapTile> positions, int empty)
        {
            var first = positions[0].Value;
            if (first == empty)
                return empty;
            
            for (var i = 1; i < positions.Count; i++)
            {
                if (positions[i].Value != first)
                    return empty;
            }
            return first;
        }

        
    }
}
