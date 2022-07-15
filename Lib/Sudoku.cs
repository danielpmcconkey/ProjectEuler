using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib
{
    public class InvalidConfigurationException : Exception { }
    public class SudokuPoint
    {
        public int number;
        //public xyCoordinate position;
        public bool[] exclusions;
        public SudokuPoint(int number)
        {
            this.number = number;
            exclusions = new bool[10];  // ignore the 0th position
        }
        public bool AddExclusion(int ex)
        {
            bool didProgress = false;
            if (number == 0 && exclusions[ex] == false)
            {
                exclusions[ex] = true;
                if (exclusions.Where(x => x == true).Count() == 8)
                {
                    for (int i = 1; i < 10; i++)
                    {
                        if (exclusions[i] == false)
                        {
                            number = i;
                            didProgress = true;
                        }
                    }
                }
            }
            return didProgress;
        }
    }
    public class Sudoku
    {
        public SudokuPoint[] points;
        public int numTurns;
        public int numGuesses;

        public Sudoku()
        {
            points = new SudokuPoint[81];
        }
        public void AddPoint(int x, int y, int number)
        {
            var ordinal = CommonAlgorithms.GetGridOrdinalFromPosition(9, y, x);
            points[ordinal] = new SudokuPoint(number);
        }
        public void PrintTable(bool withOptions=false)
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < 9; row++)
            {
                sb.AppendLine();
                for (int column = 0; column < 9; column++)
                {
                    var position = (row * 9) + column;
                    string value = points[position].number.ToString();
                    if(withOptions && value == "0")
                    {
                        value = "";
                        for(int i = 1; i < 10; i++)
                        {
                            if (points[position].exclusions[i] == false) value += i.ToString() + ",";
                        }
                    }

                    sb.Append(string.Format("{0}|", value));
                }
            }
            Console.WriteLine(sb.ToString());
            Console.WriteLine();
        }
        public (bool isSolved, int EulerSolution) Solve()
        {
            while(true)
            {
                if (points.Where(x => x.number == 0).Any() == false)
                {
                    var solution = (100 * points[0].number) + (10 * points[1].number) + points[2].number;
                    return (true, solution);
                }
                try
                {
                    bool didProgress = TakeTurn();
                    if (!didProgress)
                    {
                        Guess();
                    }
                }
                catch (InvalidConfigurationException)
                {
                    return (false, 0);
                }
            }
        }
        private bool CheckBoxValidity(int number, int ordinal)
        {
            var boxStart = GetBoxStart(ordinal);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var thisOrdinal = boxStart + j + (i * 9);
                    if (thisOrdinal != ordinal && points[thisOrdinal].number == number)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool CheckColumnValidity(int number, int ordinal)
        {
            var columnStart = GetColumnStart(ordinal);
            var columnEnd = columnStart + 72;
            for (int i = columnStart; i <= columnEnd; i += 9)
            {
                if (i != ordinal && points[i].number == number)
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckRowValidity (int number, int ordinal)
        {
            var rowStart = GetRowStart(ordinal);
            var rowEnd = rowStart + 9;
            for (int i = rowStart; i < rowEnd; i++)
            {
                if (i != ordinal && points[i].number == number)
                {
                    return false;
                }
            }
            return true;
        }
        private SudokuPoint[] ClonePoints()
        {
            SudokuPoint[] clones = new SudokuPoint[points.Length];
            for(int i = 0; i < points.Length; i++)
            {
                var oldPoint = points[i];
                var newPoint = new SudokuPoint(oldPoint.number);
                for(int j = 0; j < 10; j++)
                {
                    newPoint.exclusions[j] = oldPoint.exclusions[j];
                }
                clones[i] = newPoint;
            }
            return clones;
        }
        private bool ExcludeNumberFromColumn(int number, int ordinal)
        {
            bool didProgress = false;
            var columnStart = GetColumnStart(ordinal);
            var columnEnd = columnStart + 72;
            for (int i = columnStart; i <= columnEnd; i += 9)
            {
                if(points[i].AddExclusion(number))didProgress = true;
            }
            return didProgress;
        }
        private bool ExcludeNumberFromRow(int number, int ordinal)
        {
            bool didProgress = false;
            var rowStart = GetRowStart(ordinal);
            var rowEnd = rowStart + 9;
            for(int i = rowStart; i < rowEnd; i++)
            {
                if(points[i].AddExclusion(number)) didProgress = true;
            }
            return didProgress;
        }
        private bool ExcludeNumberFromBox(int number, int ordinal)
        {
            bool didProgress = false;
            var boxStart = GetBoxStart(ordinal);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var thisOrdinal = boxStart + j + (i * 9);
                    if(points[thisOrdinal].AddExclusion(number))didProgress = true;
                }
            }
            return didProgress;
        }
        private int GetBoxStart(int ordinal)
        {
            var column = GetColumnStart(ordinal);
            var boxStartY = (int)(Math.Floor(column / 3D)) * 3;
            var boxStartX = (int)(Math.Floor(ordinal / 27D)) * 27;
            return boxStartX + boxStartY;
        }
        private int GetColumnStart(int ordinal)
        {
            return ordinal % 9;
        }
        private int GetRowStart(int ordinal)
        {
            return (int)(Math.Floor(ordinal / 9D)) * 9;
        }
        private bool Guess()
        {
            numGuesses++;
            // pick a point and try every combination
            for(int i = 0; i < 81; i++)
            {
                if(points[i].number == 0)
                {
                    List<int> options = new List<int>();
                    for(int j = 1; j < 10; j++)
                    {
                        if (points[i].exclusions[j] == false) options.Add(j);
                    }
                    foreach(var o in options)
                    {
                        var clonePoints = ClonePoints();
                        clonePoints[i].number = o;
                        Sudoku clonePuzzle = new Sudoku();
                        clonePuzzle.points = clonePoints;
                        var solution = clonePuzzle.Solve();
                        if (solution.isSolved)
                        {
                            points = clonePuzzle.points;
                            numGuesses += clonePuzzle.numGuesses;
                            numTurns += clonePuzzle.numTurns;
                            return true;
                        }
                    }
                    throw new InvalidConfigurationException();
                }
            }
            throw new InvalidConfigurationException();
        }
        private bool IsValid()
        {            
            for(int i = 0; i < 81; i++)
            {
                if (points[i].number > 0)
                {
                    if (CheckRowValidity(points[i].number, i) == false) return false;
                    if (CheckColumnValidity(points[i].number, i) == false) return false;
                    if (CheckBoxValidity(points[i].number, i) == false) return false;
                }
            }
            return true;
        }
        private bool TakeTurn()
        {
            bool didProgress = UpdateExclusions();
            if (!IsValid())
            {
                throw new InvalidConfigurationException();
            }
            numTurns++;
            return didProgress;
        }
        private bool UpdateExclusions()
        {
            bool didProgress = false;
            for(int i = 0; i < 81; i++)
            {
                var point = points[i];
                if(point.number > 0)
                {
                    if(ExcludeNumberFromRow(point.number, i)) didProgress = true;
                    if(ExcludeNumberFromColumn(point.number, i)) didProgress = true;
                    if(ExcludeNumberFromBox(point.number, i)) didProgress = true;
                }
            }
            return didProgress;
        }
    }
}
