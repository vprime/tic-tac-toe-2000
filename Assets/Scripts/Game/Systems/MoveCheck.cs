using System.Collections.Generic;
using System.Linq;
using Game.Components;

namespace Game.Systems
{
    public class MoveCheck
    {
        public void CheckEndGame(ref GameState state)
        {
            if (CheckGameover(state.CurrentMap, state.Round))
            {
                // Game over!
                if (CheckWin(state.CurrentMap, out int winner, out List<MapTile> tiles))
                {
                    
                }
            }
        }

        public static bool CheckLegal(Move move, Map map)
        {
            return map.ValidateMove(move.Position.X, move.Position.Y);
        }

        public static bool CheckGameover(Map map, int round)
        {
            return map.Area < round;
        }

        public static bool CheckWin(Map map, out int winningPlayer, out List<MapTile> winningTiles)
        {
            List<List<MapTile>> pathsToTest = new List<List<MapTile>>();
            
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
            foreach(List<MapTile> path in pathsToTest)
            {
                // Test for a winner
                int result = WinnerInList(path, map.DefaultData);
                if (result != map.DefaultData)
                {
                    // Return the winner's ID and the winning tiles
                    winningPlayer = result;
                    winningTiles = path;
                    return true;
                }
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
        /// <returns>The winner's index, or default value</returns>
        static int WinnerInList(List<MapTile> positions, int empty)
        {
            int first = positions[0].Value;
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
