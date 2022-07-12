using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib
{
    internal struct Triangle
    {
        public int a;
        public int b;
        public int c;
        public int perimeter;
    }
    internal struct TriangleLong
    {
        public long a;
        public long b;
        public long c;
        public long perimeter;
    }
    internal struct TrianglePoints
    {
        public xyCoordinate o;
        public xyCoordinate p;
        public xyCoordinate q;

        internal TrianglePoints(xyCoordinate o, xyCoordinate p, xyCoordinate q)
        {
            this.o = o;
            this.p = p;
            this.q = q;
        }
    }
}
