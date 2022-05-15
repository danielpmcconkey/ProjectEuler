using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib
{
    internal static class GridHelper
    {
        public static int GetGridOrdinalFromPosition(int gridWidth, int row, int column)
        {
            return (row * gridWidth) + column;
        }
    }
}
