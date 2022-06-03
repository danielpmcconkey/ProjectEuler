//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0055 : Euler
	{
		public Euler0055() : base()
		{
			title = "Lychrel numbers";
			problemNumber = 55;
		}
		protected override void Run()
		{
			int limit = 10000;
			int cycleLimit = 50;
			int howManyLychrels = 0;
			for(int i = 1; i < limit; i++)
            {
				BigNumber currentVal = new BigNumber(i);
				bool isPotentialLychrel = true;
				for(int j = 1; isPotentialLychrel && j < cycleLimit; j++)
                {
					BigNumber reverseVal = GetReverse(currentVal);
					var sum = BigNumberCalculator.Add(currentVal, reverseVal);
					if(CommonAlgorithms.IsIntPalindromic(sum))
                    {
						isPotentialLychrel = false;
                    }
					currentVal = sum;
                }
				if (isPotentialLychrel)
				{
					howManyLychrels++;
				}
            }
			PrintSolution(howManyLychrels.ToString());
			return;
		}
		private BigNumber GetReverse(BigNumber n)
        {
			int length = n.digits.Length;
			int[] digits = new int[length]; 
			Array.Copy(n.digits, digits, length);
			for(int i = 0; i < length; i++)
            {
				digits[i] = n.digits[length - 1 - i];
            }
			return new BigNumber(digits);
        }
	}
}
