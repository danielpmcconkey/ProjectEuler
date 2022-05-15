using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib
{
    internal static class MathHelper
    {
        internal static List<long> GetFactorsOfN(long n)
        {
            if (n <= 0) throw new ArgumentException("n must be greater than 0");
            
            List<long> factors = new List<long>();
            if(n == 1)
            {
                factors.Add(1);
                return factors;
            }

            long maxVal = (long)Math.Floor(n * .5); // no sense looking at anything above half
            long lowestOppositeFactor = n;

            for (long i = 1; i <= maxVal; i++)
            {
                if(i >= lowestOppositeFactor) return factors;
                if (n % i == 0)
                {
                    factors.Add(i);
                    // also add the opposite factor
                    long oppositeFactor = n / i;
                    factors.Add(oppositeFactor);
                    lowestOppositeFactor = oppositeFactor;
                }
            }
            return factors;
        }
    }
}
