//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0081 : Euler
	{
        const string filePath = @"E:\ProjectEuler\ExternalFiles\p081_matrix.txt";
        public Euler0081() : base()
		{
			title = "Path sum: two ways";
			problemNumber = 81;
		}
        protected override void Run()
        {
            /* 
             * This was quite easy. I immediately saw that it was the same 
             * problem as #67, just rotated 45 degrees. In fact, I'd originally
             * intended to convert it to a pyramid using int.max to fill in any
             * "missing" values, then reuse the code from 67. But, while doing 
             * so, I decided that I could fairly easily create a means to 
             * traverse a matrix in the same way as the pyramid. The hardest
             * part was debugging, when I went through every cell in the 5 x 5
             * matrix and kept refining how the program crawled it.
             * 
             * Once I got the 5 x 5 working, it was litterally just uncomment
             * the code that reads from the file, comment the 5 x 5 input, and
             * press play. It worked perfectly first time.
             * 
             * */

            List<List<int>> intRows = new List<List<int>>();
            // read the text file and put the values into intRows
            var lines = File.ReadLines(filePath);
            //string[] lines = {
            //    "131,673,234,103,18",
            //    "201,96,342,965,150",
            //    "630,803,746,422,111",
            //    "537,699,497,121,956",
            //    "805,732,524,37,331" };
            foreach (string row in lines)
            {
                string rowTrimmed = row.Trim();
                if (rowTrimmed.Length > 1)
                {
                    string[] intsAsStrings = rowTrimmed.Split(',');
                    List<int> rowOfInts = new List<int>();
                    foreach (var intAsString in intsAsStrings)
                    {
                        rowOfInts.Add(Int16.Parse(intAsString));
                    }
                    intRows.Add(rowOfInts);
                }
            }

            var numRows = intRows.Count;
            var numColumns = intRows[0].Count;
            
            Func<(int x, int y), (int x, int y)?> whatsRight = (t) =>
            {
                if (t.y == 0) return null;
                if(t.x >= numColumns - 1) return null;
                return (t.x + 1, t.y - 1);
            };
            Func<(int x, int y), (int x, int y)?> whatsLowerRight = (t) =>
            {
                if (t.x >= numColumns - 1) return null;
                return (t.x + 1, t.y);
            };
            Func<(int x, int y), (int x, int y)?> whatsLowerLeft = (t) =>
            {
                if (t.y >= numRows - 1) return null;
                return (t.x, t.y + 1);
            };
            var x = numRows - 2;
            var y = numColumns - 1;
            var priorRowStart = x;
            while(true)
            {
                if (y == -1) break;
                var r = whatsRight((x, y));
                var ll = whatsLowerLeft((x, y));
                var lr = whatsLowerRight((x, y));
                var thisVal = intRows[y][x];
                var llVal = ll == null ? int.MaxValue : intRows[ll.Value.y][ll.Value.x];
                var lrVal = lr == null ? int.MaxValue : intRows[lr.Value.y][lr.Value.x];

                // update this row by adding the lesser between ll and lr
                int newVal = thisVal + Math.Min(llVal, lrVal);
                intRows[y][x] = newVal;

                // move to the next cell
                if(r == null)
                {
                    // do y before x because y val keeys off old x val
                    y = (y == 0) ? x - 1 : numColumns - 1;
                    if (priorRowStart == 0) x = 0;
                    else
                    {
                        x = priorRowStart - 1;
                        priorRowStart = x;
                    }
                }
                else
                {
                    x += 1;
                    y -= 1;
                }
            }


            PrintSolution(intRows[0][0].ToString());
            return;
        }
    }
}
