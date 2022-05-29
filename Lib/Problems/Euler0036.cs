using EulerProblems.Lib;
using System.Text;
using System.Text.RegularExpressions;

namespace EulerProblems.Lib.Problems
{
	public class Euler0036 : Euler
	{
		public Euler0036() : base()
		{
			title = "Double-base palindromes";
			problemNumber = 36;
			
		}
		protected override void Run()
		{
			Run_elegant();			//  27.0825 milliseconds
			//Run_bruteForce();		// 162.2343 milliseconds
			//Run_stringReverse();	// 224.5428 milliseconds
		}
		public void Run_elegant()
		{
			/*
			 *  You shouldn't have to check for a base 10 palindrome if you build 
			 *  it as a palindrome at the outset. The problem is, this has 
			 *  slightly different rules depending on how many digits your 
			 *  palindrome has (between 1 and 6). So maybe this doesn't *look*
			 *  more elegant (it's a lot more code), but it does run faster (about
			 *  6x faster) and required a good bit more thinking.
			 *  
			 *  Also, as with the brute force method, we only have to check odd
			 *  numbers.
			 * 
			 * */


			
			int answer = 0;

			// all 1-digit numbers are palindromes
			for(int i = 1; i < 10; i += 2)
            {
				if (CommonAlgorithms.IsIntPalindromicInBinary(i))
				{
					answer += i;
				}
			}

			// the only 2-digit numbers that are palindromes are 
			// the multiples of 11
			for (int i = 11; i < 100; i += 11)
			{
				if (CommonAlgorithms.IsIntPalindromicInBinary(i))
				{
					answer += i;
				}
			}

			// for 3-digit numbers, your palindromes all follow the
			// pattern ABA
			for (int a = 1; a < 10; a += 2)
			{
				for (int b = 0; b < 10; b++)
				{
					int base10Palindrome = (a * 100) + (b * 10) + a;
					if (CommonAlgorithms.IsIntPalindromicInBinary(base10Palindrome))
					{
						answer += base10Palindrome;
					}
				}
			}

			// for 4-digit numbers, your palindromes all follow the
			// pattern ABBA
			for (int a = 1; a < 10; a += 2)
			{
				for (int b = 0; b < 10; b++)
				{
					int base10Palindrome = (a * 1000) + (b * 100) + (b * 10) + a;
					if (CommonAlgorithms.IsIntPalindromicInBinary(base10Palindrome))
					{
						answer += base10Palindrome;
					}
				}
			}
			// for 5-digit numbers, your palindromes all follow the
			// pattern ABCBA
			for (int a = 1; a < 10; a += 2)
			{
				for (int b = 0; b < 10; b++)
				{
					for (int c = 0; c < 10; c++)
					{
						int base10Palindrome = (a * 10000) + (b * 1000) + (c * 100) + (b * 10) + a;
						if (CommonAlgorithms.IsIntPalindromicInBinary(base10Palindrome))
						{
							answer += base10Palindrome;
						}
					}
				}
			}
			// for 6-digit numbers, your palindromes all follow the
			// pattern ABCCBA
			for (int a = 1; a < 10; a += 2)
			{
				for (int b = 0; b < 10; b++)
				{
					for (int c = 0; c < 10; c++)
					{
						int base10Palindrome = (a * 100000) + (b * 10000) + (c * 1000) 
							+ (c * 100) + (b * 10) + a; 
						;
						if (CommonAlgorithms.IsIntPalindromicInBinary(base10Palindrome))
						{
							answer += base10Palindrome;
						}
					}
				}
			}

			PrintSolution(answer.ToString());
			return;
		}
		public void Run_bruteForce()
		{
			/*
			 * from the instructions: 
			 * 
			 *    Please note that the palindromic number, in either 
			 *    base, may not include leading zeros.
			 *    
			 * that means that only odd numbers can be a palindrome 
			 * in binary. so we can skip all evens and make this run
			 * a good bit faster
			 * 
			 * */

			int min = 1;
			int max = 999999;

			int answer = 0;

			for (int i = min; i <= max; i += 2)
			{
				if (CommonAlgorithms.IsIntPalindromic(i))
				{
					if (CommonAlgorithms.IsIntPalindromicInBinary(i))
					{
						answer += i;
					}
				}
			}

			PrintSolution(answer.ToString());
			return;
		}
		public void Run_stringReverse()
        {
			int min = 1;
			int max = 999999;
			int answer = 0;

			for (int i = min; i < max; i += 2)
            {
				string b10string = i.ToString();
				if (b10string == b10string.Reverse())
                {
					string b2string = Convert.ToString(i, 2);
					if (b2string == b2string.Reverse())
                    {
						answer += i;
                    }
				}
            }
			PrintSolution(answer.ToString());
			return;
		}



	}
}
