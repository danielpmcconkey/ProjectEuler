//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
    public class Euler0080 : Euler
    {
        public Euler0080() : base()
        {
            title = "Square root digital expansion";
            problemNumber = 80;
        }
        protected override void Run()
        {
            /*
             * This one was both rewarding and frustrating for me. Rewarding 
             * because the approach I first decided on turned out to be an
             * excellent one that came awfully close to working first try. 
             * Frustrating because the internet is a damned dirty stupid place.
             * 
             * My approach was to leverage what we did in problem 64, where we 
             * first get a continued fraction of a square root. All I had to do
             * was figure out how to translate a continued fraction into a 
             * decimal expansion. That's where the internet failed me. 
             * 
             * I very quickly found this page:
             * 
             * https://crypto.stanford.edu/pbc/notes/contfrac/convert.html
             * 
             * But the instruction on that page is so horrible that I tried 
             * many times to understand it. I kept searching and found nothing.
             * Eventually, I stared at that web page long enough to notice that
             * it was the floor value shared by the last two convergent 
             * fractions that represented your next decimal digit.
             * 
             * So, basically, if you have a continued fraction that is long 
             * enough* take the convergent fraction using the entire 
             * coefficients array (labelled as "ultimate" in the code) and then
             * take the convergent fraction using all but the last 
             * coefficients, ("penultimate").
             * 
             * Actually divide the numerators by the denominators of those 
             * convergent fractions and take the floor of that division. That
             * floor value is your next digit. Now subtract that floor value 
             * from each fraction and multiply each by 10. Loop it again.
             * 
             * As long as your floor values match between your penultimate and
             * ultimate convergent fractions, that matched floor is your next 
             * digit. Once the two don't match, you've reached the end of your
             * precision. This works remarkably well.
             * 
             * The only problem with this technique is that I don't know how 
             * many coefficients of my continued fraction I'll need to yield
             * 100 digits of precision in my expansion. Trial and error showed
             * me that, for the first 100 natural numbers, the answer is that 
             * you need a coefficient array with a length of somewhere between
             * 1.7 and 1.75 x the number of digit precision. I later confirmed 
             * that my 1.75x pad is good for solving this problem at least up 
             * to summing the first 10,000 decimals of each irrational square
             * root.
             * 
             * Last comment. I don't like the wording in the problem statement:
             * 
             *    "...find the total of the digital sums of the first one 
             *    hundred decimal digits"
             *    
             * While, yes, it is technically accurate to refer to the digits to
             * the left of the decimal point as "decimal digits", it is also
             * not perfectly clear. At least in american culture. Fortunately,
             * the problem authors gave us the example to check our work. 
             * That's how I noticed that my thinking was wrong to only sum 
             * numbers *after* the decimal point. 
             * 
             * Not a huge frustration though. Just unfortunate.
             * 
             * The below code runs in under 50 milliseconds.
             * 
             * */
            const int numDecimalsToCount = 100;
            const int maxN = 100;

            int sum = 0;

            for(int n = 2; n <= maxN; n++)
            {
                if(CommonAlgorithms.IsPerfectSquare(n)) continue;

                ContinuedFraction sqrt = CommonAlgorithms.GetContinuedFractionOfSquareRootOfN(n);
                int[] decimals = FractionCalculator.GetContinuedFractionDecimalExpansion(
                    sqrt, numDecimalsToCount);
                var decimalsToCount = decimals[0..numDecimalsToCount];
                //Console.WriteLine(string.Join("", decimalsToCount));
                sum += decimalsToCount.Sum();
            }

            
            PrintSolution(sum.ToString());
            return;
        }
        
    }
}
