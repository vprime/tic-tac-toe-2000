using System.Collections.Generic;

namespace Game.Components
{
    public class Map : List<List<int>>
    {
        private readonly int _rows;
        private readonly int _columns;
        private readonly int _defaultData;
        public int Rows => _rows;
        public int Columns => _columns;
        public int DefaultData => _defaultData;
        
        public int Area => _rows * _columns;

        public Map(int rows, int columns, int defaultData)
        {
            _rows = rows;
            _columns = columns;
            _defaultData = defaultData;
            
            // Generate Columns
            for (var iColumn = 0; iColumn < columns; iColumn++)
            {
                List<int> column = new List<int>();
                // Generate Rows
                for (var iRow = 0; iRow < rows; iRow++)
                {
                    column.Add(defaultData);
                }
                Add(column);
            }
        }

        public bool ValidateMove(int x, int y)
        {
            // Bounds Check
            if (x > Count - 1 || x < 0)
                return false;
            if (y > Count - 1 || y < 0)
                return false;

            // Empty check
            if (this[x][y] == _defaultData)
                return true;
            return false;
        }
        

        public void Set(int x, int y, int value)
        {
            this[x][y] = value;
        }
        
        /// <summary>
        /// Traverse a row
        /// </summary>
        /// <param name="index">Row in Map</param>
        /// <returns>Map contents for given row</returns>
        public List<MapTile> Row(int index)
        {
            List<MapTile> response = new List<MapTile>();
            for(var iCol=0; iCol < Count; iCol++)
            {
                response.Add(new MapTile(iCol, index, this[iCol][index]));
            }
            return response;
        }

        /// <summary>
        /// Traverse a column
        /// </summary>
        /// <param name="index">Column in map</param>
        /// <returns>Map contents for given column</returns>
        public List<MapTile> Column(int index)
        {
            List<MapTile> response = new List<MapTile>();
            for (var iRow = 0; iRow < this[index].Count; iRow++)
            {
                response.Add(new MapTile(index, iRow, this[index][iRow]));
            }
            return response;
        }

        /// <summary>
        /// Traverse a diagnal.
        /// </summary>
        /// <param name="direction">Movement along row per column. 1 = top lef to bottom right -1 = top right to bottom left</param>
        /// <returns>Map contents for the given direction</returns>
        public List<MapTile> Diagonal(int direction)
        {
            List<MapTile> response = new List<MapTile>();
            int rowIndex = 0;
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
        /// <returns></returns>
        public List<MapTile> OpenPositions()
        {
            var response = new List<MapTile>();
            for (int iCol = 0; iCol < _columns; iCol++)
            {
                for (int iRow = 0; iRow < _rows; iRow++)
                {
                    if (this[iCol][iRow] != _defaultData)
                        response.Add(new MapTile(iCol, iRow, _defaultData));
                }
            }
            return response;
        }
    }
}
