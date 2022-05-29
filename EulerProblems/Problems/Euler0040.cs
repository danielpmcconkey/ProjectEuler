using EulerProblems.Lib;
using System.Text;

namespace EulerProblems.Problems
{
	internal class Euler0040 : Euler
	{
		public Euler0040() : base()
		{
			title = "Champernowne's constant";
			problemNumber = 40;
			PrintTitle();
		}
        public override void Run()
        {
            //Run_bruteForce();
            Run_elegant();
        }
        public void Run_elegant()
        {
            /*
             * I wrote this in Excel. It was just easier that way for me to get
             * my head wrapped around this problem. I knew that we could simply
             * math our way to our answer based on how many 2-digt, 3-digit, 
             * etc numbers there were. My spreadsheet looks like this. (the 
             * debug output produces it):
             * 
             *                                                                   count of    will
             *                                              num                  ints        that                     number                number
             *                                              digits               until       get us       position    needed                needed                 position     position
             *                        last      last        last      next OOM   next OOM    to next      at next     at this               at next   number at    after this   of digit
             *    OOM    target       number    position    number    jump       jump        target?      OOM jump    OOM       gap         OOM       next OOM     hit          in array  digit
             *    -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
             *        0            1         0           0         1         10           9         True           9         1           0         0            1            1         0      1
             *        1           10         1           1         1         10           8        False           9         8           1         1           10           11         0      1
             *        2          100        10          11         2        100          89         True         189        45           0         0           55          101         0      5
             *        3         1000        55         101         2        100          44        False         189        44         811       271          370         1002         0      3
             *        4        10000       370        1002         3       1000         629        False        2889       629        7111      1778         2777        10001         2      7
             *        5       100000      2777       10001         4      10000        7222        False       38889      7222       61111     12223        22222       100004         0      2
             *        6      1000000     22222      100004         5     100000       77777        False      488889     77777      511111     85186       185185      1000005         0      1
             *
             * 
             * 
             *  
             * */

            int[] checkVals = new int[] { 1, 10, 100, 1000, 10000, 100000, 1000000 };
            int product = 1;
            int lastNumber = 0;
            int lastPosition = 0;

#if DEBUG
            Dictionary<string, int> outputColumns = new Dictionary<string, int>();
            outputColumns.Add("targetOOM", 7);
            outputColumns.Add("target", 13);
            outputColumns.Add("lastNumber", 10);
            outputColumns.Add("lastPosition", 12);
            outputColumns.Add("numDigitsLastNumber", 10);
            outputColumns.Add("nextOOMJump", 11);
            outputColumns.Add("countOfIntsUntilNextOOMJump", 12);
            outputColumns.Add("willThatGetUsToNextTarget", 13);
            outputColumns.Add("positionAtNextOOMJump", 12);
            outputColumns.Add("numberNeededAtThisOOM", 10);
            outputColumns.Add("gap", 12);
            outputColumns.Add("numberNeededAtNextOOM", 10);
            outputColumns.Add("numberAtNextOOM", 13);
            outputColumns.Add("positionAfterThisHit", 13);
            outputColumns.Add("positionOfDigitInArray", 10);
            outputColumns.Add("digit", 7);

            StringBuilder sb = new StringBuilder();
            // heading line 1
            sb.Append("  ".PadRight(outputColumns["targetOOM"]));
            sb.Append("  ".PadRight(outputColumns["target"]));
            sb.Append("  ".PadRight(outputColumns["lastNumber"]));
            sb.Append("  ".PadRight(outputColumns["lastPosition"]));
            sb.Append("  ".PadRight(outputColumns["numDigitsLastNumber"]));
            sb.Append("  ".PadRight(outputColumns["nextOOMJump"]));
            sb.Append("  count of".PadRight(outputColumns["countOfIntsUntilNextOOMJump"]));
            sb.Append("  will".PadRight(outputColumns["willThatGetUsToNextTarget"]));
            sb.Append(" ".PadRight(outputColumns["positionAtNextOOMJump"]));
            sb.Append(" ".PadRight(outputColumns["numberNeededAtThisOOM"]));
            sb.Append(" ".PadRight(outputColumns["gap"]));
            sb.Append(" ".PadRight(outputColumns["numberNeededAtNextOOM"]));
            sb.Append(" ".PadRight(outputColumns["numberAtNextOOM"]));
            sb.Append(" ".PadRight(outputColumns["positionAfterThisHit"]));
            sb.Append(" ".PadRight(outputColumns["positionOfDigitInArray"]));
            sb.Append(" ".PadRight(outputColumns["digit"]));
            sb.AppendLine();
            // heading line 2
            sb.Append(" ".PadRight(outputColumns["targetOOM"]));
            sb.Append(" ".PadRight(outputColumns["target"]));
            sb.Append(" ".PadRight(outputColumns["lastNumber"]));
            sb.Append(" ".PadRight(outputColumns["lastPosition"]));
            sb.Append("  num".PadRight(outputColumns["numDigitsLastNumber"]));
            sb.Append("   ".PadRight(outputColumns["nextOOMJump"]));
            sb.Append("  ints".PadRight(outputColumns["countOfIntsUntilNextOOMJump"]));
            sb.Append("  that".PadRight(outputColumns["willThatGetUsToNextTarget"]));
            sb.Append("  ".PadRight(outputColumns["positionAtNextOOMJump"]));
            sb.Append("  number".PadRight(outputColumns["numberNeededAtThisOOM"]));
            sb.Append("  ".PadRight(outputColumns["gap"]));
            sb.Append("  number".PadRight(outputColumns["numberNeededAtNextOOM"]));
            sb.Append("  ".PadRight(outputColumns["numberAtNextOOM"]));
            sb.Append("  ".PadRight(outputColumns["positionAfterThisHit"]));
            sb.Append(" ".PadRight(outputColumns["positionOfDigitInArray"]));
            sb.Append("  ".PadRight(outputColumns["digit"]));
            sb.AppendLine();
            // heading line 3
            sb.Append(" ".PadRight(outputColumns["targetOOM"]));
            sb.Append(" ".PadRight(outputColumns["target"]));
            sb.Append(" ".PadRight(outputColumns["lastNumber"]));
            sb.Append(" ".PadRight(outputColumns["lastPosition"]));
            sb.Append("  digits".PadRight(outputColumns["numDigitsLastNumber"]));
            sb.Append("   ".PadRight(outputColumns["nextOOMJump"]));
            sb.Append("  until".PadRight(outputColumns["countOfIntsUntilNextOOMJump"]));
            sb.Append("  get us".PadRight(outputColumns["willThatGetUsToNextTarget"]));
            sb.Append("  position".PadRight(outputColumns["positionAtNextOOMJump"]));
            sb.Append("  needed".PadRight(outputColumns["numberNeededAtThisOOM"]));
            sb.Append("  ".PadRight(outputColumns["gap"]));
            sb.Append("  needed".PadRight(outputColumns["numberNeededAtNextOOM"]));
            sb.Append("  ".PadRight(outputColumns["numberAtNextOOM"]));
            sb.Append("  position".PadRight(outputColumns["positionAfterThisHit"]));
            sb.Append("  position".PadRight(outputColumns["positionOfDigitInArray"]));
            sb.Append(" ".PadRight(outputColumns["digit"]));
            sb.AppendLine();
            // heading line 4
            sb.Append(" ".PadRight(outputColumns["targetOOM"]));
            sb.Append(" ".PadRight(outputColumns["target"]));
            sb.Append("  last".PadRight(outputColumns["lastNumber"]));
            sb.Append("  last".PadRight(outputColumns["lastPosition"]));
            sb.Append("  last".PadRight(outputColumns["numDigitsLastNumber"]));
            sb.Append("  next OOM".PadRight(outputColumns["nextOOMJump"]));
            sb.Append("  next OOM".PadRight(outputColumns["countOfIntsUntilNextOOMJump"]));
            sb.Append("  to next".PadRight(outputColumns["willThatGetUsToNextTarget"]));
            sb.Append("  at next".PadRight(outputColumns["positionAtNextOOMJump"]));
            sb.Append("  at this".PadRight(outputColumns["numberNeededAtThisOOM"]));
            sb.Append("  ".PadRight(outputColumns["gap"]));
            sb.Append("  at next".PadRight(outputColumns["numberNeededAtNextOOM"]));
            sb.Append("  number at".PadRight(outputColumns["numberAtNextOOM"]));
            sb.Append("  after this".PadRight(outputColumns["positionAfterThisHit"]));
            sb.Append("  of digit".PadRight(outputColumns["positionOfDigitInArray"]));
            sb.Append(" ".PadRight(outputColumns["digit"]));
            sb.AppendLine();
            // heading line 5
            sb.Append("  OOM".PadRight(outputColumns["targetOOM"]));
            sb.Append("  target".PadRight(outputColumns["target"]));
            sb.Append("  number".PadRight(outputColumns["lastNumber"]));
            sb.Append("  position".PadRight(outputColumns["lastPosition"]));
            sb.Append("  number".PadRight(outputColumns["numDigitsLastNumber"]));
            sb.Append("  jump".PadRight(outputColumns["nextOOMJump"]));
            sb.Append("  jump".PadRight(outputColumns["countOfIntsUntilNextOOMJump"]));
            sb.Append("  target?".PadRight(outputColumns["willThatGetUsToNextTarget"]));
            sb.Append("  OOM jump".PadRight(outputColumns["positionAtNextOOMJump"]));
            sb.Append("  OOM".PadRight(outputColumns["numberNeededAtThisOOM"]));
            sb.Append("  gap".PadRight(outputColumns["gap"]));
            sb.Append("  OOM".PadRight(outputColumns["numberNeededAtNextOOM"]));
            sb.Append("  next OOM".PadRight(outputColumns["numberAtNextOOM"]));
            sb.Append("  hit".PadRight(outputColumns["positionAfterThisHit"]));
            sb.Append("  in array".PadRight(outputColumns["positionOfDigitInArray"]));
            sb.Append("  digit".PadRight(outputColumns["digit"]));
            sb.AppendLine();
            int totalWidth = outputColumns.Sum(x => x.Value);
            for (int i = 0; i < totalWidth; i++) sb.Append("-");
            Console.WriteLine(sb.ToString()); 
#endif

            foreach (int target in checkVals)
            {
                int targetOOM = CommonAlgorithms.GetOrderOfMagnitude(target);
                int numDigitsLastNumber = CommonAlgorithms.GetOrderOfMagnitude(lastNumber) + 1;
                 
                int nextOOMJump = (int)Math.Pow(10, numDigitsLastNumber);
                int countOfIntsUntilNextOOMJump = nextOOMJump - lastNumber - 1;
                bool willThatGetUsToNextTarget =
                    (lastPosition + (countOfIntsUntilNextOOMJump * numDigitsLastNumber) >= target) ?
                    true : 
                    false;
                int positionAtNextOOMJump = lastPosition + (countOfIntsUntilNextOOMJump * numDigitsLastNumber);
                int numberNeededAtThisOOM = (willThatGetUsToNextTarget) ?
                    (int)Math.Ceiling((target - lastPosition) / 2d) :
                    countOfIntsUntilNextOOMJump;
                int gap = (willThatGetUsToNextTarget) ? 
                    0 :
                    target - lastPosition - (numberNeededAtThisOOM * numDigitsLastNumber);
                int numberNeededAtNextOOM = (willThatGetUsToNextTarget) ? 
                    0 :
                    (int)Math.Ceiling(gap / (numDigitsLastNumber + 1d));
                int numberAtNextOOM = lastNumber + numberNeededAtThisOOM + numberNeededAtNextOOM;

                // now that we've got the number that crosses the target
                // position, we need to find out which position of that number
                // is the one that crosses the line

                int[] numAsArray = CommonAlgorithms.ConvertIntToIntArray(numberAtNextOOM);
                int positionAfterThisHit = lastPosition + (numberNeededAtThisOOM * numDigitsLastNumber)
                    + (numberNeededAtNextOOM * (numDigitsLastNumber + 1));

                int positionOfDigitInArray = numAsArray.Length - 1 - (positionAfterThisHit - target);

                int digit = numAsArray[positionOfDigitInArray];

                product *= digit;


#if DEBUG
                StringBuilder sbLine = new StringBuilder();
                sbLine.Append(targetOOM.ToString().PadLeft(outputColumns["targetOOM"]));
                sbLine.Append(target.ToString().PadLeft(outputColumns["target"]));
                sbLine.Append(lastNumber.ToString().PadLeft(outputColumns["lastNumber"]));
                sbLine.Append(lastPosition.ToString().PadLeft(outputColumns["lastPosition"]));
                sbLine.Append(numDigitsLastNumber.ToString().PadLeft(outputColumns["numDigitsLastNumber"]));
                sbLine.Append(nextOOMJump.ToString().PadLeft(outputColumns["nextOOMJump"]));
                sbLine.Append(countOfIntsUntilNextOOMJump.ToString().PadLeft(outputColumns["countOfIntsUntilNextOOMJump"]));
                sbLine.Append(willThatGetUsToNextTarget.ToString().PadLeft(outputColumns["willThatGetUsToNextTarget"]));
                sbLine.Append(positionAtNextOOMJump.ToString().PadLeft(outputColumns["positionAtNextOOMJump"]));
                sbLine.Append(numberNeededAtThisOOM.ToString().PadLeft(outputColumns["numberNeededAtThisOOM"]));
                sbLine.Append(gap.ToString().PadLeft(outputColumns["gap"]));
                sbLine.Append(numberNeededAtNextOOM.ToString().PadLeft(outputColumns["numberNeededAtNextOOM"]));
                sbLine.Append(numberAtNextOOM.ToString().PadLeft(outputColumns["numberAtNextOOM"]));
                sbLine.Append(positionAfterThisHit.ToString().PadLeft(outputColumns["positionAfterThisHit"]));
                sbLine.Append(positionOfDigitInArray.ToString().PadLeft(outputColumns["positionOfDigitInArray"]));
                sbLine.Append(digit.ToString().PadLeft(outputColumns["digit"]));
                Console.WriteLine(sbLine.ToString()); 
#endif

                // update the "last" values before moving on to the next target
                lastNumber = numberAtNextOOM;
                lastPosition = positionAfterThisHit;
            }

#if DEBUG
            Console.WriteLine(); 
#endif

            PrintSolution(product.ToString());
			return;
		}
        public void Run_bruteForce()
        {
            /*
			 * first we have 9 1-digit numbers (1 - 9)
			 * then we'll have 90 2-digit numbers (10 - 99)
			 * then we (100 - 999)
			 * */

            int[] checkVals = new int[] { 1, 10, 100, 1000, 10000, 100000, 1000000 };

            int product = 1;

            int placeCurrent = 0;
            int placePrior = 0;

            for (int i = 1; i < 300000; i++)
            {
                int oom = CommonAlgorithms.GetOrderOfMagnitude(i);
                int numDigits = oom + 1;
                placeCurrent = placePrior + numDigits;
                for (int j = 0; j < checkVals.Length; j++)
                {
                    if (placeCurrent >= checkVals[j] && placePrior < checkVals[j])
                    {
                        int[] iAsArray = CommonAlgorithms.ConvertIntToIntArray(i);
                        Array.Reverse(iAsArray);    // makes it easier to pull the right digit
                        int positionInArray = placeCurrent - checkVals[j];
                        int digit = iAsArray[positionInArray];
                        Console.WriteLine(string.Format("digit: {2}; place: {0}; i: {1};",
                            placeCurrent, i, digit));

                        product *= digit;

                    }

                }
                placePrior = placeCurrent;
            }

            PrintSolution(product.ToString());
            return;
        }

    }
}
