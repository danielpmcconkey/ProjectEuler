using EulerProblems.Lib;
using System.Text;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
	internal class Euler0032 : Euler
	{
		public Euler0032() : base()
		{
			title = "Pandigital products";
			problemNumber = 32;
			PrintTitle();
		}
		public override void Run()
		{
			

			//Run_bruteForce();	
			Run_elegant();
		}
		private void Run_elegant()
		{
			int sum = 0;
			// start from the product and work backward
			// the product must be between 1234 and 9876
			for(int product = 1234; product <= 9876; product++)
            {
				newProduct:
				// check if the product has any duplicates
				if(!DoesIntHaveDuplicates(product))
                {
					// check the factors
					int[] factors = CommonAlgorithms.GetFactors(product);
					foreach(int factorA in factors)
                    {
						if(!DoesIntHaveDuplicates(factorA))
                        {
							int factorB = product / factorA;
							if (!DoesIntHaveDuplicates(factorB))
							{
								if(IsPandigital(factorA, factorB, product))
                                {
									sum += product;
									product++;
									goto newProduct;
                                }
							}
						}
                    }
                }
            }
			PrintSolution(sum.ToString());
			return;
		}
		private bool DoesIntHaveDuplicates(int n)
        {
			char[] chars = n.ToString().ToCharArray();
			Array.Sort(chars);
			for(int i = 0; i < chars.Length - 1; i++)
            {
				if(chars[i] == chars[i + 1]) return true;
            }
			return false;
        }

		
		private void Run_bruteForce()
		{
			List<long> products = new List<long>();
			/* what's the largest number to test?
			 * 9 * 8765 = 78885 
			 * all that has 10 digits. Any equation
			 * that uses a number bigger than 8765
			 * wouldn't be pandigital.
			 * */
			long limit = 8765;
			for(int a = 0; a <= limit; a++)
            {
				for (int b = 0; b <= limit; b++)
                {
					int product = a * b;
					if(IsPandigital(a, b, product))
                    {
						if(!products.Contains(product))
                        {
							products.Add(product);
                        }
                    }
                }

			}

			
			PrintSolution(products.Sum().ToString());
			return;
		}
		private bool IsPandigital(int a, int b, int product)
        {
			int productOOM = CommonAlgorithms.GetOrderOfMagnitude(product);
			int aOOM = CommonAlgorithms.GetOrderOfMagnitude(a);
			int bOOM = CommonAlgorithms.GetOrderOfMagnitude(b);
			
			long checkLong = (long)(
				product 
				+ b * Math.Pow(10, productOOM + 1)
				+ a * Math.Pow(10, productOOM + bOOM + 2)
				);
			
			return CommonAlgorithms.IsPandigital(checkLong);
        }




	}
}
