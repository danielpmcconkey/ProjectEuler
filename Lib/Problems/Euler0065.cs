//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0065 : Euler
	{
		public Euler0065() : base()
		{
			title = "Convergents of e";
			problemNumber = 65;
		}
		protected override void Run()
		{
			/*
			 * This wasn't very hard for me. More time consuming, really. 
			 * Coming off of problem 64, I felt I had much better footing for 
			 * dealing with continued fractions. So I went over to Wikipedia 
			 * because I thought I remembered seeing examples in the continued
			 * fraction page that dealt with e.
			 * 
			 * https://en.wikipedia.org/wiki/Continued_fraction
			 * 
			 * It turns out, not only did it have stuff on e, but it also 
			 * completelye gave away the solution. According to wikipedia, e is
			 * represented as a continued fraction as follows: 
			 * 
			 *      e = [2;1,2,1,1,4,1,1,6,1,1,8,1,1,10,1,1...]
			 *      
			 * It then says: "The pattern repeats indefinitely with a period 
			 * of 3 except that 2 is added to one of the terms in each cycle." 
			 * So, after the second coefficient, we have a pattern of 1, 1, n
			 * where n goes up 2 every time.
			 * 
			 * Now I needed a way to convert the continued fraction to the 
			 * fraction it converges toward and this is the time consuming 
			 * part. For this, I played around in google sheets for a while to
			 * figure out that the easy way to solve that was, to go from right
			 * to left (last coeffficient to first) and, for each, add it to my
			 * previous fraction and take the reciprocal (swap numerator and 
			 * denominator). Do this all the way up the chain and, for the 
			 * first coefficient, only add; dont take the recriprocal.
			 * 
			 * For this, I decided to beef up my fractions library and created
			 * the FractionCalculator class separate from the Fraction struct. 
			 * I then moved the old Reduce function from the struct to the 
			 * class and added "Add" and "Reciprocate" functions to the class.
			 * 
			 * I ran the new solution on the first 10 euler sequence 
			 * coefficients and got the answer stated in the problem 
			 * definition. Cool beans. But running it up to 100, got to an 
			 * integer overrun at the 14th coefficient handled. These numbers 
			 * get big real quick. So I skipped longs altogether and moved 
			 * straight to BigIntegers.
			 * 
			 * The next problem I ran into was execution speed. My 
			 * FractionCalculator class reduced the fraction at each add 
			 * operation and again in the recirocate function. This is what 
			 * you'd normally expect from stand-alone functions like this. but
			 * these numbers are big and the reduction method uses the greatest
			 * common factor method. Finding factors of numbers this large 
			 * takes stupid time. So I decided to change my methods to assume
			 * that reduction was needed, but take an optional parameter to not
			 * reduce. 
			 * 
			 * With this, I decided to only reduce the last step. But even this
			 * just took too long. So I turned off all reduction and got the 
			 * unreduced convergent fraction. It turns out that euler accepted
			 * the sum of this numerator so, either the fraction doesn't reduce
			 * or they didn't reduce it either.
			 * 
			 * Winner, winner, chicken dinner.
			 * 
			 * Final thought: 
			 * Since it seems that the Euler project guys love these stupid 
			 * continued fractions problems, I decided to move my logic to get
			 * the convergent fraction out of a continued fraction into the 
			 * FractionCalculator class. Method GetContinuedFractionConvergence
			 * is now in there, but I'll probably forget where it is next time
			 * I need it.
			 * 
			 * */

			int targetPosition = 100;
			int coefficient_0 = 2;
			// create the subsequent coefficients list. the first 2 don't
			// follow the 1,1,* pattern, so pre-seed them manually.
			List<int> subsequentCoefficients = new List<int>() { 1, 2 }; 
			int last2jump = 2;
			for(int i = subsequentCoefficients.Count + 1; i < targetPosition; i++)
            {
				int numAdd = 1;
				if (i % 3 == 2)
                {
					numAdd = last2jump + 2;
					last2jump = numAdd;
                }
				subsequentCoefficients.Add(numAdd);
            }

			// create a continued fraction out of that
			ContinuedFraction e = new ContinuedFraction()
			{
				firstCoefficient = coefficient_0,
				subsequentCoefficients = subsequentCoefficients.ToArray(),
				doCoefficientsRepeat = false,
			};
			// now find the fraction at the targetPosition
			BigFraction convergence = FractionCalculator.GetContinuedFractionConvergence(e);

			

			// now sum the numerator digits
			int[] numeratorDigits = CommonAlgorithms.ConvertBigToIntArray(convergence.numerator);
			int answer = numeratorDigits.Sum();
			PrintSolution(answer.ToString());
			return;
		}

		
	}
}
