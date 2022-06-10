//#define VERBOSEOUTPUT
using System.Numerics;

namespace EulerProblems.Lib.Problems
{
	public class Euler0063 : Euler
	{
		public Euler0063() : base()
		{
			title = "Powerful digit counts";
			problemNumber = 63;
		}
		protected override void Run()
		{
			/*
			 * this is another one I solved in a spreadsheet first. just plain
			 * old manual spreadsheet work. I incremented n and exp columns 
			 * until n^exp had more digits than exp. I counted all the rows 
			 * where n^exp had the exact numbers and that gave me the answer.
			 * 
			 * the wrong answer. I forgot to count the digits to the power of 
			 * 1. And then I counted wrong. Bottom line, I knew I had the right
			 * technique, just kept manually screwing up. So I took that same
			 * logic that I was manually doing wrong, and wrote the code below.
			 * 
			 * */
            HashSet<BigInteger> answers = new HashSet<BigInteger>();
			for(int exp = 1; true; exp++)
            {
				bool hasFoundAnswerForThisExp = false;
				for(int n = 1; true; n++)
                {
					BigInteger r = BigInteger.Pow(n, exp);
					int numDigits = r.ToString().Length;
					if(numDigits == exp)
                    {
						hasFoundAnswerForThisExp = true;
						if(!answers.Contains(r))
                        {
							answers.Add(r);
#if VERBOSEOUTPUT
							Console.WriteLine("{0} is {1} to the power of {2}", r, n, exp);
#endif
							continue;
                        }
					}
					if(numDigits > exp)
                    {
						break;
                    }
                }
				if(!hasFoundAnswerForThisExp)
                {
					// the numbers grow too fast from here on, so kill it
					int answer = answers.Count();
					PrintSolution(answer.ToString());
					return;
				}
            }
			
		}
	}
}
