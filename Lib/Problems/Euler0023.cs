using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Lib.Problems
{
    public class Euler0023 : Euler
    {
        const long _maxValue = 28123;
        const long _smallestAbundantNumber = 12;
        private long[] _abundantNumbers;
        public Euler0023() : base()
        {
            title = "Non-abundant sums";
            problemNumber = 23;
            
        }
        protected override void Run()
        {
            _abundantNumbers = GetAllAbundantNumbers();

            /*
             * it takes forever to go through all the numbers
             * less than 28123 and test if it can't be created
             * by summing up 2 abundant numbers. Instead, let's
             * first see all the numbers that *can* by just
             * adding together all the abundant number pair
             * possibilities
             * 
             * so, first, create a cartesian join of the list 
             * and itself. that gives you all the possible pairs.
             * Then create a third list of all the sums and 
             * deduplicate.
             * 
             * Finally, check each positive integer againts the
             * deduplicated list and Bob's your uncle.
             * 
             * note: this still take about 3.5 seconds to run
             * so "fast" is relative
             * 
             * */

            var joinedList = from x in _abundantNumbers
                             from y in _abundantNumbers
                             where x + y <= _maxValue
                             select new { sum = x + y };
            // get distinct sums
            var deduped = joinedList.Distinct().ToList();
            

            // long countJoin = joinedList.Count();
            long countDistinct = deduped.Count();

            // create an array of bools (all false) of size countDistinct
            bool[] allIntsStatus = new bool[_maxValue + 1];
            // set the value to true for each of the numbers that can
            for(int i = 0; i < countDistinct; i++)
            {
                allIntsStatus[deduped[i].sum] = true;
            }

            // now get the answer by iterating through all positive integers
            // and adding those that show as false in allIntsStatus

            long answer = 0;
            for (long i = 1; i <= _maxValue; i++)
            {
                if (!allIntsStatus[i])
                {
#if DEBUG
                    Console.WriteLine(i.ToString());
#endif
                    answer += i;
                }
            }

            PrintSolution(answer.ToString());
            return;
        }
        private bool CanNBeWrittenAsSumOf2AbundantNumbers(long n)
        {
            /* 
             * this method is a poor-performing way of 
             * seeing if a number can be written as a 
             * sum of 2 abundant numbers. it is too slow
             * for the final problem, but it was useful
             * to run on low _maxValue values to see if
             * my fast version was accurate
             * 
             * */

            if(n < (_smallestAbundantNumber * 2)) return false;

            var abundantNumbersLessThanN = _abundantNumbers
                .Where(x => x < n)
                .OrderByDescending(y => y);

            foreach(var abundantNumber in abundantNumbersLessThanN)
            {
                var x = n - abundantNumber;
                if(abundantNumbersLessThanN.Contains(x)) return true;
            }
            return false;
        }
        private long[] GetAllAbundantNumbers()
        {
            List<long> abundantNumbers = new List<long>();

            for (long i = _smallestAbundantNumber; i <= _maxValue; i++)
            {
                if (GetSumOfDivisors(i) > i)
                {
                    abundantNumbers.Add(i);
                }
            }
            return abundantNumbers.ToArray();
        }
        private long GetSumOfDivisors(long n)
        {
            long[] divisors = CommonAlgorithms.GetProperDivisors(n);
            return divisors.Sum();
        }
        
    }
}
