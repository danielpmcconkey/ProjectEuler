//#define VERBOSEOUTPUT
using System.Text;

namespace EulerProblems.Lib.Problems
{
	public class Euler0089 : Euler
	{
		public Euler0089() : base()
		{
			title = "Roman numerals";
			problemNumber = 89;
		}
		protected override void Run()
		{
            /*
             * This wasn't hard in the least for me. Probably because this one, 
             * unlike most of the others, is somewhat of a practical exercise. 
             * I have decades of experience solving real-world business 
             * problems through code and the Euler guys even gave us business
             * requirements and use cases. I was at home and I think I even
             * got this one right on the very first time I pressed F5.
             * 
             * Note: that's not true. I pressed F5 a couple of times to test 
             * out my reads and writes with real numbers. But the first time I
             * had it read the input values, I got it right, I'm pretty sure.
             * 
             * */

            const string filePath = @"E:\ProjectEuler\ExternalFiles\p089_roman.txt";
            string[] lines = File.ReadLines(filePath).ToArray();
            int answer = 0;
            foreach(string line in lines)
            {
                int inLength = line.Length;
                var num = ReadRomanNumeral(line);
                int outLength = WriteRomanNumeral(num).Length;
                answer += inLength - outLength;                
            }
			PrintSolution(answer.ToString());
			return;
		}
        private int ReadRomanNumeral(string s)
        {
            string newString = s.ToUpper().Replace("CM", "900,");
            newString = newString.Replace("CD", "400,");
            newString = newString.Replace("XC", "90,");
            newString = newString.Replace("XL", "40,");
            newString = newString.Replace("IX", "9,");
            newString = newString.Replace("IV", "4,");
            newString = newString.Replace("M", "1000,");
            newString = newString.Replace("D", "500,");
            newString = newString.Replace("C", "100,");
            newString = newString.Replace("L", "50,");
            newString = newString.Replace("X", "10,");
            newString = newString.Replace("V", "5,");
            newString = newString.Replace("I", "1,");
            newString += "0";
            var strings = newString.Split(",");
            var nums = new List<int>();
            foreach(var str in strings) nums.Add(int.Parse(str));
            return nums.Sum();
        }
        private string WriteRomanNumeral(int n)
        {
            int thousands = (int)Math.Floor(n / 1000D);
            int remainder = n - (thousands * 1000);
            int hundreds = (int)Math.Floor(remainder / 100D);
            remainder = remainder - (hundreds * 100);
            int tens = (int)Math.Floor(remainder / 10D);
            remainder = remainder - (tens * 10);
            int ones = remainder;

            Func<int, string> writeThousands = (n) =>
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < n; i++) sb.Append("M");
                return sb.ToString();
            };
            Func<int, string> writeHundreds = (n) =>
            {
                if (n == 9) return "CM";
                if (n == 4) return "CD";
                StringBuilder sb = new StringBuilder();
                int remainingDigits = n;
                if(n >= 5)
                {
                    sb.Append("D");
                    remainingDigits -= 5;
                }
                for (int i = 0; i < remainingDigits; i++) sb.Append("C");
                return sb.ToString();
            };
            Func<int, string> writeTens = (n) =>
            {
                if (n == 9) return "XC";
                if (n == 4) return "XL";
                StringBuilder sb = new StringBuilder();
                int remainingDigits = n;
                if (n >= 5)
                {
                    sb.Append("L");
                    remainingDigits -= 5;
                }
                for (int i = 0; i < remainingDigits; i++) sb.Append("X");
                return sb.ToString();
            };
            Func<int, string> writeOnes = (n) =>
            {
                if (n == 9) return "IX";
                if (n == 4) return "IV";
                StringBuilder sb = new StringBuilder();
                int remainingDigits = n;
                if (n >= 5)
                {
                    sb.Append("V");
                    remainingDigits -= 5;
                }
                for (int i = 0; i < remainingDigits; i++) sb.Append("I");
                return sb.ToString();
            };

            StringBuilder sb = new StringBuilder();
            sb.Append(writeThousands(thousands));
            sb.Append(writeHundreds(hundreds));
            sb.Append(writeTens(tens));
            sb.Append(writeOnes(ones));

            return sb.ToString();
        }
    }
}
