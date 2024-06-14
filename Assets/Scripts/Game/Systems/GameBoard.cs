using Game.Components;
using UnityEngine;

namespace Game.Systems
{
    public class GameBoard
    {
        public static void GameBoardSetup(Board board)
        {
            var map = new Map(board.columns, board.rows, -1);
            
        }
    }
}
