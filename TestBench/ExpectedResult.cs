using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBench
{
    public struct ExpectedResult
    {
        public int problemNumber { get; set; }
        public string solution { get; set; }
        public TimeSpan maxDuration { get; set; }
    }
}
