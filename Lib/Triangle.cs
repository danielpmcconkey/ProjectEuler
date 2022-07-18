using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib
{
    public struct Triangle
    {
        public int a;
        public int b;
        public int c;
        public int perimeter;
    }
    public struct TriangleLong
    {
        public long a;
        public long b;
        public long c;
        public long perimeter;
    }
    public struct TrianglePoints
    {
        public xyCoordinate o;
        public xyCoordinate p;
        public xyCoordinate q;

        public TrianglePoints(xyCoordinate o, xyCoordinate p, xyCoordinate q)
        {
            this.o = o;
            this.p = p;
            this.q = q;
        }
    }
}
