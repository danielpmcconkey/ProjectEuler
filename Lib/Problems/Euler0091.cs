//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0091 : Euler
	{
		public Euler0091() : base()
		{
			title = "Right triangles with integer coordinates";
			problemNumber = 91;
		}
		protected override void Run()
		{
            /*
             * This was anther fun one for me. Probably only because the Euler 
             * guys stopped at 50 x 50. If I had to optimize for a solution of 
             * 1000 x 1000, I'd have probably figured it out and been super 
             * proud of myself...eventually. Instead, I got to brute force this
             * one by creating a matrix of every possible coordinate, then 
             * cross joining in to itself to create a matrix of every possible 
             * P and Q point and just check each for "rightness".
             * 
             * Does this check every triangle twice? You betcha. Is it exactly 
             * twice? Uh huh. Can you just divide by 2? Oh yeah, baby.
             * 
             * And if I *did* have to optimize it? Well, I'd probably have 
             * implemented a system to not check obviously wrong triangles. 
             * Example: if I have an obtuse triangle, there's no sense in 
             * checking points that would make a more obtuse triangle. I'd 
             * probably also implement a means to prevent checking the same
             * triangle twice (when T1's P is T2' Q and vice versa).
             * 
             * */

            Func<TrianglePoints, bool> isRight = (t) =>
            {
                var qx2 = Math.Pow(t.q.x, 2);
                var qy2 = Math.Pow(t.q.y, 2);
                var px2 = Math.Pow(t.p.x, 2);
                var py2 = Math.Pow(t.p.y, 2);
                var qyMinusPy2 = Math.Pow(Math.Abs(t.q.y - t.p.y), 2);
                var qxMinusPx2 = Math.Pow(Math.Abs(t.q.x - t.p.x), 2);
                var OQ = (t.q.y == 0) ? qx2 : qy2 + qx2; // the actual length would be the square root, but this makes the comparison easier
                var OP = (t.p.x == 0) ? py2 : py2 + px2;
                var PQ = (t.p.x == t.q.x) ? qyMinusPy2 :
                    (t.p.y == t.q.y) ? qxMinusPx2 : qyMinusPy2 + qxMinusPx2;
                if (OQ + OP == PQ) return true;
                if (OP + PQ == OQ) return true;
                if (OQ + PQ == OP) return true;
                return false;
            };
            const int sideLength = 50;
            List<xyCoordinate> coordinates = new List<xyCoordinate>();
            for(int x = 0; x <= sideLength; x++)
            {
                for(int y = 0; y <= sideLength; y++)
                {
                    if (x == 0 && y == 0) continue;
                    coordinates.Add(new xyCoordinate(x, y));
                }
            }
            List<TrianglePoints> triangles = new List<TrianglePoints>();
            var o = new xyCoordinate(0, 0);
            for(int i = 0; i < coordinates.Count; i++)
            {
                var p = coordinates[i];
                for(int j = 0; j < coordinates.Count; j++)
                {
                    if (j == i) continue;
                    var q = coordinates[j];
                    triangles.Add(new TrianglePoints(o, p, q));
                }
            }
			int answer = 0;
            for(int i = 0; i < triangles.Count; i++)
            {
                var t = triangles[i];
                if (isRight(t)) answer++;
            }
            answer /= 2; // the above algorithm checks everything twice
			PrintSolution(answer.ToString());
			return;
		}
	}
}
