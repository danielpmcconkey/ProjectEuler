using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
    internal class Euler0019 : Euler
    {
        public Euler0019() : base()
        {
            title = "Counting Sundays";
            problemNumber = 19;
            PrintTitle();
        }
        public override void Run()
        {
            var startDate = DateTime.Parse("Jan 1, 1901");  // inclusive
            var endDate = DateTime.Parse("Jan 1, 2001");    // exclusive
            var thisDate = startDate;
            int sundays = 0;
            while(thisDate < endDate)
            {
                if(thisDate.DayOfWeek == DayOfWeek.Sunday) sundays++;
                thisDate = thisDate.AddMonths(1);
            }

            PrintSolution(sundays.ToString());
            return;
        }

    }
}
