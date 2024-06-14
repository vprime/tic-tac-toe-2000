using UnityEngine;

namespace Game.Components
{
    [CreateAssetMenu(fileName = "DEFAULT - Board", menuName = "Game/Components/Board")]
    public class Board : ScriptableObject
    {
        /// <summary>
        /// Columns on a board (X Axis)
        /// </summary>
        public int columns = 3;

        /// <summary>
        /// Rows on a board (Y Axis)
        /// </summary>
        public int rows = 3;
    }
}
