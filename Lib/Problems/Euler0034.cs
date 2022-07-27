using EulerProblems.Lib;
using System.Text;
using System.Text.RegularExpressions;

namespace EulerProblems.Lib.Problems
{
	public class Euler0034 : Euler
	{
		private int[] _factorials;
		public Euler0034() : base()
		{
			title = "Digit factorials";
			problemNumber = 34;
			
		}
		protected override void Run()
		{
			_factorials = new int[10];
			AssignFactorials();
			int min = 3;
			/* 
			 * how big should I check? 9! = 362880, which has 6 
			 * digits. so anything bigger than 10^7 would be
			 * impossible. 999,999 would be 6 * (9!) or 2,177,280
			 * but 7 * (9!) would be only 2,540,160.
			 * */
			int max = 7 * (int)CommonAlgorithms.GetFactorial(9);
			
			int answer = 0;
			for(int i = min; i <= max; i++)
            {
				BigNumber bn = new BigNumber(i); // just an easy way to put the digits into an array
				int sumOfTheseDigits = 0;
				foreach(int digit in bn.digits)
                {
					sumOfTheseDigits += _factorials[digit];
                }
				if(sumOfTheseDigits == i)
                {
					answer += i;
                }
            }
			PrintSolution(answer.ToString());
			return;
		}
		
		/// <summary>
		/// I wrote this function to use as a lookup table
		/// so I didn't have to calculate factorials on the
		/// fly over and over
		/// </summary>
		private void AssignFactorials()
        {
			for(int i = 0; i < 10; i++)
            {
				_factorials[i] = (int)CommonAlgorithms.GetFactorial(i);
            }
        }



	}
}
