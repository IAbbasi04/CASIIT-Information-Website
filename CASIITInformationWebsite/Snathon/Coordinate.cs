using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasiitCourses
{
    // readable way to keep track of positions on the game table
    public class TableCoordinate
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public TableCoordinate(int row, int col) 
        {
            Row = row;
            Col = col;
        }
    }
}