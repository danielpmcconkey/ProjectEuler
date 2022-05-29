namespace EulerProblems.Lib.Problems
{
	public class Euler0038 : Euler
	{
		public Euler0038() : base()
		{
			title = "Pandigital multiples";
			problemNumber = 38;
			
		}
		protected override void Run()
		{
			/*
			 * I didn't like the way the instructions were written. To me, the last 
			 * sentence: 
			 * 
			 *           "What is the largest 1 to 9 pandigital 9-digit number
			 *           that can be formed as the concatenated product of an 
			 *           integer with (1,2, ... , n) where n > 1?"
			 *           
			 * didn't make it clear that n could be 2. Probably, math folks get this 
			 * intuitively, but I thought it had to go at least to 3 as 1, 2, and n 
			 * were all listed as requirements. So I never got the right answer until
			 * after I set my minimum to 2. Whatevs, I'm not mad. Who's mad?
			 * 
			 * From there, it was tough to find the right order to go in. I could easily
			 * have created a list that held onto the pandigital numbers and then 
			 * compared to grab the largest. But that wasn't fun. So I wanted to create
			 * a cascade that would go from largest to smallest possible, such that when
			 * I first found a pandigital, I knew it'd be the largest.
			 * 
			 * I knew from working in excel that the largest value for i couldn't be 
			 * more than 4 digits. if i were 100000, then i * 1 would have 6 digits and
			 * i * 2 would have 6 digits. You can't concatenate 12 digits and make a 
			 * 1 - 9 pandigital. I also knew that the value had to be greater than 9
			 * since 9 was provided in the problem statement. (I did check to confirm
			 * that 918273645 was the wrong answer.
			 * 
			 * So here's how it works. Because the pandigital number always starts with
			 * i * 1, i has to go in a very specific order (read left column and go down
			 * and then move one column to the right)...
			 * 
			 *      9876	985	    9821	9763	9735
             *      9875	9847	982	    9762	9734
             *      9874	9846	9817	9761	9732
             *      9873	9845	9816	976	    9731
             *      9872	9843	9815	9758	973
             *      9871	9842	9814	9756	9728
             *      987     9841	9813	9754	9726
             *      9867	984	    9812	9753	9725
             *      9865	9837	981	    9752	9724
             *      9864	9836	98	    9751	9723
             *      9863	9835	9786	975	    9721
             *      9862	9834	9785	9748	972
             *      9861	9832	9784	9746	9718
             *      986	    9831	9783	9745	9716
             *      9857	983	    9782	9743	9715
             *      9856	9827	9781	9742	9714
             *      9854	9826	978	    9741	9713
             *      9853	9825	9768	974	    9712
             *      9852	9824	9765	9738	971
             *      9851	9823	9764	9736	97
			 *      
			 * 
			 * How to do that programatically, though? I tried creating a schweet 
			 * cascade that incremented them just right. But, in the end, while I think
			 * I got it right, it was pretty unweildy under all the rules. So I left it
			 * at what you see below
			 * 
			 * */

			// create a list of all possible numbers starting with 9
			List<int> numbers = new List<int>();
			numbers.Add(9);
			for (int n = 91; n <= 98; n++)  numbers.Add(n);
			for (int n = 912; n <= 987; n++) numbers.Add(n);
			for (int n = 9123; n <= 9876; n++) numbers.Add(n);
			
			// remove the rubbish
			List<int> numbersWithoutRubbish = new List<int>();
			foreach(int n in numbers)
            {
				int[] nAsArray = CommonAlgorithms.ConvertIntToIntArray(n);
				// first, throw out any with a 0 in it
				if (!nAsArray.Contains(0))	
                {
					// now throw out any with a repeating digit
					int[] arrayCopy = nAsArray;
					Array.Sort(arrayCopy);
					bool isRubbish = false;
					for(int j = 1; j < arrayCopy.Length; j++)
                    {
						if(arrayCopy[j] == arrayCopy[j - 1])
                        {
							// rubbish
							isRubbish = true;
                        }
                    }
					if(!isRubbish)
                    {
						numbersWithoutRubbish.Add(n);

					}
                }
            }

			// now do a "special" sort based on which would produce the higher final
			// pandigital
			numbersWithoutRubbish.Sort(CompareIntsAsCandidates);
			
			// all set, now let's blow this joint
			long answer = 0;
			foreach (int i in numbersWithoutRubbish)
            {
				//Console.WriteLine(i);
				if (IsAPandigitalMultiple(i, out answer))
                {
                    PrintSolution(answer.ToString());
                    return;
                }
            }			
		}
		private static int CompareIntsAsCandidates(int a, int b)
        {
			if (a == b) return 0;
			/*
			 * we want to sort based on which would be the start of the higher 
			 * pandigital. So, in this case 9 would preceed 84.
			 * 
             * less than zero means that this should preceed other in an ascending sort
             * zero means the two are equal
             * greater than zero means that this should follow other in an ascending sort
             * */
			int aOOM = CommonAlgorithms.GetOrderOfMagnitude(a);
			int bOOM = CommonAlgorithms.GetOrderOfMagnitude(b);
			if (aOOM == bOOM) return (a > b) ? -1 : 1;
			// pad 0s to the smaller number
			if(aOOM > bOOM)
            {
				b = b * (int)Math.Pow(10, aOOM - bOOM);
            }
			else 
			{
				a = a * (int)Math.Pow(10, bOOM - aOOM);
			}
			return (a > b) ? -1 : 1;
        }
		private bool IsAPandigitalMultiple(int candidate, out long pandigital)
        {
			
			pandigital = 0;
			const int maxNumberOfPruducts = 9;  // by definition, an N greater than 9 would result in more than 9 digits
			const int minNumberOfPruducts = 2;

			// reset the lists each time
			List<int> products = new List<int>();
			List<int> productOOMs = new List<int>();

			for(int i = 1; i <= maxNumberOfPruducts; i++)
			{
				int product = candidate * i;
				products.Add(product);
				productOOMs.Add(CommonAlgorithms.GetOrderOfMagnitude(product));
				if(i >= minNumberOfPruducts)
				{
					// check the number of digits
					int totalNumberOfDigits = productOOMs.Sum() + i;
						
					if (totalNumberOfDigits > 9) return false;
					if(totalNumberOfDigits == 9)
					{
						long checkVal = 0;
						for(int j = 0; j < products.Count; j++)
						{
							int powerOf10 = 8;
							if (j == 0) powerOf10 -= productOOMs[0];
							else
							{
								for(int k = 0; k <= j; k++)
                                {
									powerOf10 -= (productOOMs[k]);
								}
								powerOf10 -= j;
							}
							checkVal += (products[j] * (long)Math.Pow(10, powerOf10));							
						}
						if (CommonAlgorithms.IsPandigital(checkVal))
						{
							pandigital = checkVal;
							return true;
						}
							
					}
				}
			}
			return false;            
        }

	}
}
