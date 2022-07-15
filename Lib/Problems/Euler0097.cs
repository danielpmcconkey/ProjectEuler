//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0097 : Euler
	{
		public Euler0097() : base()
		{
			title = "Large non-Mersenne prime";
			problemNumber = 97;
		}
        protected override void Run() 
        {
            /* 
             * This one was not hard. At first glance, it seemed daunting, 
             * though I distinctly remembering reading in previous problem
             * threads that there was a problem where others only worked on
             * the first N digits. So that quickly sprang to my mind and I set
             * about rapidly implementing a long-hand multiplication mechanism
             * that used an array of 10 digits and just kept manually 
             * multiplying by 2 grade-school style.
             * 
             * That worked first try. Reading the problem thread showed me the
             * much easier mod % 10^10 approach that I implemented in the 
             * Run_fast() method.
             * 
             * */

            //Run_slow(); // Elapsed time: 1468.0764 milliseconds
            Run_fast(); // Elapsed time: 101.1507 milliseconds
        }
        private void Run_fast()
        {
            const int numDigits = 10;
            const int exponent = 7830457;
            const int baseNum = 2;
            const int start = 28433;
            const int modifier = 1;

            long answer = start;
            for (int i = 0; i < exponent; i++)
            {
                answer = (answer * 2) % (long)1e10;

            }
            answer += modifier;
            PrintSolution(answer.ToString());
            return;
        }
        private void Run_slow()
		{
            const int numDigits = 10;
            const int exponent = 7830457;
            const int baseNum = 2;
            const int start = 28433;
            const int modifier = 1;

            

            var chars = start.ToString().PadLeft(numDigits,'0').ToCharArray();
            int[] nums = new int[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                nums[i] = (int)chars[i] - (int)'0';
            }
#if VERBOSEOUTPUT
            Console.WriteLine(string.Join("", nums)); 
#endif
            for (int i = 0; i < exponent; i++)
            {
                int[] carries = new int[numDigits];
                int[] newNums = new int[numDigits];
                for(int j = numDigits - 1; j >= 0; j--)
                {
                    var multipliedDigit = nums[j] * baseNum;
                    multipliedDigit += carries[j];
                    var placeDigit = multipliedDigit % 10;
                    var carryDigit = (int)Math.Floor(multipliedDigit / 10D);
                    newNums[j] = placeDigit;
                    if(j - 1 >= 0) carries[j - 1] = carryDigit;
                }
                nums = newNums;
#if VERBOSEOUTPUT
                Console.WriteLine(string.Join("", nums)); 
#endif
            }
            var numsString = string.Join("", nums);
            long finalProduct = Int64.Parse(numsString);

            long answer = finalProduct + modifier;
			PrintSolution(answer.ToString());
			return;
		}
	}
}
