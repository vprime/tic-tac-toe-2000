using System;
using System.Collections.Generic;

namespace Game.Components
{
    /// <summary>
    /// Game board map container
    /// </summary>
    public class Map : List<List<int>>
    {
        /// <summary>
        /// Rows in the game map. Aka "Y".
        /// </summary>
        public int Rows { get; }

        /// <summary>
        /// Columns in the game map. Aka "X"
        /// </summary>
        public int Columns { get; }

        /// <summary>
        /// Value used when tile is empty or unset.
        /// </summary>
        public int DefaultData { get; }

        /// <summary>
        /// Rows * Columns
        /// </summary>
        public int Area => Rows * Columns;
        
        /// <summary>
        /// Event triggered when a map tile is updated
        /// </summary>
        public event Action<MapTile> OnTileUpdate;

        /// <summary>
        /// Construct a game map container.
        /// </summary>
        /// <param name="rows">How tall is the game board? Y value</param>
        /// <param name="columns">How wide is the game board? X value</param>
        /// <param name="defaultData">What is the data for empty cells? (Default: -1)</param>
        public Map(int rows, int columns, int defaultData = -1)
        {
            Rows = rows;
            Columns = columns;
            DefaultData = defaultData;
            
            // Generate Columns
            for (var iColumn = 0; iColumn < columns; iColumn++)
            {
                var column = new List<int>();
                // Generate Rows
                for (var iRow = 0; iRow < rows; iRow++)
                {
                    column.Add(defaultData);
                }
                Add(column);
            }
        }

        /// <summary>
        /// Check if input is within bounds and the location is empty.
        /// </summary>
        /// <param name="x">Column position</param>
        /// <param name="y">Row position</param>
        /// <returns>True if the position is valid.</returns>
        public bool ValidateMove(int x, int y)
        {
            // Bounds Check
            if (x > Count - 1 || x < 0)
                return false;
            if (y > Count - 1 || y < 0)
                return false;

            // Empty check
            return this[x][y] == DefaultData;
        }
        
        /// <summary>
        /// Sets a tile in the game board. Triggers <see cref="OnTileUpdate"/>
        /// </summary>
        /// <param name="x">Column position</param>
        /// <param name="y">Row position</param>
        /// <param name="value">Updated value</param>
        public void Set(int x, int y, int value)
        {
            this[x][y] = value;
            OnTileUpdate?.Invoke(new MapTile(x, y, value));
        }
        
        /// <summary>
        /// List a row of tiles.
        /// </summary>
        /// <param name="index">Position of requested row</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="MapTile"/> contents for given row</returns>
        public List<MapTile> Row(int index)
        {
            var response = new List<MapTile>();
            for(var iCol=0; iCol < Count; iCol++)
            {
                response.Add(new MapTile(iCol, index, this[iCol][index]));
            }
            return response;
        }

        /// <summary>
        /// List a column of tiles.
        /// </summary>
        /// <param name="index">Positoin of requested column</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="MapTile"/> contents for given column</returns>
        public List<MapTile> Column(int index)
        {
            var response = new List<MapTile>();
            for (var iRow = 0; iRow < this[index].Count; iRow++)
            {
                response.Add(new MapTile(index, iRow, this[index][iRow]));
            }
            return response;
        }

        /// <summary>
        /// Lists a diagonal of tiles.
        /// </summary>
        /// <param name="direction">Movement along row per column. 1 = top lef to bottom right -1 = top right to bottom left</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="MapTile"/> contents for given direction</returns>
        public List<MapTile> Diagonal(int direction)
        {
            var response = new List<MapTile>();
            var rowIndex = 0;
            if (direction < 0)
                rowIndex = this[0].Count - 1;
            for (var colIndex = 0; colIndex < Count; colIndex++)
            {
                response.Add(new MapTile(colIndex, rowIndex, this[colIndex][rowIndex]));
                rowIndex += direction;
            }
            return response;
        }

        /// <summary>
        /// Provides a list of open positions
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="MapTile"/> that are currently <see cref="DefaultData"/>.</returns>
        public List<MapTile> OpenPositions()
        {
            var response = new List<MapTile>();
            for (var iCol = 0; iCol < Columns; iCol++)
            {
                for (var iRow = 0; iRow < Rows; iRow++)
                {
                    if (this[iCol][iRow] == DefaultData)
                        response.Add(new MapTile(iCol, iRow, DefaultData));
                }
            }
            return response;
        }
    }
}
