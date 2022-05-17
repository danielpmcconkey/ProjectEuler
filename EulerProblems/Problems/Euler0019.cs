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
            // this is super cheeky, but might as well use your framework if you have it
            // I created the Run_lessCheeky() version to do this more in the spirit of 
            // the assignment

            var startDate = DateTime.Parse("Jan 1, 1901");  // inclusive
            var endDate = DateTime.Parse("Jan 1, 2001");    // exclusive
            
            List<DateTime> allDates = new List<DateTime>();

            var thisDate = startDate;
            while(thisDate < endDate)
            {
                allDates.Add(thisDate);
                thisDate = thisDate.AddDays(1);
            }

            int answer = allDates.Where(x => x.Day == 1 && x.DayOfWeek == DayOfWeek.Sunday).Count();

            PrintSolution(answer.ToString());
            return;
        }
        public void Run_lessCheeky()
        {
            int numberOfDays = 0;
            int numberOfYears = 100;
            numberOfDays += numberOfYears * 365;
            int numberOfLeapYears = 0;// (numberOfYears / 4) - 1; // minus 1 because 2000 wasn't a leap year
            for(int i = 1901; i <= 2000; i++)
            {
                if(i % 4 == 0 && i % 100 != 0) numberOfLeapYears++;
            }
            numberOfDays += numberOfLeapYears;

            // go through the days 7 at a time, starting on the first sunday
            // and add each day number to a list
            List<int> sundaysAndFirstDays = new List<int>();
            // when was the first sunday? if Jan 1, 1900 was a Monday, then
            // Jan 7, 1900 was a Sunday. if there were 365 days in 1900
            // (because it's on a century that isn't divisible by 400), then
            // Jan 1, 1900 was day -365 and Jan 7, 1900 was -359.
            int firstSunday = -359; 
            for(int i = firstSunday; i < numberOfDays; i += 7)
            {
                if(i >= 0) sundaysAndFirstDays.Add(i);  // any number lower than zero is prior to start date
            }

            // now go through month at a time and add each first day 
            // to the list
            int currentDay = 0;
            int currentYear = 1901;
            int currentMonth = 0;

            //                         j   f   m   a   m   j   j   a   s   o   n   d
            int[] daysInEachMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            while (currentDay < numberOfDays)
            {
                sundaysAndFirstDays.Add(currentDay);
                // how many days are in this month
                int daysToAdd = daysInEachMonth[currentMonth];
                // are we in February and in a leap year?
                if(currentMonth == 1 && currentYear % 4 == 0)
                {
                    daysToAdd++;
                }
                // now move the day/month/year stuff forward
                currentDay += daysToAdd;
                currentMonth++;
                if(currentMonth > 11)
                {
                    currentMonth = 0;
                    currentYear++;
                }                
            }
            // finally, count the days with more than one
            var duplicateCount = sundaysAndFirstDays
                .GroupBy(day => day)
                .Select(group => new
                {
                    day = group.Key,
                    count = group.Count()
                })
                .Where(row => row.count > 1)
                .Count();

            // print the output
            PrintSolution(duplicateCount.ToString());
            return;
        }
    }
}
