using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
	internal class Euler0028 : Euler
	{
		

		public Euler0028() : base()
		{
			title = "Number spiral diagonals";
			problemNumber = 28;
			PrintTitle();
		}
		public override void Run()
		{
			/*
			 * I solved this in google sheets first. What I did was flesh 
			 * out the pattern a little further. 
			 * 
			 *  73	74	75	76	77	78	79	80	81
			 *	72	43	44	45	46	47	48	49	50
			 *	71	42	21	22	23	24	25	26	51
			 *	70	41	20	7	8	9	10	27	52
			 *	69	40	19	6	1	2	11	28	53
			 *	68	39	18	5	4	3	12	29	54
			 *	67	38	17	16	15	14	13	30	55
			 *	66	37	36	35	34	33	32	31	56
			 *	65	64	63	62	61	60	59	58	57
			 *	
			 *	From here, it became obvious that the upper-right
			 *	corners moved in an n^2 pattern. From there, it was
			 *	easy to calculate the relationships to the other
			 *	corners.
			 *	
			 *	n    |      ur       lr        ll        ul
			 *	--------------------------------------------
			 *	1    |       1        1         1         1  
			 *	3    |       9        3         5         7  
			 *	5    |      25       13        17        21
			 *	7    |      49       31        37        43
			 *	9    |      81       57        65        73
			 *	...
			 *	1001 | 1002001   999001   1000001   1001001
			 *	
			 *	*/

			const int limit = 1001;
			int answer = 1;
			for(int n = 3; n <= limit; n += 2)
            {
				int upperRight = (int)Math.Pow(n, 2);
				int upperLeft = upperRight - (n - 1);
				int lowerLeft = upperLeft - (n - 1);
				int lowerRight = lowerLeft - (n - 1);

				answer += (upperRight + upperLeft + lowerRight + lowerLeft);
            }
			PrintSolution(answer.ToString());
			return;
		}

		

	}
}
