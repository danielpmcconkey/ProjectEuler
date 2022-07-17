//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0099 : Euler
	{
		public Euler0099() : base()
		{
			title = "Largest exponential";
			problemNumber = 99;
		}
		protected override void Run()
		{
            /*
             * Not hard at all, if you've ever heard of logarithms. I knew that 
             * you could use logs to "cancel out" exponents. It only took a 
             * minute of googling to find the right use.
             * 
             * */

            const string filePath = @"E:\ProjectEuler\ExternalFiles\p099_base_exp.txt";
            var lines = File.ReadLines(filePath);
            var baseExponentPairs = new List<(int lineNum, int baseNum, int exponent, double log)>();
            int i = 0;
            foreach (var l in lines)
            {
                i++;
                var split = l.Split(",");
                var baseNum = int.Parse(split[0]);
                var exponent = int.Parse(split[1]);
                var log = Math.Log10(baseNum) * exponent;
                baseExponentPairs.Add((i, baseNum, exponent, log));
            }
            int answer = baseExponentPairs.OrderByDescending(x => x.log).First().lineNum;

            PrintSolution(answer.ToString());
			return;
		}
	}
}
